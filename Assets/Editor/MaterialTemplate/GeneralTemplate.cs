using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	internal class GeneralTemplate : MaterialTemplate {

		internal bool UpdateGeneralProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (UpdateRenderQueue) {
				if (UpdateRenderQueueProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateGPUInstancing) {
				if (UpdateGPUInstancingProperties(TargetMaterial)) IsModified = true;
			}
			if (UpdateGlobalIllumination) {
				if (UpdateGlobalIlluminationProperties(TargetMaterial)) IsModified = true;
			}
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}

		bool UpdateRenderQueueProperties(Material TargetMaterial) {
			bool IsDirty = false;
			bool IsTransparent = TargetMaterial.shader.name.Contains("Transparent");
			int RenderQueue = (!IsTransparent) ? -1 : 3000;
			if (TargetMaterial.renderQueue != RenderQueue) { TargetMaterial.renderQueue = RenderQueue; IsDirty = true; }
			return IsDirty;
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
