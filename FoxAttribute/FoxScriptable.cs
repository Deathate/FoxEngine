
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxResources", menuName = "FoxEngine/FoxResouces")]
public class FoxScriptable : ScriptableObject
{
    public List<string> fieldsForGlobalAttribute;
    public void GlobalAttributeRegister(string s)
    {
        if (!fieldsForGlobalAttribute.Contains(s))
            fieldsForGlobalAttribute.Add(s);
    }
}
