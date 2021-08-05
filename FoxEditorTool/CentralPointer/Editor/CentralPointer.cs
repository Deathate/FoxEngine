
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CentralPointer : EditorWindow {
    public GameObject go;

    [MenuItem("FoxTool/CentralPointer")]
    public static void Init() {
        GetWindow<CentralPointer>("FoxTool/CentralPointer");
    }

    private void OnGUI() {
        go = EditorGUILayout.ObjectField("From", go, typeof(GameObject), true) as GameObject;
        var center = EditorGUILayout.Vector3Field("Position", GetCenter(go));
        if (GUILayout.Button("Apply")) {
            List<Transform> children = new List<Transform>();
            foreach (Transform item in go.transform) {
                children.Add(item);
            }
            children.ForEach((x) => x.SetParent(go.transform.parent));
            go.transform.position = center;
            children.ForEach((x) => x.SetParent(go.transform));
        }

    }

    private void OnInspectorUpdate() {
        Repaint();
    }

    Vector3 GetCenter(GameObject g) {
        if (g == null) return Vector3.zero;
        Vector3 vector = Vector3.zero;
        foreach (Transform item in g.transform) {
            vector += item.position;
        }
        vector /= g.transform.childCount;
        return vector;
    }
}
