using System;

using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[Serializable]
	public class poiyomiTemplateOption {

	}

	internal class poiyomiTemplate {

		readonly poiyomiTemplateOption TargetTemplateOption;
		readonly Material ReferenceMaterial;

		internal poiyomiTemplate(poiyomiTemplateOption NewTemplateOption, Material NewReferenceMaterial) {
			TargetTemplateOption = NewTemplateOption;
			ReferenceMaterial = NewReferenceMaterial;
		}

		internal bool UpdatepoiyomiProperties(Material TargetMaterial) {
			bool IsModified = false;
			if (IsModified) {
				EditorUtility.SetDirty(TargetMaterial);
				return true;
			}
			return false;
		}
	}
}
