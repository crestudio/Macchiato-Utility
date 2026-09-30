using System;

using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[Serializable]
	public class UTSTemplateOption {

	}

	internal class UTSTemplate {

		readonly UTSTemplateOption TargetTemplateOption;
		readonly Material ReferenceMaterial;

		internal UTSTemplate(UTSTemplateOption NewTemplateOption, Material NewReferenceMaterial) {
			TargetTemplateOption = NewTemplateOption;
			ReferenceMaterial = NewReferenceMaterial;
		}

		internal bool UpdateUnityChanToonShaderProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}
	}
}
