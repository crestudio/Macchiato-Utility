using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

using Macchiato.Core;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class MeshRendererUtility : EditorWindow {

		const string UndoGroupName = "Macchiato MeshRendererUtility";
		static int UndoGroupIndex;

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Update Renderer Setting", priority = 1000)]
		static void UpdateAvatarRenders() {
			RequestUpdateAvatarRenders();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Adjust Bound Box", priority = 1100)]
		static void UpdateBounds() {
			RequestUpdateBounds();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Assign AnchorOverride", priority = 1100)]
		static void UpdateAnchorOverride() {
			RequestUpdateAnchorOverride();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Change Probes Settings", priority = 1100)]
		static void UpdateProbes() {
			RequestUpdateProbes();
		}

		[MenuItem("Tools/Macchiato/Utility/MeshRenderer/Change to Two-Sided Shadow", priority = 1100)]
		static void UpdateTwosidedShadow() {
			RequestUpdateTwosidedShadow();
		}

		static void RequestUpdateAvatarRenders() {
			GameObject[] AvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			if (AvatarGameObjects.Length == 0) return;
			UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			Bounds NewBounds = new Bounds {
				center = new Vector3(0.0f, 0.0f, 0.0f),
				extents = new Vector3(1.0f, 1.0f, 1.0f),
			};
			foreach (GameObject AvatarGameObject in AvatarGameObjects) {
				if (!AvatarGameObject) continue;
				Transform AvatarAnchorOverride = AvatarUtility.GetAvatarAnchorOverride(AvatarGameObject);
				if (!AvatarAnchorOverride) AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.localBounds != NewBounds) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.updateWhenOffscreen = true;
						TargetSkinnedMeshRenderer.localBounds = NewBounds;
						TargetSkinnedMeshRenderer.updateWhenOffscreen = false;
						EditorUtility.SetDirty(TargetSkinnedMeshRenderer);
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[Macchiato] Changed {AvatarGameObject.name} Renderer Settings");
			}
		}

		static void RequestUpdateBounds() {
			GameObject[] AvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			if (AvatarGameObjects.Length == 0) return;
			UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			Bounds NewBounds = new Bounds {
				center = new Vector3(0.0f, 0.0f, 0.0f),
				extents = new Vector3(1.0f, 1.0f, 1.0f),
			};
			foreach (GameObject AvatarGameObject in AvatarGameObjects) {
				if (!AvatarGameObject) continue;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.localBounds != NewBounds) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.updateWhenOffscreen = true;
						TargetSkinnedMeshRenderer.localBounds = NewBounds;
						TargetSkinnedMeshRenderer.updateWhenOffscreen = false;
						EditorUtility.SetDirty(TargetSkinnedMeshRenderer);
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[Macchiato] Changed {AvatarGameObject.name} Bounds");
			}
		}

		static void RequestUpdateAnchorOverride() {
			GameObject[] AvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			if (AvatarGameObjects.Length == 0) return;
			UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			foreach (GameObject AvatarGameObject in AvatarGameObjects) {
				if (!AvatarGameObject) continue;
				Transform AvatarAnchorOverride = AvatarUtility.GetAvatarAnchorOverride(AvatarGameObject);
				if (!AvatarAnchorOverride) AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[Macchiato] Changed {AvatarGameObject.name} AnchorOverride");
			}
		}

		static void RequestUpdateProbes() {
			GameObject[] AvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			if (AvatarGameObjects.Length == 0) return;
			UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			foreach (GameObject AvatarGameObject in AvatarGameObjects) {
				if (!AvatarGameObject) continue;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[Macchiato] Changed {AvatarGameObject.name} LightProbe Usage Mode");
			}
		}

		static void RequestUpdateTwosidedShadow() {
			GameObject[] AvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			if (AvatarGameObjects.Length == 0) return;
			UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
			foreach (GameObject AvatarGameObject in AvatarGameObjects) {
				if (!AvatarGameObject) continue;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[Macchiato] Changed {AvatarGameObject.name} Shadow Casting Mode");
			}
		}

		static Transform GetAnchorOverride(GameObject TargetGameObject) {
			GameObject TargetHeadGameObject = AvatarUtility.GetHeadGameObject(TargetGameObject);
			if (TargetHeadGameObject) {
				Transform TargetHeadTransform = TargetGameObject.transform;
				Transform[] ChildTransforms = TargetHeadTransform.GetComponentsInChildren<Transform>(true);
				if (ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray().Length > 0) {
					Transform NewAnchorOverride = ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray()[0];
					return NewAnchorOverride;
				} else {
					GameObject NewChildAnchorOverride = new GameObject("AnchorOverride");
					Undo.RegisterCreatedObjectUndo(NewChildAnchorOverride, UndoGroupName);
					NewChildAnchorOverride.transform.SetParent(TargetHeadTransform, false);
					return NewChildAnchorOverride.transform;
				}
			}
			return null;
		}

		static (SkinnedMeshRenderer[], MeshRenderer[]) GetAvatarRenderers(GameObject TargetGameObject) {
			return (TargetGameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true), TargetGameObject.GetComponentsInChildren<MeshRenderer>(true));
		}
	}
}