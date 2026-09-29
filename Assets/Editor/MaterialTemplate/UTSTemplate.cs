using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	internal class UTSTemplate : MaterialTemplate {

		internal bool UpdateUnityChanToonShaderProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (UpdateUTSTextureShared) {
				if (UpdateTextureSharedProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateUTSNormalMap) {
				if (UpdateNormalMapProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateUTSBasicShading) {
				if (UpdateBasicShadingProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateUTSLightColor) {
				if (UpdateLightColorProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateUTSEnvironmentalLightingProperties) {
				if (UpdateEnvironmentalLightingProperties(TargetMaterial)) IsModified = true;
			}
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool UpdateTextureSharedProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float TextureShared = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Use_BaseAs1st") : 1.0f;
			if (TargetMaterial.GetFloat("_Use_BaseAs1st") != TextureShared) { TargetMaterial.SetFloat("_Use_BaseAs1st", TextureShared); IsDirty = true; }
			if (TargetMaterial.GetTexture("_1st_ShadeMap") != null) { TargetMaterial.SetTexture("_1st_ShadeMap", null); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Use_1stAs2nd") != TextureShared) { TargetMaterial.SetFloat("_Use_1stAs2nd", TextureShared); IsDirty = true; }
			if (TargetMaterial.GetTexture("_2nd_ShadeMap") != null) { TargetMaterial.SetTexture("_2nd_ShadeMap", null); IsDirty = true; }
			return IsDirty;
		}

		bool UpdateNormalMapProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float Is_NormalMapToBase = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToBase") : 1.0f;
			float Is_NormalMapToHighColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToHighColor") : 1.0f;
			float Is_NormalMapToRimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToRimLight") : 1.0f;
			if (TargetMaterial.GetFloat("_Is_NormalMapToBase") != Is_NormalMapToBase) { TargetMaterial.SetFloat("_Is_NormalMapToBase", Is_NormalMapToBase); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_NormalMapToHighColor") != Is_NormalMapToHighColor) { TargetMaterial.SetFloat("_Is_NormalMapToHighColor", Is_NormalMapToHighColor); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_NormalMapToRimLight") != Is_NormalMapToRimLight) { TargetMaterial.SetFloat("_Is_NormalMapToRimLight", Is_NormalMapToRimLight); IsDirty = true; }
			return IsDirty;
		}

		bool UpdateBasicShadingProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float Set_SystemShadowsToBase = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Set_SystemShadowsToBase") : 0.0f;
			float Is_Filter_HiCutPointLightColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_Filter_HiCutPointLightColor") : 0.0f;
			if (TargetMaterial.GetFloat("_Set_SystemShadowsToBase") != Set_SystemShadowsToBase) { TargetMaterial.SetFloat("_Set_SystemShadowsToBase", Set_SystemShadowsToBase); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_Filter_HiCutPointLightColor") != Is_Filter_HiCutPointLightColor) { TargetMaterial.SetFloat("_Is_Filter_HiCutPointLightColor", Is_Filter_HiCutPointLightColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdateLightColorProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float Is_LightColor_1st_Shade = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_1st_Shade") : 1.0f;
			float Is_LightColor_2nd_Shade = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_2nd_Shade") : 1.0f;
			float Is_LightColor_Ap_RimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Ap_RimLight") : 1.0f;
			float Is_LightColor_Base = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Base") : 1.0f;
			float Is_LightColor_HighColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_HighColor") : 1.0f;
			float Is_LightColor_MatCap = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_MatCap") : 1.0f;
			float Is_LightColor_Outline = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Outline") : 1.0f;
			float Is_LightColor_RimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_RimLight") : 1.0f;
			if (TargetMaterial.GetFloat("_Is_LightColor_1st_Shade") != Is_LightColor_1st_Shade) { TargetMaterial.SetFloat("_Is_LightColor_1st_Shade", Is_LightColor_1st_Shade); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_2nd_Shade") != Is_LightColor_2nd_Shade) { TargetMaterial.SetFloat("_Is_LightColor_2nd_Shade", Is_LightColor_2nd_Shade); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_Ap_RimLight") != Is_LightColor_Ap_RimLight) { TargetMaterial.SetFloat("_Is_LightColor_Ap_RimLight", Is_LightColor_Ap_RimLight); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_Base") != Is_LightColor_Base) { TargetMaterial.SetFloat("_Is_LightColor_Base", Is_LightColor_Base); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_HighColor") != Is_LightColor_HighColor) { TargetMaterial.SetFloat("_Is_LightColor_HighColor", Is_LightColor_HighColor); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_MatCap") != Is_LightColor_MatCap) { TargetMaterial.SetFloat("_Is_LightColor_MatCap", Is_LightColor_MatCap); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_RimLight") != Is_LightColor_RimLight) { TargetMaterial.SetFloat("_Is_LightColor_RimLight", Is_LightColor_RimLight); IsDirty = true; }
			if (!TargetMaterial.shader.name.Contains("NoOutline")) {
				if (TargetMaterial.GetFloat("_Is_LightColor_Outline") != Is_LightColor_Outline) { TargetMaterial.SetFloat("_Is_LightColor_Outline", Is_LightColor_Outline); IsDirty = true; }
			}
			return IsDirty;
		}

		bool UpdateEnvironmentalLightingProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float GI_Intensity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_GI_Intensity") : 0.0f;
			float Unlit_Intensity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Unlit_Intensity") : 1.0f;
			float Is_Filter_LightColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_Filter_LightColor") : 0.0f;
			float Is_BLD = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_BLD") : 0.0f;
			if (TargetMaterial.GetFloat("_GI_Intensity") != GI_Intensity) { TargetMaterial.SetFloat("_GI_Intensity", GI_Intensity); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Unlit_Intensity") != Unlit_Intensity) { TargetMaterial.SetFloat("_Unlit_Intensity", Unlit_Intensity); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_Filter_LightColor") != Is_Filter_LightColor) { TargetMaterial.SetFloat("_Is_Filter_LightColor", Is_Filter_LightColor); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Is_BLD") != Is_BLD) { TargetMaterial.SetFloat("_Is_BLD", Is_BLD); IsDirty = true; }
			return IsDirty;
		}
	}
}
