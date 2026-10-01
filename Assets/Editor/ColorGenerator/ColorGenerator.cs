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

		[SerializeField] GameObject AvatarGameObject;
		[SerializeField] Material SourceMaterial;
		[SerializeField] Material[] TargetMaterials = new Material[0];

		[SerializeField] ColorProfile TargetColorProfile;

		[SerializeField, ColorUsage(false, false)]  Color BaseColor = Color.white;
		[SerializeField, ColorUsage(false, false)] Color ShadowColor1 = Color.black;
		[SerializeField, ColorUsage(true, false)] Color ShadowColor2 = Color.black;
		[SerializeField, ColorUsage(true, false)] Color ShadowColor3 = Color.black;
		[SerializeField, ColorUsage(true, true)] Color RimLightColor = Color.white;
		[SerializeField, ColorUsage(true, false)] Color RimShadeColor = Color.black;

		SerializedObject SerializedColorGenerator;
		SerializedProperty SerializedAvatarGameObject;
		SerializedProperty SerializedSourceMaterial;
		SerializedProperty SerializedTargetMaterials;

		const string ColorProfilePath = "Caramel_Macchiato/ColorGenerator";
		const string UndoGroupName = "Macchiato ColorGenerator";

		int UndoGroupIndex = -1;
		List<Material> ModifiedMaterials = new List<Material>();

		static readonly string[] ShadowPropertyNames = new string[] {
			"_ShadowColor",
			"_Shadow2ndColor",
			"_Shadow3rdColor"
		};

		static List<ColorProfile> ColorProfiles = new List<ColorProfile>();
		int TargetProfileIndex;
		bool IsEditingProfile = false;
		string NewProfileName = string.Empty;
		bool FoldProfileDetail = false;

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
			AppWindow.Initialize();
		}

		void Initialize() {
			AvatarGameObject = AvatarUtility.GetAvatarGameObject();
			SerializedColorGenerator = new SerializedObject(this);
			SerializedAvatarGameObject = SerializedColorGenerator.FindProperty(nameof(AvatarGameObject));
			SerializedSourceMaterial = SerializedColorGenerator.FindProperty(nameof(SourceMaterial));
			SerializedTargetMaterials = SerializedColorGenerator.FindProperty(nameof(TargetMaterials));
			LoadColorProfiles();
			if (ColorProfiles.Count > 0) {
				TargetProfileIndex = 0;
				SetColorProfile(TargetProfileIndex);
			} else {
				CreateSampleColorProfile();
			}
		}

		void OnGUI() {
			if (SerializedColorGenerator == null || !SerializedColorGenerator.targetObject) {
				Initialize();
				if (SerializedColorGenerator == null) {
					Close();
					return;
				}
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
			DrawMaterialSection();
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
			EditorGUILayout.PropertyField(SerializedAvatarGameObject, new GUIContent(GetTranslatedString("String_Avatar")));
			if (GUILayout.Button(GetTranslatedString("String_Extract"), GUILayout.Width(70f))) {
				AddAvatarMaterials();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedSourceMaterial, new GUIContent(GetTranslatedString("String_SourceMaterial")));
			if (GUILayout.Button(GetTranslatedString("String_Extract"), GUILayout.Width(70f))) {
				ExtractMaterialColors();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
		}

		void DrawColorPreviewSection() {
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			Rect ColorRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(70f));
			Rect RimColorRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(30f));
			float ColumnWidth = ColorRect.width / 4f;
			Color RimLightPreview = AddColor(BaseColor, RimLightColor);
			Color RimLightShadowPreview = AddColor(ShadowColor1, RimLightColor);
			Color RimShadePreview = MultiplyColor(ShadowColor2, RimShadeColor);
			Color RimShadeShadowPreview = MultiplyColor(ShadowColor3, RimShadeColor);
			EditorGUI.DrawRect(new Rect(ColorRect.x, ColorRect.y, ColorRect.width, ColorRect.height), BaseColor);
			EditorGUI.DrawRect(new Rect(ColorRect.x + ColumnWidth, ColorRect.y, ColumnWidth, ColorRect.height), ShadowColor1);
			EditorGUI.DrawRect(new Rect(ColorRect.x + ColumnWidth * 2f, ColorRect.y, ColumnWidth, ColorRect.height), ShadowColor2);
			EditorGUI.DrawRect(new Rect(ColorRect.x + ColumnWidth * 3f, ColorRect.y, ColumnWidth, ColorRect.height), ShadowColor3);
			EditorGUI.DrawRect(new Rect(RimColorRect.x, RimColorRect.y, RimColorRect.width / 4f, RimColorRect.height), RimLightPreview);
			EditorGUI.DrawRect(new Rect(RimColorRect.x + RimColorRect.width / 4f, RimColorRect.y, RimColorRect.width / 4f, RimColorRect.height), RimLightShadowPreview);
			EditorGUI.DrawRect(new Rect(RimColorRect.x + RimColorRect.width / 2f, RimColorRect.y, RimColorRect.width / 4f, RimColorRect.height), RimShadePreview);
			EditorGUI.DrawRect(new Rect(RimColorRect.x + RimColorRect.width * 0.75f, RimColorRect.y, RimColorRect.width / 4f, RimColorRect.height), RimShadeShadowPreview);
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
			DrawColorField(BaseColor, Value => BaseColor = Value, false, Column4Width);
			DrawColorField(ShadowColor1, Value => ShadowColor1 = Value, false, Column4Width);
			DrawColorField(ShadowColor2, Value => ShadowColor2 = Value, false, Column4Width);
			DrawColorField(ShadowColor3, Value => ShadowColor3 = Value, false, Column4Width);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			DrawColorField(RimLightColor, Value => RimLightColor = Value, true, Column2Width);
			DrawColorField(RimShadeColor, Value => RimShadeColor = Value, false, Column2Width);
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
			string[] ProfileNameList = ColorProfiles.Select(Item => Item.Name).ToArray();
			if (ProfileNameList.Length > 0) {
				int NewSelectedProfileIndex = EditorGUILayout.Popup(TargetProfileIndex, ProfileNameList);
				if (NewSelectedProfileIndex != TargetProfileIndex) {
					SetColorProfile(NewSelectedProfileIndex);
				}
			}
			if (GUILayout.Button(GetTranslatedString("String_Create"), EditorStyles.miniButtonLeft, GUILayout.Width(55f))) {
				CreateColorDelta();
			}
			if (GUILayout.Button(GetTranslatedString("String_Edit"), EditorStyles.miniButtonMid, GUILayout.Width(55f))) {
				EditProfile();
			}
			if (GUILayout.Button(GetTranslatedString("String_Import"), EditorStyles.miniButtonMid, GUILayout.Width(55f))) {
				ImportColorProfile();
			}
			if (GUILayout.Button(GetTranslatedString("String_Export"), EditorStyles.miniButtonRight, GUILayout.Width(55f))) {
				ExportColorProfile();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			if (IsEditingProfile) {
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				NewProfileName = EditorGUILayout.TextField(NewProfileName);
				if (GUILayout.Button(GetTranslatedString("String_Save"), EditorStyles.miniButtonLeft, GUILayout.Width(55f))) {
					SaveEditedProfile();
				}
				if (GUILayout.Button(GetTranslatedString("String_Cancel"), EditorStyles.miniButtonRight, GUILayout.Width(55f))) {
					IsEditingProfile = false;
				}
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			FoldProfileDetail = EditorGUILayout.Foldout(FoldProfileDetail, GetTranslatedString("String_ProfileDetails"), true);
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			if (FoldProfileDetail) {
				using (new EditorGUI.DisabledScope(true)) {
					GUILayout.BeginHorizontal();
					GUILayout.Space(BorderX);
					EditorGUILayout.TextField(GetTranslatedString("String_ReferenceColor"), TargetColorProfile.ReferenceColor);
					GUILayout.Space(BorderX);
					GUILayout.EndHorizontal();
					DrawColorDeltaField(GetTranslatedString("String_Shadow1Delta"), TargetColorProfile.ColorDelta1);
					DrawColorDeltaField(GetTranslatedString("String_Shadow2Delta"), TargetColorProfile.ColorDelta2);
					DrawColorDeltaField(GetTranslatedString("String_Shadow3Delta"), TargetColorProfile.ColorDelta3);
					DrawColorDeltaField(GetTranslatedString("String_RimLightDelta"), TargetColorProfile.RimLightDelta);
					DrawColorDeltaField(GetTranslatedString("String_RimShadeDelta"), TargetColorProfile.RimShadeDelta);
				}
			}
		}

		void DrawMaterialSection() {
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent(GetTranslatedString("String_TargetMaterials")));
			if (GUILayout.Button(GetTranslatedString("String_Apply"), EditorStyles.miniButtonLeft, GUILayout.Width(60f))) {
				UpdateMaterials();
				Repaint();
			}
			if (GUILayout.Button(GetTranslatedString("String_Undo"), EditorStyles.miniButtonRight, GUILayout.Width(60f))) {
				RevertMaterialProperties();
				Repaint();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
	}

		void DrawColorField(Color TargetColor, Action<Color> SetColor, bool HDR, float TargetWidth) {
			Color NewColor = EditorGUILayout.ColorField(GUIContent.none, TargetColor, true, true, HDR, GUILayout.Width(TargetWidth));
			if (NewColor != TargetColor) SetColor(NewColor);
		}

		void DrawColorDeltaField(string TargetColor, Vector3 TargetValue) {
			float LabelWidth = 15f;
			float FloatWidth = 106f;
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label(TargetColor, GUILayout.Width(100f));
			GUILayout.Label("H", GUILayout.Width(LabelWidth));
			TargetValue.x = EditorGUILayout.FloatField(TargetValue.x, GUILayout.Width(FloatWidth));
			GUILayout.Label("S", GUILayout.Width(LabelWidth));
			TargetValue.y = EditorGUILayout.FloatField(TargetValue.y, GUILayout.Width(FloatWidth));
			GUILayout.Label("V", GUILayout.Width(LabelWidth));
			TargetValue.z = EditorGUILayout.FloatField(TargetValue.z, GUILayout.Width(FloatWidth));
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
				Color SourceShadowColor1 = SourceMaterial.GetColor("_ShadowColor");
				Color SourceShadowColor2 = SourceMaterial.GetColor("_Shadow2ndColor");
				Color SourceShadowColor3 = SourceMaterial.GetColor("_Shadow3rdColor");
				ShadowColor1 = SourceShadowColor1;
				ShadowColor2 = SourceShadowColor2.a != 0f ? SourceShadowColor2 : ShadowColor1;
				ShadowColor3 = SourceShadowColor3.a != 0f ? SourceShadowColor3 : ShadowColor2;
			}
			if (IsPropertyActive(SourceMaterial, "_UseRim")) {
				RimLightColor = SourceMaterial.GetColor("_RimColor");
			}
			if (IsPropertyActive(SourceMaterial, "_UseRimShade")) {
				RimShadeColor = SourceMaterial.GetColor("_RimShadeColor");
			}
			Repaint();
		}

		bool UpdateMaterials() {
			int NewUndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			bool IsModified = false;
			int ModifiedCount = 0;
			List<Material> NewModifiedMaterials = new List<Material>();
			foreach (Material TargetMaterial in TargetMaterials) {
				if (!TargetMaterial || GetShaderType(TargetMaterial) != ShaderType.lilToon) continue;
				bool NeedUpdate = false;
				NeedUpdate |= NeedColorUpdate(TargetMaterial, ShadowPropertyNames[0], ShadowColor1);
				NeedUpdate |= NeedColorUpdate(TargetMaterial, ShadowPropertyNames[1], ShadowColor2);
				NeedUpdate |= NeedColorUpdate(TargetMaterial, ShadowPropertyNames[2], ShadowColor3);
				NeedUpdate |= NeedColorUpdate(TargetMaterial, "_RimColor", RimLightColor);
				NeedUpdate |= NeedColorUpdate(TargetMaterial, "_RimShadeColor", RimShadeColor);
				if (!NeedUpdate) continue;
				Undo.RecordObject(TargetMaterial, UndoGroupName);
				SetColorProperty(TargetMaterial, ShadowPropertyNames[0], ShadowColor1);
				SetColorProperty(TargetMaterial, ShadowPropertyNames[1], ShadowColor2);
				SetColorProperty(TargetMaterial, ShadowPropertyNames[2], ShadowColor3);
				SetColorProperty(TargetMaterial, "_RimColor", RimLightColor);
				SetColorProperty(TargetMaterial, "_RimShadeColor", RimShadeColor);
				EditorUtility.SetDirty(TargetMaterial);
				NewModifiedMaterials.Add(TargetMaterial);
				IsModified = true;
				ModifiedCount++;
			}
			Debug.Log($"[Macchiato] {string.Format(GetTranslatedString("COMPLETED_UPDATEMATERIAL"), ModifiedCount)}");
			if (IsModified) {
				Undo.FlushUndoRecordObjects();
				Undo.CollapseUndoOperations(NewUndoGroupIndex);
				AssetDatabase.SaveAssets();
				UndoGroupIndex = NewUndoGroupIndex;
				ModifiedMaterials = NewModifiedMaterials;
				return true;
			}
			return false;
		}

		bool RevertMaterialProperties() {
			if (UndoGroupIndex != -1) return false;
			Undo.RevertAllDownToGroup(UndoGroupIndex);
			foreach (Material TargetMaterial in ModifiedMaterials) {
				if (!TargetMaterial) continue;
				EditorUtility.SetDirty(TargetMaterial);
			}
			AssetDatabase.SaveAssets();
			UndoGroupIndex = -1;
			ModifiedMaterials.Clear();
			return true;
		}

		bool NeedColorUpdate(Material TargetMaterial, string PropertyName, Color NewColor) {
			return TargetMaterial.HasProperty(PropertyName) && TargetMaterial.GetColor(PropertyName) != NewColor;
		}

		void CreateColorDelta() {
			string NewProfileName = GetProfileName(SourceMaterial ? SourceMaterial.name : $"ColorGenerator_{Random.Range(1000, 10000)}");
			ColorProfile NewColorProfile = ColorGeneratorUtility.CreateColorProfile(
				NewProfileName,
				BaseColor,
				ShadowColor1,
				ShadowColor2,
				ShadowColor3,
				RimLightColor,
				RimShadeColor
			);
			ColorProfiles.Add(NewColorProfile);
			TargetProfileIndex = ColorProfiles.Count - 1;
			TargetColorProfile = NewColorProfile;
			EditProfile();
			SaveColorProfile();
		}

		void EditProfile() {
			if (ColorProfiles.Count == 0) return;
			NewProfileName = TargetColorProfile.Name;
			IsEditingProfile = true;
		}

		void SaveEditedProfile() {
			if (string.IsNullOrWhiteSpace(this.NewProfileName)) return;
			string OldProfileName = TargetColorProfile.Name;
			string NewProfileName = GetProfileName(this.NewProfileName, TargetProfileIndex);
			TargetColorProfile.Name = NewProfileName;
			ColorProfiles[TargetProfileIndex] = TargetColorProfile;
			IsEditingProfile = false;
			DeleteColorProfile(OldProfileName, NewProfileName);
			SaveColorProfile();
		}

		string GetProfileName(string TargetName, int IgnoreIndex = -1) {
			string NewProfileName = AssetUtility.SanitizeString(TargetName.Trim());
			if (string.IsNullOrEmpty(NewProfileName)) NewProfileName = "ColorGenerator";
			string NewFileName = NewProfileName;
			int Suffix = 2;
			while (true) {
				bool IsDuplicate = false;
				for (int Index = 0; Index < ColorProfiles.Count; Index++) {
					if (Index != IgnoreIndex && ColorProfiles[Index].Name == NewFileName) {
						IsDuplicate = true;
						break;
					}
				}
				if (!IsDuplicate) return NewFileName;
				NewFileName = $"{NewProfileName}_{Suffix++}";
			}
		}

		void DeleteColorProfile(string OldProfileName, string NewProfileName) {
			if (string.IsNullOrEmpty(OldProfileName) || OldProfileName == NewProfileName) return;
			string OldFilePath = Path.Combine(GetColorProfileDirectory(), $"{AssetUtility.SanitizeString(OldProfileName)}.json");
			if (File.Exists(OldFilePath)) File.Delete(OldFilePath);
			if (File.Exists($"{OldFilePath}.meta")) File.Delete($"{OldFilePath}.meta");
			AssetDatabase.Refresh();
		}

		void LoadColorProfiles() {
			string ColorProfilePath = GetColorProfileDirectory();
			if (!Directory.Exists(ColorProfilePath)) {
				Directory.CreateDirectory(ColorProfilePath);
			}
			ColorProfiles.Clear();
			foreach (string TargetJSON in Directory.GetFiles(ColorProfilePath, "*.json").OrderBy(Item => Item)) {
				if (TryLoadColorProfile(TargetJSON, out ColorProfile TargetColorProfile)) {
					ColorProfiles.Add(TargetColorProfile);
				}
			}
		}

		bool TryLoadColorProfile(string JSONFilePath, out ColorProfile TargetColorProfile) {
			TargetColorProfile = default;
			try {
				string ColorDeltaJSON = File.ReadAllText(JSONFilePath);
				TargetColorProfile = JsonUtility.FromJson<ColorProfile>(ColorDeltaJSON);
				return !string.IsNullOrEmpty(TargetColorProfile.Name);
			} catch {
				return false;
			}
		}

		void ImportColorProfile() {
			string TargetJSONPath = EditorUtility.OpenFilePanel(
				UndoGroupName,
				Application.dataPath,
				"json"
			);
			if (string.IsNullOrEmpty(TargetJSONPath)) return;
			if (!TryLoadColorProfile(TargetJSONPath, out ColorProfile ImportedColorDelta)) return;
			ImportedColorDelta.Name = GetProfileName(ImportedColorDelta.Name);
			ColorProfiles.Add(ImportedColorDelta);
			TargetProfileIndex = ColorProfiles.Count - 1;
			TargetColorProfile = ImportedColorDelta;
			SaveColorProfile();
			Repaint();
		}

		void ExportColorProfile() {
			if (ColorProfiles.Count == 0) return;
			string TargetName = string.IsNullOrEmpty(TargetColorProfile.Name) ? "ColorGenerator" : TargetColorProfile.Name;
			string TargetPath = EditorUtility.SaveFilePanel(
				UndoGroupName,
				Application.dataPath,
				TargetName,
				"json"
			);
			if (string.IsNullOrEmpty(TargetPath)) return;
			File.WriteAllText(TargetPath, JsonUtility.ToJson(TargetColorProfile, true));
		}

		void SaveColorProfile() {
			if (string.IsNullOrEmpty(TargetColorProfile.Name)) return;
			string ColorProfilePath = GetColorProfileDirectory();
			if (!Directory.Exists(ColorProfilePath)) Directory.CreateDirectory(ColorProfilePath);
			string TargetPath = Path.Combine(ColorProfilePath, $"{AssetUtility.SanitizeString(TargetColorProfile.Name)}.json");
			File.WriteAllText(TargetPath, JsonUtility.ToJson(TargetColorProfile, true));
			AssetDatabase.Refresh();
		}

		string GetColorProfileDirectory() {
			return Path.Combine(Application.dataPath, ColorProfilePath);
		}

		void CreateSampleColorProfile() {
			ColorProfile NewColorDelta = new ColorProfile {
				Name = "Sample",
				ReferenceColor = "#FFF0EF",
				ColorDelta1 = new Vector3(-11f, 4f, -9f),
				ColorDelta2 = new Vector3(-4f, 6f, 1f),
				ColorDelta3 = new Vector3(9f, 16f, -3f),
				RimLightDelta = new Vector3(12f, 13f, 9f),
				RimShadeDelta = new Vector3(-5f, -22f, 2f)
			};
			TargetColorProfile = NewColorDelta;
			ColorProfiles.Add(NewColorDelta);
			TargetProfileIndex = 0;
			SaveColorProfile();
			SetColorProfile(TargetProfileIndex);
		}

		void SetColorProfile(int TargetIndex) {
			if (TargetIndex < 0 || TargetIndex >= ColorProfiles.Count) return;
			TargetProfileIndex = TargetIndex;
			TargetColorProfile = ColorProfiles[TargetIndex];
			ColorProfileColor NewColorProfileColor = ColorGeneratorUtility.CalculateColors(TargetColorProfile);
			SetColors(NewColorProfileColor);
		}

		void CalculateFromColor(int TargetColorIndex) {
			Color TargetColor = TargetColorIndex switch {
				0 => BaseColor,
				1 => ShadowColor1,
				2 => ShadowColor2,
				3 => ShadowColor3,
				_ => BaseColor
			};
			ColorProfileColor NewColorProfileColor = TargetColorIndex switch {
				0 => ColorGeneratorUtility.CalculateFromBaseColor(TargetColor, TargetColorProfile),
				1 => ColorGeneratorUtility.CalculateFromShadowColor1(TargetColor, TargetColorProfile),
				2 => ColorGeneratorUtility.CalculateFromShadowColor2(TargetColor, TargetColorProfile),
				3 => ColorGeneratorUtility.CalculateFromShadowColor3(TargetColor, TargetColorProfile),
				_ => ColorGeneratorUtility.CalculateColors(TargetColorProfile)
			};
			SetColors(NewColorProfileColor);
			Repaint();
		}

		void CalculateOklabGradient() {
			ColorHelper.OklabGradient NewGradient = ColorHelper.GetOklabGradient(BaseColor, ShadowColor3);
			ShadowColor1 = NewGradient.Shadow1;
			ShadowColor2 = NewGradient.Shadow2;
			Repaint();
		}

		void SetColors(ColorProfileColor TargetColorProfileColor) {
			BaseColor = TargetColorProfileColor.BaseColor;
			ShadowColor1 = TargetColorProfileColor.ShadowColor1;
			ShadowColor2 = TargetColorProfileColor.ShadowColor2;
			ShadowColor3 = TargetColorProfileColor.ShadowColor3;
			RimLightColor = TargetColorProfileColor.RimLightColor;
			RimShadeColor = TargetColorProfileColor.RimShadeColor;
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
			float Alpha = Multiply.a;
			return new Color(
				Base.r * Mathf.Lerp(1f, Multiply.r, Alpha),
				Base.g * Mathf.Lerp(1f, Multiply.g, Alpha),
				Base.b * Mathf.Lerp(1f, Multiply.b, Alpha),
				Base.a
			);
		}
	}
}
