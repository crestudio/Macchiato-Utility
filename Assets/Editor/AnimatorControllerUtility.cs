using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

using Macchiato.Core;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class AnimatorControllerUtility : EditorWindow {

		[MenuItem("Assets/Macchiato/Animator/Write Defaults On", true)]
		static bool ValidateControllerOn() {
			return AssetUtility.ContainAnimatorController(Selection.objects);
		}

		[MenuItem("Assets/Macchiato/Animator/Write Defaults Off", true)]
		static bool ValidateControllerOff() {
			return AssetUtility.ContainAnimatorController(Selection.objects);
		}

		[MenuItem("Assets/Macchiato/Animator/Write Defaults On", priority = 1000)]
		static void RequestAnimatorWriteDefaultsOn() {
			if (Selection.objects.Length > 0) {
				int ModifiedCount = 0;
				try {
					for (int Index = 0; Index < Selection.objects.Length; Index++) {
						if (!Selection.objects[Index]) continue;
						EditorUtility.DisplayProgressBar("Write Defaults On",
							$"Processing : {Selection.objects[Index].name}",
							(float)Index / Selection.objects.Length);
						if (Selection.objects[Index] is not AnimatorController) continue;
						AnimatorController TargetAnimator = Selection.objects[Index] as AnimatorController;
						if (TargetAnimator.name.EndsWith("Original")) continue;
						if (ModifyWriteDefaults(TargetAnimator, true)) {
							ModifiedCount++;
						}
					}
				} finally {
					EditorUtility.ClearProgressBar();
					if (ModifiedCount > 0) {
						AssetDatabase.SaveAssets();
						AssetDatabase.Refresh();
					}
				}
				Debug.Log($"[Macchiato] Modified write defaults on in {ModifiedCount} animator controllers");
			}
		}

		[MenuItem("Assets/Macchiato/Animator/Write Defaults Off", priority = 1000)]
		static void RequestAnimatorWriteDefaultsOff() {
			if (Selection.objects.Length > 0) {
				int ModifiedCount = 0;
				try {
					for (int Index = 0; Index < Selection.objects.Length; Index++) {
						if (!Selection.objects[Index]) continue;
						EditorUtility.DisplayProgressBar("Write Defaults Off",
							$"Processing : {Selection.objects[Index].name}",
							(float)Index / Selection.objects.Length);
						if (Selection.objects[Index] is not AnimatorController) continue;
						AnimatorController TargetAnimator = Selection.objects[Index] as AnimatorController;
						if (TargetAnimator.name.EndsWith("Original")) continue;
						if (ModifyWriteDefaults(TargetAnimator, false)) {
							ModifiedCount++;
						}
					}
				} finally {
					EditorUtility.ClearProgressBar();
					if (ModifiedCount > 0) {
						AssetDatabase.SaveAssets();
						AssetDatabase.Refresh();
					}
				}
				Debug.Log($"[Macchiato] Modified write defaults off in {ModifiedCount} animator controllers");
			}
		}

		public static bool ModifyWriteDefaults(AnimatorController TargetAnimator, bool TargetWriteDefaults) {
			AnimatorState[] AllAnimatorStates = AnimatorHelper.GetAllAnimatorStates(TargetAnimator);
			bool IsModified = false;
			foreach (AnimatorState TargetState in AllAnimatorStates) {
				if (TargetState.writeDefaultValues != TargetWriteDefaults) {
					TargetState.writeDefaultValues = TargetWriteDefaults;
					EditorUtility.SetDirty(TargetState);
					IsModified = true;
				}
			}
			return IsModified;
		}
	}
}