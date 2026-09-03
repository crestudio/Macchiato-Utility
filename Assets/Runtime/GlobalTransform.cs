#if UNITY_EDITOR
using UnityEngine;

using VRC.SDKBase;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[AddComponentMenu("Caramel Macchiato/Macchiato GlobalTransform")]
	[HelpURL("https://macchiato.booth.pm/")]
	[RequireComponent(typeof(Transform))]
	public class GlobalTransform : MonoBehaviour, IEditorOnly { }
}
#endif