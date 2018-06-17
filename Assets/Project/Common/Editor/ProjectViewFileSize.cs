using System.IO;
using UnityEditor;
using UnityEngine;

//コピペなのでコメントはないです

public static class ProjectViewFileSize {
	private const string REMOVE_STR = "Assets";

	private static int margin = 30;

	private static readonly int mRemoveCount = REMOVE_STR.Length;
	private static readonly Color mColor = new Color(0.635f, 0.635f, 0.635f, 1);

	[InitializeOnLoadMethod]
	private static void FileSize() {
		EditorApplication.projectWindowItemOnGUI += OnGUI;
	} //End_Method

	private static void OnGUI(string guid, Rect selectionRect) {
		var dataPath = Application.dataPath;
		var startIndex = dataPath.LastIndexOf(REMOVE_STR);
		var dir = dataPath.Remove(startIndex, mRemoveCount);
		var path = dir + AssetDatabase.GUIDToAssetPath(guid);

		if (!File.Exists(path)) {
			return;
		} //ENd_If

		var fileInfo = new FileInfo(path);
		var fileSize = fileInfo.Length;
		var text = GetFormatSizeString((int)fileSize);

		var label = EditorStyles.label;
		var content = new GUIContent(text);
		var width = label.CalcSize(content).x;

		var pos = selectionRect;
		pos.x = pos.xMax - width;
		pos.width = width;
		pos.yMin++;

		pos.x -= margin;

		var color = GUI.color;
		GUI.color = mColor;
		GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
		GUI.color = color;
		GUI.Label(pos, text);
	} //End_Method

	private static string GetFormatSizeString(int size) {
		return GetFormatSizeString(size, 1024);
	} //End_Method

	private static string GetFormatSizeString(int size, int p) {
		return GetFormatSizeString(size, p, "#,##0.##");
	} //End_Method

	private static string GetFormatSizeString(int size, int p, string specifier) {
		var suffix = new[] { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };
		int index = 0;

		while (size >= p) {
			size /= p;
			index++;
		} //End_While

		return string.Format(
			"{0}{1}B",
			size.ToString(specifier),
			index < suffix.Length ? suffix[index] : "-"
		);
	} //End_Method
} //End_Class