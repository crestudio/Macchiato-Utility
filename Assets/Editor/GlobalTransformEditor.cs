using UnityEngine;
using UnityEditor;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[CustomEditor(typeof(GlobalTransform))]
	[CanEditMultipleObjects]
	public class GlobalTransformEditor : Editor {

		const string UndoGroupName = "Macchiato Global Transform";

		public override void OnInspectorGUI() {
			Transform[] TargetTransforms = GetTargetTransforms();
			EditorGUILayout.Space(2f);
			DrawGlobalPositionField(TargetTransforms);
			DrawGlobalRotationField(TargetTransforms);
			DrawGlobalScaleField(TargetTransforms[0]);
		}

		Transform[] GetTargetTransforms() {
			Transform[] NewTargetTransforms = new Transform[targets.Length];
			for (int Index = 0; Index < targets.Length; Index++) {
				NewTargetTransforms[Index] = ((GlobalTransform)targets[Index]).transform;
			}
			return NewTargetTransforms;
		}

		void DrawGlobalPositionField(Transform[] TargetTransforms) {
			Vector3 FirstGlobalPosition = TargetTransforms[0].position;
			bool IsMixed = false;
			for (int Index = 1; Index < TargetTransforms.Length; Index++) {
				if (TargetTransforms[Index].position != FirstGlobalPosition) {
					IsMixed = true;
					break;
				}
			}
			EditorGUI.showMixedValue = IsMixed;
			EditorGUI.BeginChangeCheck();
			Vector3 NewGlobalPosition = EditorGUILayout.Vector3Field("Position", FirstGlobalPosition);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObjects(TargetTransforms, UndoGroupName);
				foreach (Transform TargetTransform in TargetTransforms) {
					TargetTransform.position = NewGlobalPosition;
				}
			}
			EditorGUI.showMixedValue = false;
		}

		void DrawGlobalRotationField(Transform[] TargetTransforms) {
			Vector3 FirstGlobalRotation = TargetTransforms[0].eulerAngles;
			bool IsMixed = false;
			for (int Index = 1; Index < TargetTransforms.Length; Index++) {
				if (TargetTransforms[Index].eulerAngles != FirstGlobalRotation) {
					IsMixed = true;
					break;
				}
			}
			EditorGUI.showMixedValue = IsMixed;
			EditorGUI.BeginChangeCheck();
			Vector3 NewGlobalRotation = EditorGUILayout.Vector3Field("Rotation", FirstGlobalRotation);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObjects(TargetTransforms, UndoGroupName);
				foreach (Transform TargetTransform in TargetTransforms) {
					TargetTransform.eulerAngles = NewGlobalRotation;
				}
			}
			EditorGUI.showMixedValue = false;
		}

		void DrawGlobalScaleField(Transform TargetTransform) {
			using (new EditorGUI.DisabledScope(true)) {
				EditorGUILayout.Vector3Field("Scale", TargetTransform.lossyScale);
			}
		}
	}
}