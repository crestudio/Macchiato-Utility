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
				if (UpdateFloatProperties(TargetMaterial, BasicFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonLighting) {
				if (UpdatelilToonLightingProperties(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonShadow) {
				if (UpdateFloatProperties(TargetMaterial, ShadowFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonReceiveShadow) {
				if (UpdateFloatProperties(TargetMaterial, ReceiveShadowFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBackfaceMask) {
				if (UpdateFloatProperties(TargetMaterial, BackfaceMaskFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBacklight) {
				if (UpdateFloatProperties(TargetMaterial, BacklightFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonShadowColor) {
				if (UpdatelilToonShadowColors(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonRimShadeColor) {
				if (UpdatelilToonRimShadeColors(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonBacklightColor) {
				if (UpdatelilToonBacklightColors(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonReflectionColor) {
				if (UpdatelilToonReflectionColors(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonRimLightColor) {
				if (UpdatelilToonRimLightColors(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdatelilToonOutlineColor) {
				if (UpdatelilToonOutlineColors(TargetMaterial)) IsModified = true;
			}
			if (ForcelilToonProperties(TargetMaterial)) IsModified = true;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool UpdateFloatProperties(Material TargetMaterial, (string PropertyName, float DefaultValue)[] TargetProperties) {
			bool IsDirty = false;
			foreach ((string PropertyName, float DefaultValue) TargetProperty in TargetProperties) {
				bool HasReferenceValue = ReferenceMaterial && ReferenceMaterial.HasProperty(TargetProperty.PropertyName);
				float NewValue = HasReferenceValue ? ReferenceMaterial.GetFloat(TargetProperty.PropertyName) : TargetProperty.DefaultValue;
				if (MaterialUtility.SetFloatProperty(TargetMaterial, TargetProperty.PropertyName, NewValue)) IsDirty = true;
			}
			return IsDirty;
		}

		bool UpdatelilToonLightingProperties(Material TargetMaterial) {
			bool IsDirty = false;
			foreach ((string PropertyName, float DefaultValue) TargetProperty in LightingFloatProperties) {
				bool HasReferenceValue = ReferenceMaterial && ReferenceMaterial.HasProperty(TargetProperty.PropertyName);
				float NewValue = HasReferenceValue ? ReferenceMaterial.GetFloat(TargetProperty.PropertyName) : TargetProperty.DefaultValue;
				if (MaterialUtility.SetFloatProperty(TargetMaterial, TargetProperty.PropertyName, NewValue)) IsDirty = true;
			}
			foreach ((string PropertyName, Color DefaultValue) TargetProperty in LightingColorProperties) {
				bool HasReferenceValue = ReferenceMaterial && ReferenceMaterial.HasProperty(TargetProperty.PropertyName);
				Color NewValue = HasReferenceValue ? ReferenceMaterial.GetColor(TargetProperty.PropertyName) : TargetProperty.DefaultValue;
				if (MaterialUtility.SetColorProperty(TargetMaterial, TargetProperty.PropertyName, NewValue)) IsDirty = true;
			}
			return IsDirty;
		}

		bool ForcelilToonProperties(Material TargetMaterial) {
			bool IsDirty = false;
			if (TargetTemplateOption.ForcelilToonShadow) {
				if (TargetMaterial.GetFloat("_UseShadow") != 1.0f) {
					TargetMaterial.SetFloat("_UseShadow", 1.0f);
					IsDirty = true;
				}
			}
			if (TargetTemplateOption.ForcelilToonRimShade) {
				if (TargetMaterial.GetFloat("_UseRimShade") != 1.0f) {
					TargetMaterial.SetFloat("_UseRimShade", 1.0f);
					IsDirty = true;
				}
			}
			if (TargetTemplateOption.ForcelilToonBacklight) {
				if (TargetMaterial.GetFloat("_UseBacklight") != 1.0f) {
					TargetMaterial.SetFloat("_UseBacklight", 1.0f);
					IsDirty = true;
				}
			}
			if (TargetTemplateOption.ForcelilToonReflection) {
				if (TargetMaterial.GetFloat("_UseReflection") != 1.0f) {
					TargetMaterial.SetFloat("_UseReflection", 1.0f);
					IsDirty = true;
				}
			}
			if (TargetTemplateOption.ForcelilToonRimLight) {
				if (TargetMaterial.GetFloat("_UseRim") != 1.0f) {
					TargetMaterial.SetFloat("_UseRim", 1.0f);
					IsDirty = true;
				}
			}
			return IsDirty;
		}

		bool UpdatelilToonShadowColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color ShadowColor = TargetTemplateOption.TargetShadow1Color;
			Color Shadow2ndColor = TargetTemplateOption.TargetShadow2Color;
			Color Shadow3rdColor = TargetTemplateOption.TargetShadow3Color;
			Color ShadowBorderColor = TargetTemplateOption.TargetShadowBorderColor;
			if (TargetMaterial.GetColor("_ShadowColor") != ShadowColor) { TargetMaterial.SetColor("_ShadowColor", ShadowColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_Shadow2ndColor") != Shadow2ndColor) { TargetMaterial.SetColor("_Shadow2ndColor", Shadow2ndColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_Shadow3rdColor") != Shadow3rdColor) { TargetMaterial.SetColor("_Shadow3rdColor", Shadow3rdColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_ShadowBorderColor") != ShadowBorderColor) { TargetMaterial.SetColor("_ShadowBorderColor", ShadowBorderColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonRimShadeColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color RimShadeColor = TargetTemplateOption.TargetRimShadeColor;
			if (TargetMaterial.GetColor("_RimShadeColor") != RimShadeColor) { TargetMaterial.SetColor("_RimShadeColor", RimShadeColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonBacklightColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color BacklightColor = TargetTemplateOption.TargetBacklightColor;
			if (TargetMaterial.GetColor("_BacklightColor") != BacklightColor) { TargetMaterial.SetColor("_BacklightColor", BacklightColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonReflectionColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color ReflectionColor = TargetTemplateOption.TargetReflectionColor;
			if (TargetMaterial.GetColor("_ReflectionColor") != ReflectionColor) { TargetMaterial.SetColor("_ReflectionColor", ReflectionColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonRimLightColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color RimColor = TargetTemplateOption.TargetRimLightColor;
			if (TargetMaterial.GetColor("_RimColor") != RimColor) { TargetMaterial.SetColor("_RimColor", RimColor); IsDirty = true; }
			return IsDirty;
		}

		bool UpdatelilToonOutlineColors(Material TargetMaterial) {
			bool IsDirty = false;
			Color OutlineColor = TargetTemplateOption.TargetOutlineColor;
			Color OutlineLitColor = TargetTemplateOption.TargetOutlineHighlightColor;
			if (TargetMaterial.GetColor("_OutlineColor") != OutlineColor) { TargetMaterial.SetColor("_OutlineColor", OutlineColor); IsDirty = true; }
			if (TargetMaterial.GetColor("_OutlineLitColor") != OutlineLitColor) { TargetMaterial.SetColor("_OutlineLitColor", OutlineLitColor); IsDirty = true; }
			return IsDirty;
		}
	}
}
