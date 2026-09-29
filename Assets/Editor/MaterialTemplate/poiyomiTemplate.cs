using UnityEditor;
using UnityEngine;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	internal class poiyomiTemplate : MaterialTemplate {

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
