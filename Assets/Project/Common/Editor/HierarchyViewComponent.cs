using System.Linq;
using UnityEditor;
using UnityEngine;

//コピペなのでコメントはないです

public static class HierarchyViewComponent{
	private static readonly Color mDisabledColor = new Color(1, 1, 1, 0.5f);

	private const int WIDTH = 16;
	private const int HEIGHT = 16;

	[InitializeOnLoadMethod]
	private static void Component() {
		EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
	} //End_Method

	private static void OnGUI(int instanceID, Rect selectionRect) {
		var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

		if (go == null) {
			return;
		} //End_If

		var pos = selectionRect;
		pos.x = pos.xMax - WIDTH;
		pos.width = WIDTH;
		pos.height = HEIGHT;
		pos.x -= pos.width;

		var components = go
			.GetComponents<Component>()
			.Where(c => c != null)
			.Where(c => !(c is Transform))
			.Reverse();

		var current = Event.current;

		foreach (var c in components) {
			Texture image = AssetPreview.GetMiniThumbnail(c);

			if (image == null && c is MonoBehaviour) {
				var ms = MonoScript.FromMonoBehaviour(c as MonoBehaviour);
				var path = AssetDatabase.GetAssetPath(ms);
				image = AssetDatabase.GetCachedIcon(path);
			} //End_If

			if (image == null) {
				continue;
			} //End_If

			if (current.type == EventType.MouseDown &&
				 pos.Contains(current.mousePosition)) {
				c.SetEnable(!c.IsEnabled());
			} //End_If

			var color = GUI.color;
			GUI.color = c.IsEnabled() ? Color.white : mDisabledColor;
			GUI.DrawTexture(pos, image, ScaleMode.ScaleToFit);
			GUI.color = color;
			pos.x -= pos.width;
		} //End_Foreach
	} //End_Method

	public static bool IsEnabled(this Component self) {
		if (self == null) {
			return true;
		} //End_If

		var type = self.GetType();
		var property = type.GetProperty("enabled", typeof(bool));

		if (property == null) {
			return true;
		} //End_If

		return (bool)property.GetValue(self, null);
	} //End_Method

	public static void SetEnable(this Component self, bool isEnabled) {
		if (self == null) {
			return;
		} //End_If

		var type = self.GetType();
		var property = type.GetProperty("enabled", typeof(bool));

		if (property == null) {
			return;
		} //End_If

		property.SetValue(self, isEnabled, null);
	} //End_Method
} //End_Class