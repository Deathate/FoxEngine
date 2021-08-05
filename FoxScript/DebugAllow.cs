﻿
using UnityEngine;

public class DebugAllow : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
             Debug.unityLogger.logEnabled=false;
#endif
    }
}
