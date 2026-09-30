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
			DrawButtonSection();
			DrawlilToonSection();
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

		void DrawButtonSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			if (GUILayout.Button(GetTranslatedString("String_Common"), EditorStyles.miniButtonLeft)) {
				TargetlilToonOption.SelectCommon();
				TargetGeneralOption.SelectCommon();
				SerializedMaterialTemplate.Update();
			}
			if (GUILayout.Button(GetTranslatedString("String_Macchiato"), EditorStyles.miniButtonMid)) {
				TargetlilToonOption.SelectMacchiato();
				TargetGeneralOption.SelectMacchiato();
				SerializedMaterialTemplate.Update();
			}
			if (GUILayout.Button(GetTranslatedString("String_DeepCopy"), EditorStyles.miniButtonMid)) {
				TargetlilToonOption.SelectDeepCopy();
				TargetGeneralOption.SelectDeepCopy();
				SerializedMaterialTemplate.Update();
			}
			if (GUILayout.Button(GetTranslatedString("String_None"), EditorStyles.miniButtonRight)) {
				TargetlilToonOption.SelectNone();
				TargetGeneralOption.SelectNone();
				SerializedMaterialTemplate.Update();
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
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateBasic), "String_UpdatelilToonBasic");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateLighting), "String_UpdatelilToonLighting");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateAlpha), "String_UpdatelilToonAlpha");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateShadow), "String_UpdatelilToonShadow");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateRimShade), "String_UpdatelilToonRimShade");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateEmission1), "String_UpdatelilToonEmission1");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateEmission2), "String_UpdatelilToonEmission2");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateAnisotropy), "String_UpdatelilToonAnisotropy");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateBacklight), "String_UpdatelilToonBacklight");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateReflection), "String_UpdatelilToonReflection");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateMatCap1), "String_UpdatelilToonMatCap1");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateMatCap2), "String_UpdatelilToonMatCap2");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateRimLight), "String_UpdatelilToonRimLight");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateGlitter), "String_UpdatelilToonGlitter");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateOutline), "String_UpdatelilToonOutline");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateParallax), "String_UpdatelilToonParallax");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateDistanceFade), "String_UpdatelilToonDistanceFade");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateAudioLink), "String_UpdatelilToonAudioLink");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateDissolve), "String_UpdatelilToonDissolve");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateIDMask), "String_UpdatelilToonIDMask");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateUVTileDiscard), "String_UpdatelilToonUVTileDiscard");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateStencil), "String_UpdatelilToonStencil");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateRendering), "String_UpdatelilToonRendering");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateTessellation), "String_UpdatelilToonTessellation");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceShadow), "String_ForcelilToonShadow");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceRimShade), "String_ForcelilToonRimShade");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceEmission1), "String_ForcelilToonEmission1");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceEmission2), "String_ForcelilToonEmission2");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceAnisotropy), "String_ForcelilToonAnisotropy");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceBacklight), "String_ForcelilToonBacklight");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceReflection), "String_ForcelilToonReflection");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceMatCap1), "String_ForcelilToonMatCap1");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceMatCap2), "String_ForcelilToonMatCap2");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceRimLight), "String_ForcelilToonRimLight");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceGlitter), "String_ForcelilToonGlitter");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceParallax), "String_ForcelilToonParallax");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceAudioLink), "String_ForcelilToonAudioLink");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ForceUVTileDiscard), "String_ForcelilToonUVTileDiscard");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateBackfaceColor), "String_UpdatelilToonBackfaceColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.BackfaceColor), "String_TargetBackfaceColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateShadowColor), "String_UpdatelilToonShadowColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.Shadow1Color), "String_TargetShadow1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.Shadow2Color), "String_TargetShadow2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.Shadow3Color), "String_TargetShadow3Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ShadowBorderColor), "String_TargetShadowBorderColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateRimShadeColor), "String_UpdatelilToonRimShadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.RimShadeColor), "String_TargetRimShadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateEmission1Color), "String_UpdatelilToonEmission1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.Emission1Color), "String_TargetEmission1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateEmission2Color), "String_UpdatelilToonEmission2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.Emission2Color), "String_TargetEmission2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateBacklightColor), "String_UpdatelilToonBacklightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.BacklightColor), "String_TargetBacklightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateReflectionColor), "String_UpdatelilToonReflectionColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.ReflectionColor), "String_TargetReflectionColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateMatCap1Color), "String_UpdatelilToonMatCap1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.MatCap1Color), "String_TargetMatCap1Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateMatCap2Color), "String_UpdatelilToonMatCap2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.MatCap2Color), "String_TargetMatCap2Color");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateRimLightColor), "String_UpdatelilToonRimLightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.RimLightColor), "String_TargetRimLightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateGlitterColor), "String_UpdatelilToonGlitterColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.GlitterColor), "String_TargetGlitterColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateOutlineColor), "String_UpdatelilToonOutlineColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.OutlineColor), "String_TargetOutlineColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.OutlineHighlightColor), "String_TargetOutlineHighlightColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.UpdateDistanceFadeColor), "String_UpdatelilToonDistanceFadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.DistanceFadeColor), "String_TargetDistanceFadeColor");
					DrawProperty(SerializedlilToonOption, nameof(TargetlilToonOption.DistanceFadeRimColor), "String_TargetDistanceFadeRimColor");
				}
				GUILayout.Space(BorderX);
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
			}
		}

		void DrawpoiyomiSection() {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			Foldpoiyomi = EditorGUILayout.Foldout(Foldpoiyomi, "poiyomi");
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			if (Foldpoiyomi) {
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {

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
					DrawProperty(SerializedGeneralOption, nameof(TargetGeneralOption.ResetRenderQueue), "String_RenderQueue");
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

		bool IsReadyToRevert() {
			return UndoGroupIndex >= 0 && ModifiedMaterials.Count > 0;
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
						IsModified = lilToonTemplateInstance.UpdateProperties(TargetMaterial);
						break;
					case ShaderType.poiyomi:
						IsModified = poiyomiTemplateInstance.UpdatepoiyomiProperties(TargetMaterial);
						break;
					case ShaderType.UnityChanToonShader:
						IsModified = UTSTemplateInstance.UpdateUnityChanToonShaderProperties(TargetMaterial);
						break;
					default:
						Debug.LogError($"[Macchiato] {string.Format(GetTranslatedString("NOT_SUPPORT_SHADER"), TargetMaterial.shader.name)}");
						break;
				}
				if (GeneralTemplateInstance.UpdateGeneralProperties(TargetMaterial)) IsModified = true;
				if (IsModified) NewModifiedMaterials.Add(TargetMaterial);
			}
			Debug.Log($"[Macchiato] {string.Format(GetTranslatedString("COMPLETED_UPDATEMATERIAL"), NewModifiedMaterials.Count)}");
			if (NewModifiedMaterials.Count == 0) return false;
			Undo.FlushUndoRecordObjects();
			Undo.CollapseUndoOperations(NewUndoGroupIndex);
			AssetDatabase.SaveAssets();
			UndoGroupIndex = NewUndoGroupIndex;
			ModifiedMaterials = NewModifiedMaterials;
			return true;
		}

		void AddAvatarMaterials() {
			Material[] AvatarMaterials = AvatarUtility.GetAvatarMaterials(AvatarGameObject);
			if (AvatarMaterials == null) return;
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

		public bool RevertMaterialProperties() {
			if (!IsReadyToRevert()) return false;
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
	}
}
