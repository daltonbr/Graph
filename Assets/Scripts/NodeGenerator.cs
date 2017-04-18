using UnityEngine;
using System.Collections;
using UnityEditor;

public class NodeGenerator
{
	[MenuItem("Assets/Create/My Node")]
	public static void CreateNode()
	{
		Node nodeAsset = ScriptableObject.CreateInstance<Node>();

		AssetDatabase.CreateAsset(nodeAsset, "Assets/Node.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = nodeAsset;
	}
}