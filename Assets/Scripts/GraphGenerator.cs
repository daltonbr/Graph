using System.Collections;
using UnityEditor;
using UnityEngine;

public class GraphGenerator : ScriptableObject
{
    [MenuItem("Assets/Create/My Graph")]
    public static void CreateGraph()
    {
        Graph graphAsset = ScriptableObject.CreateInstance<Graph>();

        AssetDatabase.CreateAsset(graphAsset, "Assets/Graphs/Graph.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = graphAsset;
    }

}
