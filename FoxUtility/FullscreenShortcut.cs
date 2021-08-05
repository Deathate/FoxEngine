
using UnityEngine;
using UnityEditor;

public class FullscreenShortcut : EditorWindow {

    [MenuItem("Edit/Maximize")]
    public static void CopyAction() {
        EditorWindow.focusedWindow.maximized = !EditorWindow.focusedWindow.maximized;
    }

    private void OnInspectorUpdate() {
        Repaint();
    }
}
