using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

using Macchiato.Core;
using static Macchiato.Core.Translator;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class MaterialTemplate : EditorWindow {

		public GameObject AvatarGameObject;
		public Material ReferenceMaterial;
		public Material[] TargetMaterials;

		public lilToonTemplateOption TargetlilToonOption;
		public poiyomiTemplateOption TargetpoiyomiOption;
		public UTSTemplateOption TargetUTSOption;
		public GeneralTemplateOption TargetGeneralOption;

		const string UndoGroupName = "Macchiato MaterialTemplate";
		int UndoGroupIndex = -1;
		List<Material> ModifiedMaterials = new List<Material>();

		enum ShaderType {
			Unknown,
			lilToon,
			poiyomi,
			UnityChanToonShader
		}

		SerializedObject SerializedMaterialTemplate;
		SerializedProperty SerializedAvatarGameObject;
		SerializedProperty SerializedReferenceMaterial;
		SerializedProperty SerializedTargetMaterials;

		SerializedProperty SerializedlilToonOption;
		SerializedProperty SerializedpoiyomiOption;
		SerializedProperty SerializedUTSOption;
		SerializedProperty SerializedGeneralOption;

		bool FoldlilToon;
		bool Foldpoiyomi;
		bool FoldUnityChanToonShader;
		bool FoldGeneral;

		Vector2 ScrollPosition;
		const float BorderX = 30f;
		const float ToggleControlWidth = 45f;
		const float ColorLabelRatio = 0.5f;

		void OnEnable() {
			SerializedMaterialTemplate = new SerializedObject(this);
			SerializedAvatarGameObject = SerializedMaterialTemplate.FindProperty(nameof(AvatarGameObject));
			SerializedReferenceMaterial = SerializedMaterialTemplate.FindProperty(nameof(ReferenceMaterial));
			SerializedTargetMaterials = SerializedMaterialTemplate.FindProperty(nameof(TargetMaterials));
			SerializedlilToonOption = SerializedMaterialTemplate.FindProperty(nameof(TargetlilToonOption));
			SerializedpoiyomiOption = SerializedMaterialTemplate.FindProperty(nameof(TargetpoiyomiOption));
			SerializedUTSOption = SerializedMaterialTemplate.FindProperty(nameof(TargetUTSOption));
			SerializedGeneralOption = SerializedMaterialTemplate.FindProperty(nameof(TargetGeneralOption));
		}

		[MenuItem("Tools/Macchiato/Utility/MaterialTemplate", priority = 1000)]
		static void CreateWindow() {
			MaterialTemplate AppWindow = GetWindowWithRect<MaterialTemplate>(new Rect(0, 0, 450, 685), true, "Macchiato MaterialTemplate");
			AppWindow.Initialize();
		}

		void Initialize() {
			AvatarGameObject = AvatarUtility.GetAvatarGameObject();
			TargetMaterials = new Material[0];
			TargetlilToonOption = new lilToonTemplateOption();
			TargetpoiyomiOption = new poiyomiTemplateOption();
			TargetUTSOption = new UTSTemplateOption();
			TargetGeneralOption = new GeneralTemplateOption();
			FoldlilToon = true;
			Foldpoiyomi = true;
			FoldUnityChanToonShader = true;
			FoldGeneral = true;
		}

		void OnGUI() {
			if (SerializedMaterialTemplate == null || !SerializedMaterialTemplate.targetObject) {
				Initialize();
				if (SerializedMaterialTemplate == null) {
					Close();
					return;
				}
			}
			SerializedMaterialTemplate.Update();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			DrawHeaderSection();
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition, GUILayout.Height(400f));
			DrawlilToonSection();
			DrawUTSSection();
			DrawGeneralSection();
			EditorGUILayout.EndScrollView();
			SerializedMaterialTemplate.ApplyModifiedPropertiesWithoutUndo();
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUI.backgroundColor = Color.cyan;
			GUI.enabled = IsReadyToUpdate();
			if (GUILayout.Button(GetTranslatedString("String_Update"), GUILayout.Height(40f))) {
				UpdateMaterialProperties();
				Repaint();
			}
			GUI.enabled = true;
			GUI.backgroundColor = Color.white;
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUI.enabled = IsReadyToRevert();
			if (GUILayout.Button(GetTranslatedString("String_Undo"))) {
				RevertMaterialProperties();
				Repaint();
			}
			GUI.enabled = true;
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
		}

		void DrawHeaderSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUIUtility.labelWidth = 125f;
			LanguageIndex = EditorGUILayout.Popup(GetTranslatedString("String_Language"), LanguageIndex, LanguageOption);
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedAvatarGameObject, new GUIContent(GetTranslatedString("String_Avatar")));
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(SerializedReferenceMaterial, new GUIContent(GetTranslatedString("String_ReferenceMaterial")));
			if (EditorGUI.EndChangeCheck()) {
				SerializedMaterialTemplate.ApplyModifiedPropertiesWithoutUndo();
				UpdateMaterialColors();
				SerializedMaterialTemplate.Update();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent(GetTranslatedString("String_Material")));
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			if (GUILayout.Button(GetTranslatedString("String_GetAvatarMaterials"))) {
				AddAvatarMaterials();
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
		}

		void DrawlilToonSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			FoldlilToon = EditorGUILayout.Foldout(FoldlilToon, "lilToon");
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			if (FoldlilToon) {
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonBasic), "String_UpdatelilToonBasic");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonLighting), "String_UpdatelilToonLighting");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonShadow), "String_UpdatelilToonShadow");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonReceiveShadow), "String_UpdatelilToonReceiveShadow");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonBackfaceMask), "String_UpdatelilToonBackfaceMask");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonBacklight), "String_UpdatelilToonBacklight");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForcelilToonShadow), "String_ForcelilToonShadow");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForcelilToonRimShade), "String_ForcelilToonRimShade");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForcelilToonBacklight), "String_ForcelilToonBacklight");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForcelilToonReflection), "String_ForcelilToonReflection");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForcelilToonRimLight), "String_ForcelilToonRimLight");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonShadowColor), "String_UpdatelilToonShadowColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetShadow1Color), "String_TargetShadow1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetShadow2Color), "String_TargetShadow2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetShadow3Color), "String_TargetShadow3Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetShadowBorderColor), "String_TargetShadowBorderColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonRimShadeColor), "String_UpdatelilToonRimShadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetRimShadeColor), "String_TargetRimShadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonBacklightColor), "String_UpdatelilToonBacklightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetBacklightColor), "String_TargetBacklightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonReflectionColor), "String_UpdatelilToonReflectionColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetReflectionColor), "String_TargetReflectionColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonRimLightColor), "String_UpdatelilToonRimLightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetRimLightColor), "String_TargetRimLightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdatelilToonOutlineColor), "String_UpdatelilToonOutlineColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetOutlineColor), "String_TargetOutlineColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.TargetOutlineHighlightColor), "String_TargetOutlineHighlightColor");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
			}
		}

		void DrawUTSSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			FoldUnityChanToonShader = EditorGUILayout.Foldout(FoldUnityChanToonShader, "UnityChanToonShader");
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			if (FoldUnityChanToonShader) {
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedUTSOption, nameof(TargetUTSOption.UpdateUTSTextureShared), "String_UpdateUTSTextureShared");
					DrawProperty(SerializedUTSOption, nameof(TargetUTSOption.UpdateUTSNormalMap), "String_UpdateUTSNormalMap");
					DrawProperty(SerializedUTSOption, nameof(TargetUTSOption.UpdateUTSBasicShading), "String_UpdateUTSBasicShading");
					DrawProperty(SerializedUTSOption, nameof(TargetUTSOption.UpdateUTSLightColor), "String_UpdateUTSLightColor");
					DrawProperty(SerializedUTSOption, nameof(TargetUTSOption.UpdateUTSEnvironmentalLightingProperties), "String_UpdateUTSEnvironmentalLightingProperties");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
			}
		}

		void DrawGeneralSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			FoldGeneral = EditorGUILayout.Foldout(FoldGeneral, GetTranslatedString("String_General"));
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			if (FoldGeneral) {
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedGeneralOption, nameof(TargetGeneralOption.UpdateRenderQueue), "String_RenderQueue");
					DrawProperty(SerializedGeneralOption, nameof(TargetGeneralOption.UpdateGPUInstancing), "String_GPUInstancing");
					DrawProperty(SerializedGeneralOption, nameof(TargetGeneralOption.UpdateGlobalIllumination), "String_GlobalIllumination");
				}
				EditorGUI.indentLevel--;
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
			}
		}

		void DrawProperty(SerializedProperty ParentProperty, string TargetPropertyName, string TargetString) {
			SerializedProperty TargetProperty = ParentProperty.FindPropertyRelative(TargetPropertyName);
			float OriginalLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = GetLabelWidth(TargetProperty);
			EditorGUILayout.PropertyField(TargetProperty, new GUIContent(GetTranslatedString(TargetString)));
			EditorGUIUtility.labelWidth = OriginalLabelWidth;
		}

		float GetLabelWidth(SerializedProperty TargetProperty) {
			float ContentWidth = position.width - (BorderX * 2f);
			if (TargetProperty.propertyType == SerializedPropertyType.Boolean) return ContentWidth - ToggleControlWidth;
			return ContentWidth * ColorLabelRatio;
		}

		bool IsReadyToUpdate() {
			return TargetMaterials.Length > 0;
		}

		public bool UpdateMaterialProperties() {
			int NewUndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			List<Material> NewModifiedMaterials = new List<Material>();
			lilToonTemplate lilToonTemplateInstance = new lilToonTemplate(TargetlilToonOption, ReferenceMaterial);
			poiyomiTemplate poiyomiTemplateInstance = new poiyomiTemplate(TargetpoiyomiOption, ReferenceMaterial);
			UTSTemplate UTSTemplateInstance = new UTSTemplate(TargetUTSOption, ReferenceMaterial);
			GeneralTemplate GeneralTemplateInstance = new GeneralTemplate(TargetGeneralOption, ReferenceMaterial);
			foreach (Material TargetMaterial in TargetMaterials) {
				if (!TargetMaterial) continue;
				Undo.RecordObject(TargetMaterial, UndoGroupName);
				bool IsModified = false;
				switch (GetShaderType(TargetMaterial)) {
					case ShaderType.lilToon:
						IsModified = lilToonTemplateInstance.UpdatelilToonProperties(TargetMaterial);
						break;
					case ShaderType.poiyomi:
						IsModified = poiyomiTemplateInstance.UpdatepoiyomiProperties(TargetMaterial);
						break;
					case ShaderType.UnityChanToonShader:
						IsModified = UTSTemplateInstance.UpdateUnityChanToonShaderProperties(TargetMaterial);
						break;
					default:
						Debug.LogError(string.Format(GetTranslatedString("NOT_SUPPORT_SHADER"), TargetMaterial.shader.name));
						break;
				}
				if (GeneralTemplateInstance.UpdateGeneralProperties(TargetMaterial)) IsModified = true;
				if (IsModified) NewModifiedMaterials.Add(TargetMaterial);
			}
			if (NewModifiedMaterials.Count == 0) return false;
			Undo.FlushUndoRecordObjects();
			Undo.CollapseUndoOperations(NewUndoGroupIndex);
			foreach (Material TargetMaterial in NewModifiedMaterials) {
				AssetDatabase.SaveAssetIfDirty(TargetMaterial);
			}
			UndoGroupIndex = NewUndoGroupIndex;
			ModifiedMaterials = NewModifiedMaterials;
			return true;
		}

		void AddAvatarMaterials() {
			Material[] AvatarMaterials = AvatarUtility.GetAvatarMaterials(AvatarGameObject);
			TargetMaterials = TargetMaterials.Concat(AvatarMaterials).Distinct().ToArray();
		}

		ShaderType GetShaderType(Material TargetMaterial) {
			string TargetShaderName = TargetMaterial.shader.name;
			if (TargetShaderName.Contains("lilToon", StringComparison.OrdinalIgnoreCase)) return ShaderType.lilToon;
			if (TargetShaderName.Contains("poiyomi", StringComparison.OrdinalIgnoreCase)) return ShaderType.poiyomi;
			if (TargetShaderName.Contains("UnityChanToonShader", StringComparison.OrdinalIgnoreCase)) return ShaderType.UnityChanToonShader;
			return ShaderType.Unknown;
		}

		void UpdateMaterialColors() {
			if (ReferenceMaterial) {
				switch (GetShaderType(ReferenceMaterial)) {
					case ShaderType.lilToon:
						TargetlilToonOption.GetlilToonColors(ReferenceMaterial);
						break;
					case ShaderType.poiyomi:
						break;
					case ShaderType.UnityChanToonShader:
						break;
				}
			}
		}

		bool IsReadyToRevert() {
			return UndoGroupIndex >= 0 && ModifiedMaterials.Count > 0;
		}

		public bool RevertMaterialProperties() {
			if (!IsReadyToRevert()) return false;
			Undo.RevertAllDownToGroup(UndoGroupIndex);
			foreach (Material TargetMaterial in ModifiedMaterials) {
				if (!TargetMaterial) continue;
				EditorUtility.SetDirty(TargetMaterial);
				AssetDatabase.SaveAssetIfDirty(TargetMaterial);
			}
			UndoGroupIndex = -1;
			ModifiedMaterials.Clear();
			return true;
		}
	}
}
