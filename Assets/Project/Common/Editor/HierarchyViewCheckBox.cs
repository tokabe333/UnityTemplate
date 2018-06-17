using UnityEditor;
using UnityEngine;

//コピペなのでコメントはないです

public static class HierarchyViewCheckBox {
	private const int WIDTH = 16;

	[InitializeOnLoadMethod]
	private static void CheckBox() {
		EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
	} //End_Method

	private static void OnGUI(int instanceID, Rect selectionRect) {
		var pos = selectionRect;
		pos.x = pos.xMax - WIDTH;
		pos.width = WIDTH;

		var oldSelected = Selection.Contains(instanceID);
		var newSelected = GUI.Toggle(pos, oldSelected, string.Empty);

		if (newSelected == oldSelected) {
			return;
		} //End_If

		var instanceIDs = Selection.instanceIDs;

		if (newSelected) {
			ArrayUtility.Add(ref instanceIDs, instanceID);
		} //End_If
		else {
			ArrayUtility.Remove(ref instanceIDs, instanceID);
		} //End_If

		Selection.instanceIDs = instanceIDs;
	} //End_Method
} //End_Class