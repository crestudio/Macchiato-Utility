using System;

using UnityEditor;
using UnityEngine;

using Macchiato.Core;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[Serializable]
	public class lilToonTemplateOption {

		public bool UpdatelilToonBasic = false;
		public bool UpdatelilToonLighting = false;
		public bool UpdatelilToonShadow = false;
		public bool UpdatelilToonReceiveShadow = false;
		public bool UpdatelilToonBackfaceMask = false;
		public bool UpdatelilToonBacklight = false;
		public bool ForcelilToonShadow = false;
		public bool ForcelilToonRimShade = false;
		public bool ForcelilToonBacklight = false;
		public bool ForcelilToonReflection = false;
		public bool ForcelilToonRimLight = false;
		public bool UpdatelilToonShadowColor = false;
		public bool UpdatelilToonRimShadeColor = false;
		public bool UpdatelilToonBacklightColor = false;
		public bool UpdatelilToonReflectionColor = false;
		public bool UpdatelilToonRimLightColor = false;
		public bool UpdatelilToonOutlineColor = false;

		[ColorUsage(false, false)] public Color TargetShadow1Color = Color.white;
		[ColorUsage(true, false)] public Color TargetShadow2Color = Color.white;
		[ColorUsage(true, false)] public Color TargetShadow3Color = Color.white;
		[ColorUsage(true, false)] public Color TargetShadowBorderColor = Color.white;
		[ColorUsage(true, false)] public Color TargetRimShadeColor = Color.white;
		[ColorUsage(true, true)] public Color TargetBacklightColor = Color.white;
		[ColorUsage(true, true)] public Color TargetReflectionColor = Color.white;
		[ColorUsage(true, true)] public Color TargetRimLightColor = Color.white;
		[ColorUsage(true, true)] public Color TargetOutlineColor = Color.white;
		[ColorUsage(true, true)] public Color TargetOutlineHighlightColor = Color.white;

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

	internal class lilToonTemplate {

		readonly lilToonTemplateOption TargetTemplateOption;
		readonly Material ReferenceMaterial;

		internal lilToonTemplate(lilToonTemplateOption NewTemplateOption, Material NewReferenceMaterial) {
			TargetTemplateOption = NewTemplateOption;
			ReferenceMaterial = NewReferenceMaterial;
		}

		static readonly (string PropertyName, float DefaultValue)[] BasicFloatProperties = {
			("_Cutoff", 0.5f),
			("_Cull", 2.0f),
			("_FlipNormal", 1.0f),
			("_BackfaceForceShadow", 1.0f),
			("_AlphaMaskValue", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] LightingFloatProperties = {
			("_LightMinLimit", 0.0f),
			("_LightMaxLimit", 1.0f),
			("_MonochromeLighting", 0.0f),
			("_ShadowEnvStrength", 1.0f),
			("_AsUnlit", 0.0f),
			("_VertexLightStrength", 0.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] LightingColorProperties = {
			("_LightDirectionOverride", new Color(0.0f, 0.001f, 0.0f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] ShadowFloatProperties = {
			("_ShadowBorder", 0.6f),
			("_ShadowBlur", 0.15f),
			("_ShadowNormalStrength", 1.0f),
			("_Shadow2ndBorder", 0.4f),
			("_Shadow2ndBlur", 0.15f),
			("_Shadow2ndNormalStrength", 1.0f),
			("_Shadow3rdBorder", 0.2f),
			("_Shadow3rdBlur", 0.15f),
			("_Shadow3rdNormalStrength", 1.0f),
			("_ShadowBorderRange", 0.0f),
			("_ShadowMainStrength", 0.0f),
			("_ShadowEnvStrength", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] ReceiveShadowFloatProperties = {
			("_ShadowReceive", 1.0f),
			("_Shadow2ndReceive", 1.0f),
			("_Shadow3rdReceive", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] BackfaceMaskFloatProperties = {
			("_BacklightBackfaceMask", 1.0f),
			("_GlitterBackfaceMask", 1.0f),
			("_MatCapBackfaceMask", 1.0f),
			("_MatCap2ndBackfaceMask", 1.0f),
			("_RimBackfaceMask", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] BacklightFloatProperties = {
			("_BacklightMainStrength", 0.3f),
			("_BacklightBorder", 0.8f),
			("_BacklightBlur", 0.3f),
			("_BacklightDirectivity", 2.0f)
		};

		internal bool UpdatelilToonProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (TargetTemplateOption.UpdatelilToonBasic) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, BasicFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonLighting) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, LightingFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, LightingColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonShadow) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, ShadowFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonReceiveShadow) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, ReceiveShadowFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBackfaceMask) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, BackfaceMaskFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBacklight) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, BacklightFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonShadowColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ShadowColor", TargetTemplateOption.TargetShadow1Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_Shadow2ndColor", TargetTemplateOption.TargetShadow2Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_Shadow3rdColor", TargetTemplateOption.TargetShadow3Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ShadowBorderColor", TargetTemplateOption.TargetShadowBorderColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonRimShadeColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_RimShadeColor", TargetTemplateOption.TargetRimShadeColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBacklightColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_BacklightColor", TargetTemplateOption.TargetBacklightColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonReflectionColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ReflectionColor", TargetTemplateOption.TargetReflectionColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonRimLightColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_RimColor", TargetTemplateOption.TargetRimLightColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonOutlineColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_OutlineColor", TargetTemplateOption.TargetOutlineColor)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_OutlineLitColor", TargetTemplateOption.TargetOutlineHighlightColor)) IsModified = true;
			}
			if (ForcelilToonProperties(TargetMaterial)) IsModified = true;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool ForcelilToonProperties(Material TargetMaterial) {
			bool IsDirty = false;
			if (TargetTemplateOption.ForcelilToonShadow) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseShadow", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForcelilToonRimShade) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseRimShade", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForcelilToonBacklight) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseBacklight", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForcelilToonReflection) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseReflection", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForcelilToonRimLight) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseRim", 1.0f)) IsDirty = true;
			}
			return IsDirty;
		}
	}
}
