using UnityEditor;
using UnityEngine;

using static Macchiato.Core.Translator;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	public class GUIDUtility : EditorWindow {

		public string TargetGUID = string.Empty;
		public string AssetPath = string.Empty;

		const float BorderX = 30f;

		[MenuItem("Assets/Macchiato/Asset/Copy GUID", priority = 1000)]
		static void CopyGUID() {
			if (Selection.assetGUIDs != null) {
				if (Selection.assetGUIDs.Length == 1) {
					GUIUtility.systemCopyBuffer = Selection.assetGUIDs[0];
				} else if (Selection.assetGUIDs.Length > 1) {
					GUIUtility.systemCopyBuffer = string.Join("\n", Selection.assetGUIDs);
				}
			}
		}

		[MenuItem("Tools/Macchiato/Utility/GUIDUtility", priority = 1000)]
		static void CreateWindow() {
			GUIDUtility AppWindow = GetWindow<GUIDUtility>(true, "Macchiato GUIDUtility", true);
			AppWindow.minSize = new Vector2(450, 140);
			AppWindow.maxSize = new Vector2(750, 140);
		}

		void OnGUI() {
			GUIStyle BoldCenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			float OriginalLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUIUtility.labelWidth = 100f;
			int OldLanguageIndex = LanguageIndex;
			LanguageIndex = EditorGUILayout.Popup(GetTranslatedString("String_Language"), LanguageIndex, LanguageOption);
			if (LanguageIndex != OldLanguageIndex) AssetPath = string.Empty;
			EditorGUIUtility.labelWidth = OriginalLabelWidth;
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label(GetTranslatedString("String_InputGUID"), BoldCenteredStyle);
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			TargetGUID = GUILayout.TextField(TargetGUID, GUILayout.Width(GetFieldWidth(position.size)));
			if (GUILayout.Button(GetTranslatedString("String_Browse"), GUILayout.Width(100f))) {
				AssetPath = GetAssetPath(TargetGUID);
			}
			GUILayout.Space(BorderX);
			EditorGUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label(AssetPath, CenteredStyle);
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
		}

		string GetAssetPath(string TargetGUID) {
			string NewAssetPath = AssetDatabase.GUIDToAssetPath(TargetGUID);
			if (string.IsNullOrEmpty(NewAssetPath)) NewAssetPath = GetTranslatedString("NO_GUID");
			return NewAssetPath;
		}

		float GetFieldWidth(Vector2 WindowSize) {
			return WindowSize.x - ((BorderX * 2f) + 100f + 7f);
		}
	}
}