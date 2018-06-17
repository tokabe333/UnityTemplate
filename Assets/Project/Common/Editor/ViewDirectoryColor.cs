using UnityEditor;
using UnityEngine;

//コピペなのでコメントはないです

public static class ViewDirectoryColor{
	[InitializeOnLoadMethod]
	private static void Hierarchy() {
		EditorApplication.hierarchyWindowItemOnGUI += OnGuiHierachy;
		EditorApplication.projectWindowItemOnGUI += OnGuiProject;
	} //End_Method

	private static void OnGuiHierachy(int instanceID, Rect selectionRect) {
		var index = (int)(selectionRect.y - 4) / 16;

		if (index % 2 == 0) {
			return;
		} //End_If

		var pos = selectionRect;
		pos.x = 0;
		pos.xMax = selectionRect.xMax;

		var color = GUI.color;
		GUI.color = new Color(0, 0, 0, 0.1f);
		GUI.Box(pos, string.Empty);
		GUI.color = color;
	} //End_Method


	private static void OnGuiProject(string guid, Rect selectionRect) {
		var index = (int)(selectionRect.y - 0) / 16;

		if (index % 2 == 0) {
			return;
		} //End_If

		var pos = selectionRect;
		pos.x = 0;
		pos.xMax = selectionRect.xMax;

		var color = GUI.color;
		GUI.color = new Color(0, 0, 0, 0.066f);
		GUI.Box(pos, string.Empty);
		GUI.color = color;
	} //End_Method
} //End_Class