using System.IO;
using UnityEditor;
using UnityEngine;

//コピペなのでコメントはないです

public static class Extension{
	private static readonly Color mColor = new Color(0.635f, 0.635f, 0.635f, 1);

	[InitializeOnLoadMethod]
	private static void ProjectView() {
		EditorApplication.projectWindowItemOnGUI += OnGUI;
	} //End_Method

	private static void OnGUI(string guid, Rect selectionRect) {
		var path = AssetDatabase.GUIDToAssetPath(guid);
		var extension = Path.GetExtension(path);

		if (string.IsNullOrEmpty(extension)) {
			return;
		} //End_If

		var label = EditorStyles.label;
		var content = new GUIContent(extension);
		var width = label.CalcSize(content).x;

		var pos = selectionRect;
		pos.x = pos.xMax - width;
		pos.width = width;
		pos.yMin++;

		var color = GUI.color;
		GUI.color = mColor;
		GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
		GUI.color = color;
		GUI.Label(pos, extension);
	} //End_Method
} //End_Class