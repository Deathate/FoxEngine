
using UnityEngine;
using UnityEditor;
using System;
using FoxEngine;

[InitializeOnLoad]
class FoxEditor
{
    static FoxEditor()
    {
        RegistGlobalAttributeToEngine();
    }

    static void RegistGlobalAttributeToEngine()
    {
        string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { "Assets" });
        var foxResouces = Resources.Load("FoxResources") as FoxScriptable;
        foxResouces.fieldsForGlobalAttribute.Clear();

        foreach (string guid in guids)
        {
            string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
            if (myObjectPath.Contains(".cs"))
            {
                UnityEngine.Object[] myObjs = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);

                foreach (UnityEngine.Object thisObject in myObjs)
                {
                    var obj = thisObject as MonoScript;
                    if (obj != null && obj.GetClass() != null)
                    {
                        GlobalAttribute attribute = Attribute.GetCustomAttribute(obj.GetClass(), typeof(GlobalAttribute)) as GlobalAttribute;

                        if (attribute != null)
                        {
                            foxResouces.GlobalAttributeRegister(obj.name);
                        }
                    }
                }
            }
        }
    }
}
