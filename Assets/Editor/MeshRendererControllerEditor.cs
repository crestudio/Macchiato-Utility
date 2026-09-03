using UnityEditor;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class MeshRendererControllerEditor : EditorWindow {

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Update Renderer Setting", priority = 1000)]
		static void UpdateAvatarRenders() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAvatarRenders();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Adjust Bound Box", priority = 1100)]
		static void UpdateBounds() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateBounds();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Change to Two-Sided Shadow", priority = 1100)]
		static void UpdateTwosidedShadow() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateTwosidedShadow();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Change Probes Settings", priority = 1100)]
		static void UpdateProbes() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateProbes();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Assign AnchorOverride", priority = 1100)]
		static void UpdateAnchorOverride() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAnchorOverride();
		}
	}
}