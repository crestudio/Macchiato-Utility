using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;
using UnityEngine;

using Macchiato.Core;
using static Macchiato.Core.MaterialUtility;
using static Macchiato.Core.Translator;

using Random = UnityEngine.Random;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class ColorGenerator : EditorWindow {

		const string ColorDeltaPath = "Caramel_Macchiato/ColorGenerator";
		const string UndoGroupName = "Macchiato ColorGenerator";

		static readonly string[] ShadowPropertyNames = new string[] {
			"_ShadowColor",
			"_Shadow2ndColor",
			"_Shadow3rdColor"
		};

		[SerializeField] Color BaseColor = Color.white;
		[SerializeField] Color ShadowColor1 = Color.black;
		[SerializeField] Color ShadowColor2 = Color.black;
		[SerializeField] Color ShadowColor3 = Color.black;
		[SerializeField, ColorUsage(true, true)] Color RimLightColor = Color.white;
		[SerializeField] Color RimShadeColor = Color.black;
		[SerializeField] GameObject AvatarGameObject;
		[SerializeField] Material SourceMaterial;
		[SerializeField] Material[] TargetMaterials = new Material[0];

		SerializedObject SerializedColorGenerator;
		SerializedProperty SerializedTargetMaterials;
		[SerializeField] ColorDelta TargetColorDelta;

		static List<ColorDelta> ColorDeltaList = new List<ColorDelta>();
		int SelectedColorDeltaIndex;
		bool IsEditingProfileName;
		string EditingProfileName = string.Empty;
		bool ShowProfileDetails;

		const float BorderX = 30f;
		float WindowColumnWidth;
		float Column4Width;
		float Column2Width;

		public static ColorGenerator Instance {
			get {
				return GetWindow<ColorGenerator>();
			}
		}

		[MenuItem("Tools/Macchiato/Utility/ColorGenerator", priority = 1000)]
		static void CreateWindow() {
			ColorGenerator AppWindow = GetWindow<ColorGenerator>(true, "Macchiato ColorGenerator", true);
			AppWindow.minSize = new Vector2(550, 500);
			AppWindow.maxSize = new Vector2(550, 1000);
		}

		void OnEnable() {
			SerializedColorGenerator = new SerializedObject(this);
			SerializedTargetMaterials = SerializedColorGenerator.FindProperty("TargetMaterials");
			AvatarGameObject = AvatarUtility.GetAvatarGameObject();
			LoadColorDeltas();
			if (ColorDeltaList.Count > 0) {
				SelectedColorDeltaIndex = 0;
				SetColorDelta(SelectedColorDeltaIndex);
			} else {
				CreateSampleColorDelta();
			}
		}

		void OnGUI() {
			if (SerializedColorGenerator == null) {
				SerializedColorGenerator = new SerializedObject(this);
				SerializedTargetMaterials = SerializedColorGenerator.FindProperty("TargetMaterials");
			}
			SerializedColorGenerator.Update();
			WindowColumnWidth = position.size.x / 4f;
			Column4Width = WindowColumnWidth - 4f;
			Column2Width = (WindowColumnWidth * 2f) - 4.75f;
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			DrawHeaderSection();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			DrawColorPreviewSection();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			DrawColorFieldSection();
			DrawOklabSection();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			DrawProfileSection();
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			DrawTargetMaterialSection();
			SerializedColorGenerator.ApplyModifiedProperties();
		}

		void DrawHeaderSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			float OriginalLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 100f;
			LanguageIndex = EditorGUILayout.Popup(GetTranslatedString("String_Language"), LanguageIndex, LanguageOption);
			EditorGUIUtility.labelWidth = OriginalLabelWidth;
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			AvatarGameObject = (GameObject)EditorGUILayout.ObjectField(GetTranslatedString("String_Avatar"), AvatarGameObject, typeof(GameObject), true);
			if (GUILayout.Button(GetTranslatedString("String_Extract"), GUILayout.Width(70f))) {
				AddAvatarMaterials();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			SourceMaterial = (Material)EditorGUILayout.ObjectField(GetTranslatedString("String_SourceMaterial"), SourceMaterial, typeof(Material), false);
			if (GUILayout.Button(GetTranslatedString("String_Extract"), GUILayout.Width(70f))) {
				ExtractMaterialColors();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
		}

		void DrawColorPreviewSection() {
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			Rect PreviewRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(70f));
			float ColumnWidth = PreviewRect.width / 4f;
			EditorGUI.DrawRect(new Rect(PreviewRect.x, PreviewRect.y, PreviewRect.width, PreviewRect.height), BaseColor);
			EditorGUI.DrawRect(new Rect(PreviewRect.x + ColumnWidth, PreviewRect.y, ColumnWidth, PreviewRect.height), ShadowColor1);
			EditorGUI.DrawRect(new Rect(PreviewRect.x + ColumnWidth * 2f, PreviewRect.y, ColumnWidth, PreviewRect.height), ShadowColor2);
			EditorGUI.DrawRect(new Rect(PreviewRect.x + ColumnWidth * 3f, PreviewRect.y, ColumnWidth, PreviewRect.height), ShadowColor3);
			Rect RimPreviewRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(30f));
			Color RimLightPreview = AddColor(BaseColor, RimLightColor);
			Color RimLightShadowPreview = AddColor(ShadowColor1, RimLightColor);
			Color RimShadePreview1 = MultiplyColor(ShadowColor2, RimShadeColor);
			Color RimShadePreview2 = MultiplyColor(ShadowColor3, RimShadeColor);
			EditorGUI.DrawRect(new Rect(RimPreviewRect.x, RimPreviewRect.y, RimPreviewRect.width / 4f, RimPreviewRect.height), RimLightPreview);
			EditorGUI.DrawRect(new Rect(RimPreviewRect.x + RimPreviewRect.width / 4f, RimPreviewRect.y, RimPreviewRect.width / 4f, RimPreviewRect.height), RimLightShadowPreview);
			EditorGUI.DrawRect(new Rect(RimPreviewRect.x + RimPreviewRect.width / 2f, RimPreviewRect.y, RimPreviewRect.width / 4f, RimPreviewRect.height), RimShadePreview1);
			EditorGUI.DrawRect(new Rect(RimPreviewRect.x + RimPreviewRect.width * 0.75f, RimPreviewRect.y, RimPreviewRect.width / 4f, RimPreviewRect.height), RimShadePreview2);
			GUILayout.BeginHorizontal();
			GUILayout.Label(GetTranslatedString("String_BaseColor"), CenteredStyle, GUILayout.Width(Column4Width));
			GUILayout.Label(GetTranslatedString("String_Shadow1Color"), CenteredStyle, GUILayout.Width(Column4Width));
			GUILayout.Label(GetTranslatedString("String_Shadow2Color"), CenteredStyle, GUILayout.Width(Column4Width));
			GUILayout.Label(GetTranslatedString("String_Shadow3Color"), CenteredStyle, GUILayout.Width(Column4Width));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label(GetTranslatedString("String_RimLightColor"), CenteredStyle, GUILayout.Width(Column2Width));
			GUILayout.Label(GetTranslatedString("String_RimShadeColor"), CenteredStyle, GUILayout.Width(Column2Width));
			GUILayout.EndHorizontal();
		}

		void DrawColorFieldSection() {
			GUILayout.BeginHorizontal();
			DrawColorField(BaseColor, value => BaseColor = value, false, Column4Width);
			DrawColorField(ShadowColor1, value => ShadowColor1 = value, false, Column4Width);
			DrawColorField(ShadowColor2, value => ShadowColor2 = value, false, Column4Width);
			DrawColorField(ShadowColor3, value => ShadowColor3 = value, false, Column4Width);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			DrawColorField(RimLightColor, value => RimLightColor = value, true, Column2Width);
			DrawColorField(RimShadeColor, value => RimShadeColor = value, false, Column2Width);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(GetTranslatedString("String_Recalculate"), EditorStyles.miniButtonLeft)) {
				CalculateFromColor(0);
			}
			if (GUILayout.Button(GetTranslatedString("String_Recalculate"), EditorStyles.miniButtonMid)) {
				CalculateFromColor(1);
			}
			if (GUILayout.Button(GetTranslatedString("String_Recalculate"), EditorStyles.miniButtonMid)) {
				CalculateFromColor(2);
			}
			if (GUILayout.Button(GetTranslatedString("String_Recalculate"), EditorStyles.miniButtonRight)) {
				CalculateFromColor(3);
			}
			GUILayout.EndHorizontal();
		}

		void DrawColorField(Color TargetColor, Action<Color> SetColor, bool HDR, float TargetWidth) {
			Color NewColor = EditorGUILayout.ColorField(GUIContent.none, TargetColor, true, true, HDR, GUILayout.Width(TargetWidth));
			if (NewColor != TargetColor) SetColor(NewColor);
		}

		void DrawOklabSection() {
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(GetTranslatedString("String_CalculateOklabGradient"), GUILayout.Width(Column2Width))) {
				CalculateOklabGradient();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
	}

		void DrawProfileSection() {
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label(GetTranslatedString("String_Profile"), GUILayout.Width(70f));
			string[] ProfileNameList = ColorDeltaList.Select(Item => Item.Name).ToArray();
			if (ProfileNameList.Length > 0) {
				int NewSelectedProfileIndex = EditorGUILayout.Popup(SelectedColorDeltaIndex, ProfileNameList);
				if (NewSelectedProfileIndex != SelectedColorDeltaIndex) {
					SetColorDelta(NewSelectedProfileIndex);
				}
			}
			if (GUILayout.Button(GetTranslatedString("String_Create"), EditorStyles.miniButtonLeft, GUILayout.Width(55f))) {
				CreateColorDelta();
			}
			if (GUILayout.Button(GetTranslatedString("String_Edit"), EditorStyles.miniButtonMid, GUILayout.Width(55f))) {
				BeginEditProfileName();
			}
			if (GUILayout.Button(GetTranslatedString("String_Import"), EditorStyles.miniButtonMid, GUILayout.Width(55f))) {
				ImportColorDelta();
			}
			if (GUILayout.Button(GetTranslatedString("String_Export"), EditorStyles.miniButtonRight, GUILayout.Width(55f))) {
				ExportColorDelta();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			if (IsEditingProfileName) {
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				EditingProfileName = EditorGUILayout.TextField(EditingProfileName);
				if (GUILayout.Button(GetTranslatedString("String_Save"), EditorStyles.miniButtonLeft, GUILayout.Width(55f))) {
					SaveEditedProfileName();
				}
				if (GUILayout.Button(GetTranslatedString("String_Cancel"), EditorStyles.miniButtonRight, GUILayout.Width(55f))) {
					IsEditingProfileName = false;
				}
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			ShowProfileDetails = EditorGUILayout.Foldout(ShowProfileDetails, GetTranslatedString("String_ProfileDetails"), true);
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			if (ShowProfileDetails) {
				using (new EditorGUI.DisabledScope(true)) {
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.TextField(GetTranslatedString("String_ReferenceColor"), TargetColorDelta.ReferenceColor);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.Vector3Field(GetTranslatedString("String_Shadow1Delta"), TargetColorDelta.ColorDelta1);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.Vector3Field(GetTranslatedString("String_Shadow2Delta"), TargetColorDelta.ColorDelta2);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.Vector3Field(GetTranslatedString("String_Shadow3Delta"), TargetColorDelta.ColorDelta3);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.Vector3Field(GetTranslatedString("String_RimLightDelta"), TargetColorDelta.RimLightDelta);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.Vector3Field(GetTranslatedString("String_RimShadeDelta"), TargetColorDelta.RimShadeDelta);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
				}
			}
		}

		void DrawTargetMaterialSection() {
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent(GetTranslatedString("String_TargetMaterials")));
			if (GUILayout.Button(GetTranslatedString("String_Apply"), EditorStyles.miniButtonLeft, GUILayout.Width(60f))) {
				ApplyToMaterials();
				Repaint();
			}
			if (GUILayout.Button(GetTranslatedString("String_Undo"), EditorStyles.miniButtonRight, GUILayout.Width(60f))) {
				Undo.PerformUndo();
				Repaint();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
	}

		void AddAvatarMaterials() {
			Material[] AvatarMaterials = AvatarUtility.GetAvatarMaterials(AvatarGameObject);
			if (AvatarMaterials == null) return;
			TargetMaterials = TargetMaterials.Concat(AvatarMaterials).Distinct().ToArray();
		}

		void ExtractMaterialColors() {
			if (!SourceMaterial || GetShaderType(SourceMaterial) != ShaderType.lilToon) return;

			if (IsPropertyActive(SourceMaterial, "_UseShadow")) {
				BaseColor = Color.white;
				ShadowColor1 = SourceMaterial.GetColor("_ShadowColor");
				Color SourceShadowColor2 = SourceMaterial.GetColor("_Shadow2ndColor");
				Color SourceShadowColor3 = SourceMaterial.GetColor("_Shadow3rdColor");
				ShadowColor2 = SourceShadowColor2.a != 0f ? SourceShadowColor2 : ShadowColor1;
				ShadowColor3 = SourceShadowColor3.a != 0f ? SourceShadowColor3 : ShadowColor2;
			} else {
				BaseColor = Color.white;
				ShadowColor1 = Color.black;
				ShadowColor2 = Color.black;
				ShadowColor3 = Color.black;
			}

			RimLightColor = IsPropertyActive(SourceMaterial, "_UseRim")
				? SourceMaterial.GetColor("_RimColor")
				: ShadowColor1;
			RimShadeColor = IsPropertyActive(SourceMaterial, "_UseRimShade")
				? SourceMaterial.GetColor("_RimShadeColor")
				: ShadowColor3;
			Repaint();
		}

		void ApplyToMaterials() {
			int UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			bool HasChanges = false;
			foreach (Material TargetMaterial in TargetMaterials ?? new Material[0]) {
				if (!TargetMaterial || GetShaderType(TargetMaterial) != ShaderType.lilToon) continue;
				bool HasMaterialChanges = false;

				if (IsPropertyActive(TargetMaterial, "_UseShadow")) {
					HasMaterialChanges |= HasColorPropertyChange(TargetMaterial, ShadowPropertyNames[0], ShadowColor1);
					HasMaterialChanges |= HasColorPropertyChange(TargetMaterial, ShadowPropertyNames[1], ShadowColor2);
					HasMaterialChanges |= HasColorPropertyChange(TargetMaterial, ShadowPropertyNames[2], ShadowColor3);
				}
				if (IsPropertyActive(TargetMaterial, "_UseRim")) {
					HasMaterialChanges |= HasColorPropertyChange(TargetMaterial, "_RimColor", RimLightColor);
				}
				if (IsPropertyActive(TargetMaterial, "_UseRimShade")) {
					HasMaterialChanges |= HasColorPropertyChange(TargetMaterial, "_RimShadeColor", RimShadeColor);
				}

				if (!HasMaterialChanges) continue;
				Undo.RecordObject(TargetMaterial, UndoGroupName);
				if (IsPropertyActive(TargetMaterial, "_UseShadow")) {
					SetColorProperty(TargetMaterial, ShadowPropertyNames[0], ShadowColor1);
					SetColorProperty(TargetMaterial, ShadowPropertyNames[1], ShadowColor2);
					SetColorProperty(TargetMaterial, ShadowPropertyNames[2], ShadowColor3);
				}
				if (IsPropertyActive(TargetMaterial, "_UseRim")) {
					SetColorProperty(TargetMaterial, "_RimColor", RimLightColor);
				}
				if (IsPropertyActive(TargetMaterial, "_UseRimShade")) {
					SetColorProperty(TargetMaterial, "_RimShadeColor", RimShadeColor);
				}
				EditorUtility.SetDirty(TargetMaterial);
				HasChanges = true;
				Debug.Log($"[Macchiato] {TargetMaterial.name} 머테리얼에 설정을 적용하였습니다");
			}
			if (HasChanges) {
				Undo.CollapseUndoOperations(UndoGroupIndex);
			}
		}

		bool HasColorPropertyChange(Material TargetMaterial, string PropertyName, Color NewColor) {
			return TargetMaterial.HasProperty(PropertyName) && TargetMaterial.GetColor(PropertyName) != NewColor;
		}

		void CreateColorDelta() {
			string NewProfileName = GetUniqueProfileName(SourceMaterial ? SourceMaterial.name : $"ColorGenerator_{Random.Range(1000, 10000)}");
			ColorDelta NewColorDelta = ColorGeneratorUtility.CreateColorDelta(
				NewProfileName,
				BaseColor,
				ShadowColor1,
				ShadowColor2,
				ShadowColor3,
				RimLightColor,
				RimShadeColor
			);
			ColorDeltaList.Add(NewColorDelta);
			SelectedColorDeltaIndex = ColorDeltaList.Count - 1;
			TargetColorDelta = NewColorDelta;
			BeginEditProfileName();
			SaveColorDelta();
			Debug.Log($"[Macchiato] {NewProfileName} 설정을 생성하였습니다");
		}

		void BeginEditProfileName() {
			if (ColorDeltaList.Count == 0) return;
			EditingProfileName = TargetColorDelta.Name;
			IsEditingProfileName = true;
		}

		void SaveEditedProfileName() {
			if (string.IsNullOrWhiteSpace(EditingProfileName)) return;
			string OldProfileName = TargetColorDelta.Name;
			string NewProfileName = GetUniqueProfileName(EditingProfileName, SelectedColorDeltaIndex);
			TargetColorDelta.Name = NewProfileName;
			ColorDeltaList[SelectedColorDeltaIndex] = TargetColorDelta;
			IsEditingProfileName = false;
			DeleteProfileFile(OldProfileName, NewProfileName);
			SaveColorDelta();
		}

		string GetUniqueProfileName(string BaseName, int IgnoreIndex = -1) {
			string SanitizedName = AssetUtility.SanitizeString(BaseName.Trim());
			if (string.IsNullOrEmpty(SanitizedName)) SanitizedName = "ColorGenerator";
			string CandidateName = SanitizedName;
			int Suffix = 2;
			while (true) {
				bool IsDuplicate = false;
				for (int Index = 0; Index < ColorDeltaList.Count; Index++) {
					if (Index != IgnoreIndex && ColorDeltaList[Index].Name == CandidateName) {
						IsDuplicate = true;
						break;
					}
				}
				if (!IsDuplicate) return CandidateName;
				CandidateName = $"{SanitizedName}_{Suffix++}";
			}
		}

		void DeleteProfileFile(string OldProfileName, string NewProfileName) {
			if (string.IsNullOrEmpty(OldProfileName) || OldProfileName == NewProfileName) return;
			string OldFilePath = Path.Combine(GetColorDeltaDirectory(), $"{AssetUtility.SanitizeString(OldProfileName)}.json");
			if (File.Exists(OldFilePath)) {
				File.Delete(OldFilePath);
				AssetDatabase.Refresh();
			}
		}

		void LoadColorDeltas() {
			string SaveDirectory = GetColorDeltaDirectory();
			if (!Directory.Exists(SaveDirectory)) {
				Directory.CreateDirectory(SaveDirectory);
			}
			ColorDeltaList.Clear();
			foreach (string JSONFile in Directory.GetFiles(SaveDirectory, "*.json").OrderBy(Item => Item)) {
				if (TryReadColorDelta(JSONFile, out ColorDelta ColorDeltaData)) {
					ColorDeltaList.Add(ColorDeltaData);
				}
			}
		}

		bool TryReadColorDelta(string JSONFilePath, out ColorDelta TargetColorDelta) {
			TargetColorDelta = default;
			try {
				string ColorDeltaJSON = File.ReadAllText(JSONFilePath);
				TargetColorDelta = JsonUtility.FromJson<ColorDelta>(ColorDeltaJSON);
				return !string.IsNullOrEmpty(TargetColorDelta.Name);
			} catch (Exception Exception) {
				Debug.LogWarning($"[Macchiato] ColorDelta 파일을 읽지 못했습니다: {JSONFilePath}\n{Exception.Message}");
				return false;
			}
		}

		void ImportColorDelta() {
			string LoadPath = EditorUtility.OpenFilePanel(
				GetTranslatedString("String_ImportColorDelta"),
				Application.dataPath,
				"json"
			);
			if (string.IsNullOrEmpty(LoadPath)) return;
			if (!TryReadColorDelta(LoadPath, out ColorDelta ImportedColorDelta)) return;
			ImportedColorDelta.Name = GetUniqueProfileName(ImportedColorDelta.Name);
			ColorDeltaList.Add(ImportedColorDelta);
			SelectedColorDeltaIndex = ColorDeltaList.Count - 1;
			TargetColorDelta = ImportedColorDelta;
			SaveColorDelta();
			Repaint();
		}

		void ExportColorDelta() {
			if (ColorDeltaList.Count == 0) return;
			string DefaultName = string.IsNullOrEmpty(TargetColorDelta.Name) ? "ColorGenerator" : TargetColorDelta.Name;
			string SavePath = EditorUtility.SaveFilePanel(
				GetTranslatedString("String_ExportColorDelta"),
				Application.dataPath,
				DefaultName,
				"json"
			);
			if (string.IsNullOrEmpty(SavePath)) return;
			File.WriteAllText(SavePath, JsonUtility.ToJson(TargetColorDelta, true));
		}

		void SaveColorDelta() {
			if (string.IsNullOrEmpty(TargetColorDelta.Name)) return;
			string SaveDirectory = GetColorDeltaDirectory();
			if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);
			string JSONFilePath = Path.Combine(SaveDirectory, $"{AssetUtility.SanitizeString(TargetColorDelta.Name)}.json");
			File.WriteAllText(JSONFilePath, JsonUtility.ToJson(TargetColorDelta, true));
			AssetDatabase.Refresh();
		}

		string GetColorDeltaDirectory() {
			return Path.Combine(Application.dataPath, ColorDeltaPath);
		}

		void CreateSampleColorDelta() {
			ColorDelta SampleColorDelta = new ColorDelta {
				Name = "Glossy",
				ReferenceColor = "#FFF0EF",
				ColorDelta1 = new Vector3(-11f, 4f, -9f),
				ColorDelta2 = new Vector3(-4f, 6f, 1f),
				ColorDelta3 = new Vector3(9f, 16f, -3f),
				RimLightDelta = new Vector3(12f, 13f, 9f),
				RimShadeDelta = new Vector3(-5f, -22f, 2f)
			};
			TargetColorDelta = SampleColorDelta;
			ColorDeltaList.Add(SampleColorDelta);
			SelectedColorDeltaIndex = 0;
			SaveColorDelta();
		}

		void SetColorDelta(int TargetIndex) {
			if (TargetIndex < 0 || TargetIndex >= ColorDeltaList.Count) return;
			SelectedColorDeltaIndex = TargetIndex;
			TargetColorDelta = ColorDeltaList[TargetIndex];
			ColorGeneratorColors GeneratedColors = ColorGeneratorUtility.GenerateColors(TargetColorDelta);
			SetColors(GeneratedColors);
		}

		void CalculateFromColor(int TargetColorIndex) {
			Color TargetColor = TargetColorIndex switch {
				0 => BaseColor,
				1 => ShadowColor1,
				2 => ShadowColor2,
				3 => ShadowColor3,
				_ => BaseColor
			};
			ColorGeneratorColors CalculatedColors = TargetColorIndex switch {
				0 => ColorGeneratorUtility.CalculateFromBaseColor(TargetColor, TargetColorDelta),
				1 => ColorGeneratorUtility.CalculateFromShadowColor1(TargetColor, TargetColorDelta),
				2 => ColorGeneratorUtility.CalculateFromShadowColor2(TargetColor, TargetColorDelta),
				3 => ColorGeneratorUtility.CalculateFromShadowColor3(TargetColor, TargetColorDelta),
				_ => ColorGeneratorUtility.GenerateColors(TargetColorDelta)
			};
			SetColors(CalculatedColors);
			Repaint();
		}

		void CalculateOklabGradient() {
			Macchiato.Core.ColorUtility.OklabGradient Gradient = Macchiato.Core.ColorUtility.GetOklabGradient(BaseColor, ShadowColor3);
			ShadowColor1 = Gradient.Shadow1;
			ShadowColor2 = Gradient.Shadow2;
			Repaint();
		}

		void SetColors(ColorGeneratorColors TargetColors) {
			BaseColor = TargetColors.BaseColor;
			ShadowColor1 = TargetColors.ShadowColor1;
			ShadowColor2 = TargetColors.ShadowColor2;
			ShadowColor3 = TargetColors.ShadowColor3;
			RimLightColor = TargetColors.RimLightColor;
			RimShadeColor = TargetColors.RimShadeColor;
		}

		Color AddColor(Color Base, Color Add) {
			return new Color(
				Mathf.Clamp01(Base.r + Add.r * Add.a),
				Mathf.Clamp01(Base.g + Add.g * Add.a),
				Mathf.Clamp01(Base.b + Add.b * Add.a),
				Base.a
			);
		}

		Color MultiplyColor(Color Base, Color Multiply) {
			return new Color(
				Base.r * Multiply.r * Multiply.a,
				Base.g * Multiply.g * Multiply.a,
				Base.b * Multiply.b * Multiply.a,
				Base.a
			);
		}
	}
}
