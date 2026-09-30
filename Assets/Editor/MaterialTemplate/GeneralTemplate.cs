using System;

using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[Serializable]
	public class GeneralTemplateOption {

		public bool ResetRenderQueue = false;
		public bool UpdateGPUInstancing = false;
		public bool UpdateGlobalIllumination = false;

		internal void SelectCommon() {
			ResetRenderQueue = false;
			UpdateGPUInstancing = true;
			UpdateGlobalIllumination = true;
		}

		internal void SelectMacchiato() {
			ResetRenderQueue = false;
			UpdateGPUInstancing = true;
			UpdateGlobalIllumination = true;
		}

		internal void SelectDeepCopy() {
			ResetRenderQueue = true;
			UpdateGPUInstancing = true;
			UpdateGlobalIllumination = true;
		}

		internal void SelectNone() {
			ResetRenderQueue = false;
			UpdateGPUInstancing = false;
			UpdateGlobalIllumination = false;
		}
	}

	internal class GeneralTemplate {

		readonly GeneralTemplateOption TargetTemplateOption;
		readonly Material ReferenceMaterial;

		internal GeneralTemplate(GeneralTemplateOption NewTemplateOption, Material NewReferenceMaterial) {
			TargetTemplateOption = NewTemplateOption;
			ReferenceMaterial = NewReferenceMaterial;
		}

		internal bool UpdateGeneralProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (TargetTemplateOption.ResetRenderQueue) {
				if (ResetRenderQueueProperties(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGPUInstancing) {
				if (UpdateGPUInstancingProperties(TargetMaterial, ReferenceMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGlobalIllumination) {
				if (UpdateGlobalIlluminationProperties(TargetMaterial, ReferenceMaterial)) IsModified = true;
			}
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool ResetRenderQueueProperties(Material TargetMaterial) {
			bool IsTransparent = TargetMaterial.shader.name.Contains("Transparent", StringComparison.OrdinalIgnoreCase);
			int ExpectedRenderQueue = IsTransparent ? 3000 : TargetMaterial.shader.renderQueue;
			if (TargetMaterial.renderQueue == ExpectedRenderQueue) return false;
			TargetMaterial.renderQueue = IsTransparent ? 3000 : -1;
			return true;
		}

		bool UpdateGPUInstancingProperties(Material TargetMaterial, Material ReferenceMaterial) {
			bool IsDirty = false;
			bool NewValue = ReferenceMaterial ? ReferenceMaterial.enableInstancing : true;
			if (TargetMaterial.enableInstancing != NewValue) { TargetMaterial.enableInstancing = NewValue; IsDirty = true; }
			return IsDirty;
		}

		bool UpdateGlobalIlluminationProperties(Material TargetMaterial, Material ReferenceMaterial) {
			bool IsDirty = false;
			MaterialGlobalIlluminationFlags NewFlag = ReferenceMaterial ? ReferenceMaterial.globalIlluminationFlags : MaterialGlobalIlluminationFlags.BakedEmissive;
			bool NewValue = ReferenceMaterial ? ReferenceMaterial.doubleSidedGI : true;
			if (TargetMaterial.globalIlluminationFlags != NewFlag) { TargetMaterial.globalIlluminationFlags = NewFlag; IsDirty = true; }
			if (TargetMaterial.doubleSidedGI != NewValue) { TargetMaterial.doubleSidedGI = NewValue; IsDirty = true; }
			return IsDirty;
		}
	}
}
