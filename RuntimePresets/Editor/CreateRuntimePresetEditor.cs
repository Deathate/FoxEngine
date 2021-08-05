
using UnityEngine;
using UnityEditor;

namespace RuntimePresets
{
    public class CreateRuntimePresetEditor
    {
        [MenuItem("CONTEXT/Component/Create Runtime Preset")]
        private static void CreateRuntimePreset(MenuCommand command)
        {
            var sourceComponent = (Component)command.context;
            var dummyObject = new GameObject();

            var targetComponent = dummyObject.GetOrCreateComponent(sourceComponent.GetType());
            var runtimePreset = dummyObject.GetOrCreateComponent<Preset>();
            runtimePreset.TargetComponent = targetComponent;

            UnityEditorInternal.ComponentUtility.CopyComponent(sourceComponent);
            UnityEditorInternal.ComponentUtility.PasteComponentValues(targetComponent);

            PrefabUtility.SaveAsPrefabAsset(dummyObject, "Assets/New Preset.prefab");
            Object.DestroyImmediate(dummyObject);
        }
    }
}