
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using FoxEngine;

public class SelectByComponent : EditorWindow {
    public enum _Selector {
        SpriteRenderer,
        ParticleSystem,
    }
    public _Selector selector;
    public string type_name;

    [MenuItem("FoxTool/Select All Type")]
    static void Init() {
        GetWindow<SelectByComponent>("FoxTool/SelectByComponent");
    }

    private void OnGUI() {

        selector = (_Selector)EditorGUILayout.EnumPopup(selector);
        type_name = EditorGUILayout.TextField(type_name);

        if (GUILayout.Button("Apply")) {
            Type type = typeof(Transform);
            if (!string.IsNullOrEmpty(type_name))
                type = FoxUtility.GetType(type_name);
            else {
                switch (selector) {
                    case _Selector.SpriteRenderer:
                        type = typeof(SpriteRenderer);
                        break;
                    case _Selector.ParticleSystem:
                        type = typeof(ParticleSystem);
                        break;
                }
            }
            if (Selection.gameObjects != null) {
                var selections = GetAllChildren(Selection.gameObjects[0].transform, type);
                if (selections.Count != 0) {
                    Selection.objects = selections.ToArray();
                }
            } else
                Debug.Log("Must Select A GameObject");
        }
    }

    private void OnInspectorUpdate() {
        Repaint();
    }

    List<GameObject> GetAllChildren(Transform parent, Type type, List<GameObject> transformList = null) {
        if (transformList == null) transformList = new List<GameObject>();

        foreach (Transform child in parent) {
            if (child.GetComponent(type))
                transformList.Add(child.gameObject);
            GetAllChildren(child, type, transformList);
        }
        return transformList;
    }
}
