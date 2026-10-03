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

		public bool UpdateBasic = false;
		public bool UpdateLighting = false;
		public bool UpdateAlpha = false;
		public bool UpdateShadow = false;
		public bool UpdateRimShade = false;
		public bool UpdateEmission1 = false;
		public bool UpdateEmission2 = false;
		public bool UpdateAnisotropy = false;
		public bool UpdateBacklight = false;
		public bool UpdateReflection = false;
		public bool UpdateMatCap1 = false;
		public bool UpdateMatCap2 = false;
		public bool UpdateRimLight = false;
		public bool UpdateGlitter = false;
		public bool UpdateOutline = false;
		public bool UpdateParallax = false;
		public bool UpdateDistanceFade = false;
		public bool UpdateAudioLink = false;
		public bool UpdateDissolve = false;
		public bool UpdateIDMask = false;
		public bool UpdateUVTileDiscard = false;
		public bool UpdateStencil = false;
		public bool UpdateRendering = false;
		public bool UpdateTessellation = false;
		public bool ForceShadow = false;
		public bool ForceRimShade = false;
		public bool ForceEmission1 = false;
		public bool ForceEmission2 = false;
		public bool ForceAnisotropy = false;
		public bool ForceBacklight = false;
		public bool ForceReflection = false;
		public bool ForceMatCap1 = false;
		public bool ForceMatCap2 = false;
		public bool ForceRimLight = false;
		public bool ForceGlitter = false;
		public bool ForceParallax = false;
		public bool ForceAudioLink = false;
		public bool ForceUVTileDiscard = false;
		public bool UpdateBackfaceColor = false;
		public bool UpdateShadowColor = false;
		public bool UpdateRimShadeColor = false;
		public bool UpdateEmission1Color = false;
		public bool UpdateEmission2Color = false;
		public bool UpdateBacklightColor = false;
		public bool UpdateReflectionColor = false;
		public bool UpdateMatCap1Color = false;
		public bool UpdateMatCap2Color = false;
		public bool UpdateRimLightColor = false;
		public bool UpdateGlitterColor = false;
		public bool UpdateOutlineColor = false;
		public bool UpdateDistanceFadeColor = false;

		[ColorUsage(true, true)] public Color BackfaceColor = Color.black;
		[ColorUsage(false, false)] public Color Shadow1Color = Color.white;
		[ColorUsage(true, false)] public Color Shadow2Color = Color.white;
		[ColorUsage(true, false)] public Color Shadow3Color = Color.white;
		[ColorUsage(true, false)] public Color ShadowBorderColor = Color.white;
		[ColorUsage(true, false)] public Color RimShadeColor = Color.white;
		[ColorUsage(true, true)] public Color Emission1Color = Color.white;
		[ColorUsage(true, true)] public Color Emission2Color = Color.white;
		[ColorUsage(true, true)] public Color BacklightColor = Color.white;
		[ColorUsage(true, true)] public Color ReflectionColor = Color.white;
		[ColorUsage(true, true)] public Color MatCap1Color = Color.white;
		[ColorUsage(true, true)] public Color MatCap2Color = Color.white;
		[ColorUsage(true, true)] public Color RimLightColor = Color.white;
		[ColorUsage(true, true)] public Color GlitterColor = Color.white;
		[ColorUsage(true, true)] public Color OutlineColor = Color.black;
		[ColorUsage(true, true)] public Color OutlineHighlightColor = Color.white;
		[ColorUsage(true, true)] public Color DistanceFadeColor = Color.black;
		[ColorUsage(true, true)] public Color DistanceFadeRimColor = Color.black;

		internal void GetlilToonColors(Material TargetMaterial) {
			BackfaceColor = TargetMaterial.GetColor("_BackfaceColor");
			Shadow1Color = TargetMaterial.GetColor("_ShadowColor");
			Shadow2Color = TargetMaterial.GetColor("_Shadow2ndColor");
			Shadow3Color = TargetMaterial.GetColor("_Shadow3rdColor");
			ShadowBorderColor = TargetMaterial.GetColor("_ShadowBorderColor");
			RimShadeColor = TargetMaterial.GetColor("_RimShadeColor");
			Emission1Color = TargetMaterial.GetColor("_EmissionColor");
			Emission2Color = TargetMaterial.GetColor("_Emission2ndColor");
			BacklightColor = TargetMaterial.GetColor("_BacklightColor");
			ReflectionColor = TargetMaterial.GetColor("_ReflectionColor");
			MatCap1Color = TargetMaterial.GetColor("_MatCapColor");
			MatCap2Color = TargetMaterial.GetColor("_MatCap2ndColor");
			RimLightColor = TargetMaterial.GetColor("_RimColor");
			GlitterColor = TargetMaterial.GetColor("_GlitterColor");
			OutlineColor = TargetMaterial.GetColor("_OutlineColor");
			OutlineHighlightColor = TargetMaterial.GetColor("_OutlineLitColor");
			DistanceFadeColor = TargetMaterial.GetColor("_DistanceFadeColor");
			DistanceFadeRimColor = TargetMaterial.GetColor("_DistanceFadeRimColor");
		}

		internal void SelectCommon() {
			UpdateBasic = true;
			UpdateLighting = true;
			UpdateAlpha = false;
			UpdateShadow = true;
			UpdateRimShade = true;
			UpdateEmission1 = false;
			UpdateEmission2 = false;
			UpdateAnisotropy = false;
			UpdateBacklight = true;
			UpdateReflection = true;
			UpdateMatCap1 = false;
			UpdateMatCap2 = false;
			UpdateRimLight = true;
			UpdateGlitter = false;
			UpdateOutline = false;
			UpdateParallax = false;
			UpdateDistanceFade = false;
			UpdateAudioLink = false;
			UpdateDissolve = false;
			UpdateIDMask = false;
			UpdateUVTileDiscard = false;
			UpdateStencil = false;
			UpdateRendering = false;
			UpdateTessellation = false;
			UpdateBackfaceColor = false;
			UpdateShadowColor = true;
			UpdateRimShadeColor = true;
			UpdateEmission1Color = false;
			UpdateEmission2Color = false;
			UpdateBacklightColor = true;
			UpdateReflectionColor = true;
			UpdateMatCap1Color = false;
			UpdateMatCap2Color = false;
			UpdateRimLightColor = true;
			UpdateGlitterColor = false;
			UpdateOutlineColor = false;
			UpdateDistanceFadeColor = false;
		}

		internal void SelectMacchiato() {
			UpdateBasic = true;
			UpdateLighting = true;
			UpdateAlpha = false;
			UpdateShadow = true;
			UpdateRimShade = true;
			UpdateEmission1 = false;
			UpdateEmission2 = false;
			UpdateAnisotropy = false;
			UpdateBacklight = true;
			UpdateReflection = true;
			UpdateMatCap1 = false;
			UpdateMatCap2 = false;
			UpdateRimLight = true;
			UpdateGlitter = false;
			UpdateOutline = false;
			UpdateParallax = false;
			UpdateDistanceFade = false;
			UpdateAudioLink = false;
			UpdateDissolve = false;
			UpdateIDMask = false;
			UpdateUVTileDiscard = false;
			UpdateStencil = false;
			UpdateRendering = false;
			UpdateTessellation = false;
			ForceShadow = true;
			ForceRimShade = true;
			ForceEmission1 = false;
			ForceEmission2 = false;
			ForceAnisotropy = false;
			ForceBacklight = true;
			ForceReflection = true;
			ForceMatCap1 = false;
			ForceMatCap2 = false;
			ForceRimLight = true;
			ForceGlitter = false;
			ForceParallax = false;
			ForceAudioLink = false;
			ForceUVTileDiscard = false;
			UpdateBackfaceColor = true;
			UpdateShadowColor = true;
			UpdateRimShadeColor = true;
			UpdateEmission1Color = false;
			UpdateEmission2Color = false;
			UpdateBacklightColor = true;
			UpdateReflectionColor = true;
			UpdateMatCap1Color = false;
			UpdateMatCap2Color = false;
			UpdateRimLightColor = true;
			UpdateGlitterColor = false;
			UpdateOutlineColor = false;
			UpdateDistanceFadeColor = false;
		}

		internal void SelectDeepCopy() {
			UpdateBasic = true;
			UpdateLighting = true;
			UpdateAlpha = true;
			UpdateShadow = true;
			UpdateRimShade = true;
			UpdateEmission1 = true;
			UpdateEmission2 = true;
			UpdateAnisotropy = true;
			UpdateBacklight = true;
			UpdateReflection = true;
			UpdateMatCap1 = true;
			UpdateMatCap2 = true;
			UpdateRimLight = true;
			UpdateGlitter = true;
			UpdateOutline = true;
			UpdateParallax = true;
			UpdateDistanceFade = true;
			UpdateAudioLink = true;
			UpdateDissolve = true;
			UpdateIDMask = true;
			UpdateUVTileDiscard = true;
			UpdateStencil = true;
			UpdateRendering = true;
			UpdateTessellation = true;
			UpdateBackfaceColor = true;
			UpdateShadowColor = true;
			UpdateRimShadeColor = true;
			UpdateEmission1Color = true;
			UpdateEmission2Color = true;
			UpdateBacklightColor = true;
			UpdateReflectionColor = true;
			UpdateMatCap1Color = true;
			UpdateMatCap2Color = true;
			UpdateRimLightColor = true;
			UpdateGlitterColor = true;
			UpdateOutlineColor = true;
			UpdateDistanceFadeColor = true;
		}

		internal void SelectNone() {
			UpdateBasic = false;
			UpdateLighting = false;
			UpdateAlpha = false;
			UpdateShadow = false;
			UpdateRimShade = false;
			UpdateEmission1 = false;
			UpdateEmission2 = false;
			UpdateAnisotropy = false;
			UpdateBacklight = false;
			UpdateReflection = false;
			UpdateMatCap1 = false;
			UpdateMatCap2 = false;
			UpdateRimLight = false;
			UpdateGlitter = false;
			UpdateOutline = false;
			UpdateParallax = false;
			UpdateDistanceFade = false;
			UpdateAudioLink = false;
			UpdateDissolve = false;
			UpdateIDMask = false;
			UpdateUVTileDiscard = false;
			UpdateStencil = false;
			UpdateRendering = false;
			UpdateTessellation = false;
			ForceShadow = false;
			ForceRimShade = false;
			ForceEmission1 = false;
			ForceEmission2 = false;
			ForceAnisotropy = false;
			ForceBacklight = false;
			ForceReflection = false;
			ForceMatCap1 = false;
			ForceMatCap2 = false;
			ForceRimLight = false;
			ForceGlitter = false;
			ForceParallax = false;
			ForceAudioLink = false;
			ForceUVTileDiscard = false;
			UpdateBackfaceColor = false;
			UpdateShadowColor = false;
			UpdateRimShadeColor = false;
			UpdateEmission1Color = false;
			UpdateEmission2Color = false;
			UpdateBacklightColor = false;
			UpdateReflectionColor = false;
			UpdateMatCap1Color = false;
			UpdateMatCap2Color = false;
			UpdateRimLightColor = false;
			UpdateGlitterColor = false;
			UpdateOutlineColor = false;
			UpdateDistanceFadeColor = false;
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
			("_Cull", 0.0f),
			("_FlipNormal", 1.0f),
			("_BackfaceForceShadow", 1.0f),
			("_Invisible", 0.0f),
			("_ZWrite", 1.0f),
			("_AAStrength", 1.0f),
			("_EnvRimBorder", 3.0f),
			("_EnvRimBlur", 0.35f),
			("_UseDither", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] LightingFloatProperties = {
			("_LightMinLimit", 0.0f),
			("_LightMaxLimit", 1.0f),
			("_MonochromeLighting", 0.0f),
			("_ShadowEnvStrength", 1.0f),
			("_AsUnlit", 0.0f),
			("_VertexLightStrength", 0.0f),
			("_BlendOpFA", 4.0f),
			("_BeforeExposureLimit", 10000.0f),
			("_lilDirectionalLightStrength", 1.0f),
		};

		static readonly (string PropertyName, Color DefaultValue)[] LightingColorProperties = {
			("_LightDirectionOverride", new Color(0.0f, 0.001f, 0.0f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] AlphaFloatProperties = {
			("_Cutoff", 0.5f),
			("_AlphaMaskValue", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] ShadowFloatProperties = {
			("_ShadowStrength", 1.0f),
			("_ShadowStrengthMaskLOD", 0.0f),
			("_ShadowFlatBorder", 1.0f),
			("_ShadowFlatBlur", 1.0f),
			("_ShadowBorder", 0.5f),
			("_ShadowBlur", 0.15f),
			("_ShadowNormalStrength", 1.0f),
			("_ShadowReceive", 1.0f),
			("_Shadow2ndBorder", 0.4f),
			("_Shadow2ndBlur", 0.15f),
			("_Shadow2ndNormalStrength", 1.0f),
			("_Shadow2ndReceive", 1.0f),
			("_Shadow3rdBorder", 0.3f),
			("_Shadow3rdBlur", 0.15f),
			("_Shadow3rdNormalStrength", 1.0f),
			("_Shadow3rdReceive", 1.0f),
			("_ShadowBorderRange", 0.1f),
			("_ShadowMainStrength", 0.0f),
			("_ShadowEnvStrength", 1.0f),
			("_lilShadowCasterBias", 0.0f),
			("_ShadowBorderMaskLOD", 0.0f),
			("_ShadowPostAO", 1.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] ShadowColorProperties = {
			("_ShadowAOShift", new Color(1.0f, 0.0f, 1.0f, 0.0f)),
			("_ShadowAOShift2", new Color(1.0f, 0.0f, 1.0f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] RimShadeFloatProperties = {
			("_RimShadeNormalStrength", 1.0f),
			("_RimShadeBorder", 0.5f),
			("_RimShadeBlur", 1.0f),
			("_RimShadeFresnelPower", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] Emission1FloatProperties = {
			("_EmissionMainStrength", 0.0f),
			("_EmissionBlendMode", 1.0f),
			("_EmissionUseGrad", 0.0f),
			("_EmissionGradSpeed", 1.0f),
			("_EmissionParallaxDepth", 0.0f),
			("_EmissionFluorescence", 0.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] Emission1ColorProperties = {
			("_EmissionBlink", new Color(0.0f, 0.0f, 3.141593f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] Emission2FloatProperties = {
			("_Emission2ndMainStrength", 0.0f),
			("_Emission2ndBlendMode", 1.0f),
			("_Emission2ndUseGrad", 0.0f),
			("_Emission2ndGradSpeed", 1.0f),
			("_Emission2ndParallaxDepth", 0.0f),
			("_Emission2ndFluorescence", 0.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] Emission2ColorProperties = {
			("_Emission2ndBlink", new Color(0.0f, 0.0f, 3.141593f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] AnisotropyFloatProperties = {
			("_AnisotropyScale", 1.0f),
			("_Anisotropy2Reflection", 0.0f),
			("_AnisotropyTangentWidth", 1.0f),
			("_AnisotropyBitangentWidth", 1.0f),
			("_AnisotropyShift", 0.0f),
			("_AnisotropyShiftNoiseScale", 0.0f),
			("_AnisotropySpecularStrength", 1.0f),
			("_Anisotropy2ndTangentWidth", 1.0f),
			("_Anisotropy2ndBitangentWidth", 1.0f),
			("_Anisotropy2ndShift", 0.0f),
			("_Anisotropy2ndShiftNoiseScale", 0.0f),
			("_Anisotropy2ndSpecularStrength", 0.0f),
			("_Anisotropy2MatCap", 0.0f),
			("_Anisotropy2MatCap2nd", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] BacklightFloatProperties = {
			("_BacklightMainStrength", 0.0f),
			("_BacklightReceiveShadow", 1.0f),
			("_BacklightBackfaceMask", 1.0f),
			("_BacklightNormalStrength", 1.0f),
			("_BacklightBorder", 0.8f),
			("_BacklightBlur", 0.3f),
			("_BacklightDirectivity", 2.0f),
			("_BacklightViewStrength", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] ReflectionFloatProperties = {
			("_Smoothness", 0.0f),
			("_GSAAStrength", 1.0f),
			("_Metallic", 0.0f),
			("_Reflectance", 0.0f),
			("_ApplySpecular", 1.0f),
			("_SpecularToon", 0.0f),
			("_SpecularNormalStrength", 1.0f),
			("_SpecularBorder", 1.0f),
			("_SpecularBlur", 1.0f),
			("_ApplySpecularFA", 1.0f),
			("_ApplyReflection", 0.0f),
			("_ReflectionBlendMode", 1.0f),
		};

		static readonly (string PropertyName, float DefaultValue)[] MatCap1FloatProperties = {
			("_MatCapMainStrength", 0.0f),
			("_MatCapNormalStrength", 1.0f),
			("_MatCapBlend", 1.0f),
			("_MatCapEnableLighting", 1.0f),
			("_MatCapShadowMask", 1.0f),
			("_MatCapBackfaceMask", 1.0f),
			("_MatCapLod", 0.0f),
			("_MatCapBlendMode", 1.0f),
			("_MatCapCustomNormal", 0.0f),
			("_MatCapBumpScale", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] MatCap2FloatProperties = {
			("_MatCap2ndMainStrength", 0.0f),
			("_MatCap2ndNormalStrength", 1.0f),
			("_MatCap2ndBlend", 1.0f),
			("_MatCap2ndEnableLighting", 1.0f),
			("_MatCap2ndShadowMask", 1.0f),
			("_MatCap2ndBackfaceMask", 1.0f),
			("_MatCap2ndLod", 0.0f),
			("_MatCap2ndBlendMode", 1.0f),
			("_MatCap2ndCustomNormal", 0.0f),
			("_MatCap2ndBumpScale", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] RimLightFloatProperties = {
			("_RimMainStrength", 0.0f),
			("_RimEnableLighting", 1.0f),
			("_RimShadowMask", 1.0f),
			("_RimBackfaceMask", 1.0f),
			("_RimBlendMode", 1.0f),
			("_RimDirStrength", 0.0f),
			("_RimBorder", 0.5f),
			("_RimBlur", 0.4f),
			("_RimNormalStrength", 1.0f),
			("_RimFresnelPower", 1.0f),
			("_RimVRParallaxStrength", 1.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] GlitterFloatProperties = {
			("_GlitterUVMode", 0.0f),
			("_GlitterMainStrength", 0.0f),
			("_GlitterEnableLighting", 1.0f),
			("_GlitterShadowMask", 1.0f),
			("_GlitterBackfaceMask", 1.0f),
			("_GlitterApplyShape", 0.0f),
			("_GlitterScaleRandomize", 0.0f),
			("_GlitterSensitivity", 0.25f),
			("_GlitterVRParallaxStrength", 0.0f),
			("_GlitterNormalStrength", 1.0f),
			("_GlitterPostContrast", 1.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] GlitterColorProperties = {
			("_GlitterAtras", new Color(1.0f, 1.0f, 0.0f, 0.0f)),
			("_GlitterParams1", new Color(256.0f, 256.0f, 0.16f, 50.0f)),
			("_GlitterParams2", new Color(0.25f, 0.0f, 0.0f, 0.0f)),
		};

		static readonly (string PropertyName, float DefaultValue)[] OutlineFloatProperties = {
			("_OutlineLitApplyTex", 0.0f),
			("_OutlineLitScale", 10.0f),
			("_OutlineLitOffset", -8.0f),
			("_OutlineLitShadowReceive", 1.0f),
			("_OutlineEnableLighting", 1.0f),
			("_OutlineWidth", 0.05f),
			("_OutlineFixWidth", 0.5f),
			("_OutlineVertexR2Width", 0.0f),
			("_OutlineDeleteMesh", 1.0f),
			("_OutlineZBias", 0.0f),
			("_OutlineDisableInVR", 0.0f),
			("_OutlineVectorScale", 1.0f),
			("_OutlineVectorUVMode", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] ParallaxFloatProperties = {
			("_Parallax", 0.02f),
			("_ParallaxOffset", 0.5f),
			("_UsePOM", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] DistanceFadeFloatProperties = {
			("_DistanceFadeMode", 0.0f),
			("_DistanceFadeRimFresnelPower", 5.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] DistanceFadeColorProperties = {
			("_DistanceFade", new Color(0.1f, 0.01f, 0.0f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] AudioLinkFloatProperties = {
			("_AudioLinkUVMode", 1.0f),
			("_AudioLink2Main2nd", 0.0f),
			("_AudioLink2Main3rd", 0.0f),
			("_AudioLink2Emission", 0.0f),
			("_AudioLink2EmissionGrad", 0.0f),
			("_AudioLink2Emission2nd", 0.0f),
			("_AudioLink2Emission2ndGrad", 0.0f),
			("_AudioLink2Vertex", 0.0f),
			("_AudioLinkAsLocal", 0.0f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] AudioLinkColorProperties = {
			("_AudioLinkUVParams", new Color(0.25f, 0.0f, 0.0f, 0.125f)),
			("_AudioLinkDefaultValue", new Color(0.0f, 0.0f, 2.0f, 0.75f))
		};

		static readonly (string PropertyName, float DefaultValue)[] DissolveFloatProperties = {
			("_DissolveNoiseStrength", 0.1f)
		};

		static readonly (string PropertyName, Color DefaultValue)[] DissolveColorProperties = {
			("_DissolveParams", new Color(0.0f, 0.0f, 0.5f, 0.1f)),
			("_DissolveNoiseMask_ScrollRotate", new Color(0.0f, 0.0f, 0.0f, 0.0f))
		};

		static readonly (string PropertyName, float DefaultValue)[] IDMaskFloatProperties = {
			("_IDMaskCompile", 0.0f),
			("_IDMaskFrom", 8.0f),
			("_IDMaskIsBitmap", 0.0f),
			("_IDMask1", 0.0f),
			("_IDMask2", 0.0f),
			("_IDMask3", 0.0f),
			("_IDMask4", 0.0f),
			("_IDMask5", 0.0f),
			("_IDMask6", 0.0f),
			("_IDMask7", 0.0f),
			("_IDMask8", 0.0f),
			("_IDMaskIndex1", 0.0f),
			("_IDMaskIndex2", 0.0f),
			("_IDMaskIndex3", 0.0f),
			("_IDMaskIndex4", 0.0f),
			("_IDMaskIndex5", 0.0f),
			("_IDMaskIndex6", 0.0f),
			("_IDMaskIndex7", 0.0f),
			("_IDMaskIndex8", 0.0f),
			("_IDMaskControlsDissolve", 0.0f),
			("_IDMaskPrior1", 0.0f),
			("_IDMaskPrior2", 0.0f),
			("_IDMaskPrior3", 0.0f),
			("_IDMaskPrior4", 0.0f),
			("_IDMaskPrior5", 0.0f),
			("_IDMaskPrior6", 0.0f),
			("_IDMaskPrior7", 0.0f),
			("_IDMaskPrior8", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] UVTileDiscardFloatProperties = {
			("_UDIMDiscardUV", 0.0f),
			("_UDIMDiscardMode", 0.0f),
			("_UDIMDiscardRow0_0", 0.0f),
			("_UDIMDiscardRow0_1", 0.0f),
			("_UDIMDiscardRow0_2", 0.0f),
			("_UDIMDiscardRow0_3", 0.0f),
			("_UDIMDiscardRow1_0", 0.0f),
			("_UDIMDiscardRow1_1", 0.0f),
			("_UDIMDiscardRow1_2", 0.0f),
			("_UDIMDiscardRow1_3", 0.0f),
			("_UDIMDiscardRow2_0", 0.0f),
			("_UDIMDiscardRow2_1", 0.0f),
			("_UDIMDiscardRow2_2", 0.0f),
			("_UDIMDiscardRow2_3", 0.0f),
			("_UDIMDiscardRow3_0", 0.0f),
			("_UDIMDiscardRow3_1", 0.0f),
			("_UDIMDiscardRow3_2", 0.0f),
			("_UDIMDiscardRow3_3", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] StencilFloatProperties = {
			("_StencilRef", 0.0f),
			("_StencilReadMask", 255.0f),
			("_StencilWriteMask", 255.0f),
			("_StencilComp", 8.0f),
			("_StencilPass", 0.0f),
			("_StencilFail", 0.0f),
			("_StencilZFail", 0.0f),
			("_OutlineStencilRef", 0.0f),
			("_OutlineStencilReadMask", 255.0f),
			("_OutlineStencilWriteMask", 255.0f),
			("_OutlineStencilComp", 8.0f),
			("_OutlineStencilPass", 0.0f),
			("_OutlineStencilFail", 0.0f),
			("_OutlineStencilZFail", 0.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] RenderingFloatProperties = {
			("_Cull", 0.0f),
			("_ZClip", 1.0f),
			("_ZWrite", 1.0f),
			("_ZTest", 4.0f),
			("_OffsetFactor", 0.0f),
			("_OffsetUnits", 0.0f),
			("_ColorMask", 15.0f),
			("_AlphaToMask", 0.0f),
			("_lilShadowCasterBias", 0.0f),
			("_SrcBlend", 1.0f),
			("_DstBlend", 0.0f),
			("_SrcBlendAlpha", 1.0f),
			("_DstBlendAlpha", 10.0f),
			("_BlendOp", 0.0f),
			("_BlendOpAlpha", 0.0f),
			("_SrcBlendFA", 1.0f),
			("_DstBlendFA", 1.0f),
			("_SrcBlendAlphaFA", 0.0f),
			("_DstBlendAlphaFA", 1.0f),
			("_BlendOpFA", 4.0f),
			("_BlendOpAlphaFA", 4.0f),
			("_OutlineCull", 1.0f),
			("_OutlineZClip", 1.0f),
			("_OutlineZWrite", 1.0f),
			("_OutlineZTest", 2.0f),
			("_OutlineOffsetFactor", 0.0f),
			("_OutlineOffsetUnits", 0.0f),
			("_OutlineColorMask", 15.0f),
			("_OutlineAlphaToMask", 0.0f),
			("_OutlineSrcBlend", 1.0f),
			("_OutlineDstBlend", 0.0f),
			("_OutlineSrcBlendAlpha", 1.0f),
			("_OutlineDstBlendAlpha", 10.0f),
			("_OutlineBlendOp", 0.0f),
			("_OutlineBlendOpAlpha", 0.0f),
			("_OutlineSrcBlendFA", 1.0f),
			("_OutlineDstBlendFA", 1.0f),
			("_OutlineSrcBlendAlphaFA", 0.0f),
			("_OutlineDstBlendAlphaFA", 1.0f),
			("_OutlineBlendOpFA", 4.0f),
			("_OutlineBlendOpAlphaFA", 4.0f)
		};

		static readonly (string PropertyName, float DefaultValue)[] TessellationFloatProperties = {
			("_TessEdge", 10.0f),
			("_TessStrength", 0.5f),
			("_TessShrink", 0.0f),
			("_TessFactorMax", 3.0f)
		};

		internal bool UpdateProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (TargetTemplateOption.UpdateBasic) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, BasicFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateLighting) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, LightingFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, LightingColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateAlpha) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, AlphaFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateShadow && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseShadow")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, ShadowFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, ShadowColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateRimShade && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseRimShade")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, RimShadeFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateEmission1 && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseEmission")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, Emission1FloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, Emission1ColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateEmission2 && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseEmission2nd")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, Emission2FloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, Emission2ColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateAnisotropy && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseAnisotropy")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, AnisotropyFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateBacklight && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseBacklight")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, BacklightFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateReflection && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseReflection")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, ReflectionFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateMatCap1 && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseMatCap")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, MatCap1FloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateMatCap2 && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseMatCap2nd")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, MatCap2FloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateRimLight && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseRim")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, RimLightFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGlitter && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseGlitter")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, GlitterFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, GlitterColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateOutline && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseOutline")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, OutlineFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateParallax && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseParallax")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, ParallaxFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateDistanceFade) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, DistanceFadeFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, DistanceFadeColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateAudioLink && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseAudioLink")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, AudioLinkFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, AudioLinkColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateDissolve) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, DissolveFloatProperties)) IsModified = true;
				if (MaterialUtility.UpdateColorProperties(TargetMaterial, ReferenceMaterial, DissolveColorProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateIDMask) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, IDMaskFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateUVTileDiscard && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UDIMDiscardCompile")) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, UVTileDiscardFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateStencil) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, StencilFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateRendering) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, RenderingFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateTessellation) {
				if (MaterialUtility.UpdateFloatProperties(TargetMaterial, ReferenceMaterial, TessellationFloatProperties)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateBackfaceColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_BackfaceColor", TargetTemplateOption.BackfaceColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateShadowColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseShadow")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ShadowColor", TargetTemplateOption.Shadow1Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_Shadow2ndColor", TargetTemplateOption.Shadow2Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_Shadow3rdColor", TargetTemplateOption.Shadow3Color)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ShadowBorderColor", TargetTemplateOption.ShadowBorderColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateRimShadeColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseRimShade")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_RimShadeColor", TargetTemplateOption.RimShadeColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateEmission1Color && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseEmission")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_EmissionColor", TargetTemplateOption.Emission1Color)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateEmission2Color && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseEmission2nd")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_Emission2ndColor", TargetTemplateOption.Emission2Color)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateBacklightColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseBacklight")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_BacklightColor", TargetTemplateOption.BacklightColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateReflectionColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseReflection")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_ReflectionColor", TargetTemplateOption.ReflectionColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateMatCap1Color && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseMatCap")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_MatCapColor", TargetTemplateOption.MatCap1Color)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateMatCap2Color && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseMatCap2nd")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_MatCap2ndColor", TargetTemplateOption.MatCap2Color)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateRimLightColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseRim")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_RimColor", TargetTemplateOption.RimLightColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGlitterColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseGlitter")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_GlitterColor", TargetTemplateOption.GlitterColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateOutlineColor && MaterialUtility.IsPropertyActive(ReferenceMaterial, "_UseOutline")) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_OutlineColor", TargetTemplateOption.OutlineColor)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_OutlineLitColor", TargetTemplateOption.OutlineHighlightColor)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateDistanceFadeColor) {
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_DistanceFadeColor", TargetTemplateOption.DistanceFadeColor)) IsModified = true;
				if (MaterialUtility.SetColorProperty(TargetMaterial, "_DistanceFadeRimColor", TargetTemplateOption.DistanceFadeRimColor)) IsModified = true;
			}
			if (ForceProperties(TargetMaterial)) IsModified = true;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool ForceProperties(Material TargetMaterial) {
			bool IsDirty = false;
			if (TargetTemplateOption.ForceShadow) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseShadow", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceRimShade) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseRimShade", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceEmission1) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseEmission", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceEmission2) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseEmission2nd", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceAnisotropy) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseAnisotropy", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceBacklight) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseBacklight", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceReflection) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseReflection", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceMatCap1) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseMatCap", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceMatCap2) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseMatCap2nd", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceRimLight) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseRim", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceGlitter) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseGlitter", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceParallax) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseParallax", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceAudioLink) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UseAudioLink", 1.0f)) IsDirty = true;
			}
			if (TargetTemplateOption.ForceUVTileDiscard) {
				if (MaterialUtility.SetFloatProperty(TargetMaterial, "_UDIMDiscardCompile", 1.0f)) IsDirty = true;
			}
			return IsDirty;
		}
	}
}
