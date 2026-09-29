using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	internal class lilToonTemplate : MaterialTemplate {

		internal bool UpdatelilToonProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (UpdatelilToonBasic) {
				if (UpdatelilToonBasicProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonLighting) {
				if (UpdatelilToonLightingProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonShadow) {
				if (UpdatelilToonShadowProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonReceiveShadow) {
				if (UpdatelilToonReceiveShadowProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonBackfaceMask) {
				if (UpdatelilToonBackfaceMaskProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonBacklight) {
				if (UpdatelilToonBacklightProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonShadowColor) {
				if (UpdatelilToonShadowColors(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonRimShadeColor) {
				if (UpdatelilToonRimShadeColors(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonBacklightColor) {
				if (UpdatelilToonBacklightColors(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonReflectionColor) {
				if (UpdatelilToonReflectionColors(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonRimLightColor) {
				if (UpdatelilToonRimLightColors(TargetMaterial)) IsModified = true;
			}
			if (UpdatelilToonOutlineColor) {
				if (UpdatelilToonOutlineColors(TargetMaterial)) IsModified = true;
			}
			if (ForcelilToonProperties(TargetMaterial)) IsModified = true;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool UpdatelilToonBasicProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float Cutoff = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Cutoff") : 0.5f;
			float Cull = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Cull") : 2.0f;
			float FlipNormal = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_FlipNormal") : 1.0f;
			float BackfaceForceShadow = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BackfaceForceShadow") : 1.0f;
			float AlphaMaskValue = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_AlphaMaskValue") : 0.0f;
			if (TargetMaterial.GetFloat("_Cutoff") != Cutoff) { TargetMaterial.SetFloat("_Cutoff", Cutoff); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Cull") != Cull) { TargetMaterial.SetFloat("_Cull", Cull); IsDirty = true; }
			if (TargetMaterial.GetFloat("_FlipNormal") != FlipNormal) { TargetMaterial.SetFloat("_FlipNormal", FlipNormal); IsDirty = true; }
			if (TargetMaterial.GetFloat("_BackfaceForceShadow") != BackfaceForceShadow) { TargetMaterial.SetFloat("_BackfaceForceShadow", BackfaceForceShadow); IsDirty = true; }
			if (TargetMaterial.GetFloat("_AlphaMaskValue") != AlphaMaskValue) { TargetMaterial.SetFloat("_AlphaMaskValue", AlphaMaskValue); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonLightingProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float LightMinLimit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_LightMinLimit") : 0.0f;
			float LightMaxLimit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_LightMaxLimit") : 1.0f;
			float MonochromeLighting = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_MonochromeLighting") : 0.0f;
			float ShadowEnvStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowEnvStrength") : 1.0f;
			float AsUnlit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_AsUnlit") : 0.0f;
			float VertexLightStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_VertexLightStrength") : 0.0f;
			Color LightDirectionOverride = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_LightDirectionOverride") : new Color(0.0f, 0.001f, 0.0f, 0.0f);
			if (TargetMaterial.GetFloat("_LightMinLimit") != LightMinLimit) { TargetMaterial.SetFloat("_LightMinLimit", LightMinLimit); IsDirty = true; }
			if (TargetMaterial.GetFloat("_LightMaxLimit") != LightMaxLimit) { TargetMaterial.SetFloat("_LightMaxLimit", LightMaxLimit); IsDirty = true; }
			if (TargetMaterial.GetFloat("_MonochromeLighting") != MonochromeLighting) { TargetMaterial.SetFloat("_MonochromeLighting", MonochromeLighting); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowEnvStrength") != ShadowEnvStrength) { TargetMaterial.SetFloat("_ShadowEnvStrength", ShadowEnvStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_AsUnlit") != AsUnlit) { TargetMaterial.SetFloat("_AsUnlit", AsUnlit); IsDirty = true; }
			if (TargetMaterial.GetFloat("_VertexLightStrength") != VertexLightStrength) { TargetMaterial.SetFloat("_VertexLightStrength", VertexLightStrength); IsDirty = true; }
			if (TargetMaterial.GetColor("_LightDirectionOverride") != LightDirectionOverride) { TargetMaterial.SetColor("_LightDirectionOverride", LightDirectionOverride); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonShadowProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float ShadowBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBorder") : 0.6f;
			float ShadowBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBlur") : 0.15f;
			float ShadowNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowNormalStrength") : 1.0f;
			float Shadow2ndBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndBorder") : 0.4f;
			float Shadow2ndBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndBlur") : 0.15f;
			float Shadow2ndNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndNormalStrength") : 1.0f;
			float Shadow3rdBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdBorder") : 0.2f;
			float Shadow3rdBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdBlur") : 0.15f;
			float Shadow3rdNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdNormalStrength") : 1.0f;
			float ShadowBorderRange = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBorderRange") : 0.0f;
			float ShadowMainStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowMainStrength") : 0.0f;
			float ShadowEnvStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowEnvStrength") : 1.0f;
			if (TargetMaterial.name.Contains("Head")) {
				ShadowBorder = 0.25f;
				Shadow2ndBorder = 0.15f;
				Shadow3rdBorder = 0.05f;
			}
			if (TargetMaterial.GetFloat("_ShadowBorder") != ShadowBorder) { TargetMaterial.SetFloat("_ShadowBorder", ShadowBorder); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowBlur") != ShadowBlur) { TargetMaterial.SetFloat("_ShadowBlur", ShadowBlur); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowNormalStrength") != ShadowNormalStrength) { TargetMaterial.SetFloat("_ShadowNormalStrength", ShadowNormalStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndBorder") != Shadow2ndBorder) { TargetMaterial.SetFloat("_Shadow2ndBorder", Shadow2ndBorder); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndBlur") != Shadow2ndBlur) { TargetMaterial.SetFloat("_Shadow2ndBlur", Shadow2ndBlur); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndNormalStrength") != Shadow2ndNormalStrength) { TargetMaterial.SetFloat("_Shadow2ndNormalStrength", Shadow2ndNormalStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdBorder") != Shadow3rdBorder) { TargetMaterial.SetFloat("_Shadow3rdBorder", Shadow3rdBorder); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdBlur") != Shadow3rdBlur) { TargetMaterial.SetFloat("_Shadow3rdBlur", Shadow3rdBlur); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdNormalStrength") != Shadow3rdNormalStrength) { TargetMaterial.SetFloat("_Shadow3rdNormalStrength", Shadow3rdNormalStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowBorderRange") != ShadowBorderRange) { TargetMaterial.SetFloat("_ShadowBorderRange", ShadowBorderRange); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowMainStrength") != ShadowMainStrength) { TargetMaterial.SetFloat("_ShadowMainStrength", ShadowMainStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_ShadowEnvStrength") != ShadowEnvStrength) { TargetMaterial.SetFloat("_ShadowEnvStrength", ShadowEnvStrength); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonReceiveShadowProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float ShadowReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowReceive") : 1.0f;
			float Shadow2ndReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndReceive") : 1.0f;
			float Shadow3rdReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdReceive") : 1.0f;
			if (TargetMaterial.GetFloat("_ShadowReceive") != ShadowReceive) { TargetMaterial.SetFloat("_ShadowReceive", ShadowReceive); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndReceive") != Shadow2ndReceive) { TargetMaterial.SetFloat("_Shadow2ndReceive", Shadow2ndReceive); IsDirty = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdReceive") != Shadow3rdReceive) { TargetMaterial.SetFloat("_Shadow3rdReceive", Shadow3rdReceive); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonBackfaceMaskProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float BackfaceMask = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_MatCapBackfaceMask") : 1.0f;
			if (TargetMaterial.GetFloat("_BacklightBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_BacklightBackfaceMask", BackfaceMask); IsDirty = true; }
			if (TargetMaterial.GetFloat("_GlitterBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_GlitterBackfaceMask", BackfaceMask); IsDirty = true; }
			if (TargetMaterial.GetFloat("_MatCap2ndBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_MatCap2ndBackfaceMask", BackfaceMask); IsDirty = true; }
			if (TargetMaterial.GetFloat("_MatCapBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_MatCapBackfaceMask", BackfaceMask); IsDirty = true; }
			if (TargetMaterial.GetFloat("_RimBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_RimBackfaceMask", BackfaceMask); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonBacklightProperties(Material TargetMaterial) {
			bool IsDirty = false;
			float BacklightMainStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightMainStrength") : 0.3f;
			float BacklightBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightBorder") : 0.8f;
			float BacklightBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightBlur") : 0.3f;
			float BacklightDirectivity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightDirectivity") : 2.0f;
			if (TargetMaterial.GetFloat("_BacklightMainStrength") != BacklightMainStrength) { TargetMaterial.SetFloat("_BacklightMainStrength", BacklightMainStrength); IsDirty = true; }
			if (TargetMaterial.GetFloat("_BacklightBorder") != BacklightBorder) { TargetMaterial.SetFloat("_BacklightBorder", BacklightBorder); IsDirty = true; }
			if (TargetMaterial.GetFloat("_BacklightBlur") != BacklightBlur) { TargetMaterial.SetFloat("_BacklightBlur", BacklightBlur); IsDirty = true; }
			if (TargetMaterial.GetFloat("_BacklightDirectivity") != BacklightDirectivity) { TargetMaterial.SetFloat("_BacklightDirectivity", BacklightDirectivity); IsDirty = true; }
			return IsDirty;
		}

		bool ForcelilToonProperties(Material TargetMaterial) {
			bool IsDirty = false;
			if (ForcelilToonShadow) { TargetMaterial.SetFloat("_UseShadow", 1.0f); IsDirty = true; }
			if (ForcelilToonRimShade) { TargetMaterial.SetFloat("_UseRimShade", 1.0f); IsDirty = true; }
			if (ForcelilToonBacklight) { TargetMaterial.SetFloat("_UseBacklight", 1.0f); IsDirty = true; }
			if (ForcelilToonReflection) { TargetMaterial.SetFloat("_UseReflection", 1.0f); IsDirty = true; }
			if (ForcelilToonRimLight) { TargetMaterial.SetFloat("_UseRim", 1.0f); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonShadowColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color ShadowColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_ShadowColor") : TargetShadow1Color;
			Color Shadow2ndColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_Shadow2ndColor") : TargetShadow2Color;
			Color Shadow3rdColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_Shadow3rdColor") : TargetShadow3Color;
			Color ShadowBorderColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_ShadowBorderColor") : TargetShadowBorderColor;
			if (TargetMaterial.GetColor("_ShadowColor") != ShadowColor) { TargetMaterial.SetColor("_ShadowColor", ShadowColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_Shadow2ndColor") != Shadow2ndColor) { TargetMaterial.SetColor("_Shadow2ndColor", Shadow2ndColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_Shadow3rdColor") != Shadow3rdColor) { TargetMaterial.SetColor("_Shadow3rdColor", Shadow3rdColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_ShadowBorderColor") != ShadowBorderColor) { TargetMaterial.SetColor("_ShadowBorderColor", ShadowBorderColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonRimShadeColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color RimShadeColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_RimShadeColor") : TargetRimShadeColor;
			if (TargetMaterial.GetColor("_RimShadeColor") != RimShadeColor) { TargetMaterial.SetColor("_RimShadeColor", RimShadeColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonBacklightColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color BacklightColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_BacklightColor") : TargetBacklightColor;
			if (TargetMaterial.GetColor("_BacklightColor") != BacklightColor) { TargetMaterial.SetColor("_BacklightColor", BacklightColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonReflectionColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color ReflectionColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_ReflectionColor") : TargetReflectionColor;
			if (TargetMaterial.GetColor("_ReflectionColor") != ReflectionColor) { TargetMaterial.SetColor("_ReflectionColor", ReflectionColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonRimLightColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color RimColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_RimColor") : TargetRimLightColor;
			if (TargetMaterial.GetColor("_RimColor") != RimColor) { TargetMaterial.SetColor("_RimColor", RimColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonOutlineColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color OutlineColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_OutlineColor") : TargetOutlineColor;
			Color OutlineLitColor = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_OutlineLitColor") : TargetOutlineHighlightColor;
			if (TargetMaterial.GetColor("_OutlineColor") != OutlineColor) { TargetMaterial.SetColor("_OutlineColor", OutlineColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_OutlineLitColor") != OutlineLitColor) { TargetMaterial.SetColor("_OutlineLitColor", OutlineLitColor); IsDirty = true; }
			return IsDirty;
		}

		internal void GetlilToonColors(Material TargetMaterial) {
			TargetShadow1Color = TargetMaterial.GetColor("_ShadowColor");
			TargetShadow2Color = TargetMaterial.GetColor("_Shadow2ndColor");
			TargetShadow3Color = TargetMaterial.GetColor("_Shadow3rdColor");
			TargetShadowBorderColor = TargetMaterial.GetColor("_ShadowBorderColor");
			TargetRimShadeColor = TargetMaterial.GetColor("_RimShadeColor");
			TargetBacklightColor = TargetMaterial.GetColor("_BacklightColor");
			TargetReflectionColor = TargetMaterial.GetColor("_ReflectionColor");
			TargetRimLightColor = TargetMaterial.GetColor("_RimColor");
			TargetOutlineColor = TargetMaterial.GetColor("_OutlineColor");
			TargetOutlineHighlightColor = TargetMaterial.GetColor("_OutlineLitColor");
		}
	}
}
