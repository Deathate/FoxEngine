
using UnityEngine;
using UnityEditor;

public class QuickTransformer : EditorWindow {
    public static GameObject go;

    [MenuItem("Window/QuickTransformer/Copy")]
    public static void CopyAction() {
        if (Selection.gameObjects == null || Selection.gameObjects.Length == 0) {
            go = null;
            return;
        }
        if (Selection.gameObjects.Length > 1) {
            Debug.LogError("QuickTransfromer: error: Only one object can be selected");
            return;
        }
        go = Selection.gameObjects[0];
        Debug.Log("QuickTransfromer: Action: " + go.name + "is selected");
    }

    [MenuItem("Window/QuickTransformer/Paste")]
    public static void PasteAction() {
        if (go == null || Selection.gameObjects == null || Selection.gameObjects.Length == 0) {
            return;
        }
        if (Selection.gameObjects.Length > 1) {
            Debug.LogError("QuickTransfromer: error: Only one object can be selected");
            return;
        }

        var tmp = Selection.gameObjects[0];

        tmp.transform.position = go.transform.position;
        tmp.transform.rotation = go.transform.rotation;
        tmp.transform.localScale = go.transform.localScale;
        Debug.Log("QuickTransfromer: Action: " + tmp.name + "'s transform has been changed");
    }

    [MenuItem("Window/QuickTransformer/Debug")]
    public static void Init() {
        GetWindow<QuickTransformer>("Debug");
    }

    private void OnGUI() {
        EditorGUILayout.ObjectField("From", go, typeof(GameObject), true);
    }

    private void OnInspectorUpdate() {
        Repaint();
    }
}
