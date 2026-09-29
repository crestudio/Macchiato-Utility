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

		public bool UpdateRenderQueue = false;
		public bool UpdateGPUInstancing = false;
		public bool UpdateGlobalIllumination = false;
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
			if (TargetTemplateOption.UpdateRenderQueue) {
				if (UpdateRenderQueueProperties(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGPUInstancing) {
				if (UpdateGPUInstancingProperties(TargetMaterial)) IsModified = true;
			}
			if (TargetTemplateOption.UpdateGlobalIllumination) {
				if (UpdateGlobalIlluminationProperties(TargetMaterial)) IsModified = true;
			}
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool UpdateRenderQueueProperties(Material TargetMaterial) {
			bool IsTransparent = TargetMaterial.shader.name.Contains("Transparent", StringComparison.OrdinalIgnoreCase);
			int ExpectedRenderQueue = IsTransparent ? 3000 : TargetMaterial.shader.renderQueue;
			if (TargetMaterial.renderQueue == ExpectedRenderQueue) return false;
			TargetMaterial.renderQueue = IsTransparent ? 3000 : -1;
			return true;
		}

		bool UpdateGPUInstancingProperties(Material TargetMaterial) {
			bool IsDirty = false;
			bool EnableInstancingVariants = true;
			if (TargetMaterial.enableInstancing != EnableInstancingVariants) { TargetMaterial.enableInstancing = EnableInstancingVariants; IsDirty = true; }
			return IsDirty;
		}

		bool UpdateGlobalIlluminationProperties(Material TargetMaterial) {
			bool IsDirty = false;
			MaterialGlobalIlluminationFlags GlobalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
			bool DoubleSidedGI = true;
			if (TargetMaterial.globalIlluminationFlags != GlobalIlluminationFlags) { TargetMaterial.globalIlluminationFlags = GlobalIlluminationFlags; IsDirty = true; }
			if (TargetMaterial.doubleSidedGI != DoubleSidedGI) { TargetMaterial.doubleSidedGI = DoubleSidedGI; IsDirty = true; }
			return IsDirty;
		}
	}
}
