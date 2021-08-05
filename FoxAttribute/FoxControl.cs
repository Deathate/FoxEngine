
using UnityEngine;

namespace FoxEngine.detail
{
    class AutoInitializeClass
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //static void OnBeforeSceneLoadRuntimeMethod()
        //{
        //    Debug.Log("Before first Scene loaded");
        //}

        /// <summary>
        /// After first Scene loaded
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnAfterSceneLoadRuntimeMethod()
        {
            FoxControl.Init();
        }

        //[RuntimeInitializeOnLoadMethod]
        //static void OnRuntimeMethodLoad()
        //{
        //    SceneManager.GetActiveScene().buildIndex.Print();
        //}
    }

    public class FoxControl : MonoBehaviour
    {
        public static void Init()
        {
            GameObject FoxControler = null;
            if (FoxControler == null)
            {
                FoxControler = new GameObject();
                FoxControler.name = "FoxControl";
                if (GlobalAttributeField(FoxControler) == 0)
                {
                    Destroy(FoxControler);
                }
                else
                {
                    DontDestroyOnLoad(FoxControler);
                }

            }
        }
        public static int GlobalAttributeField(GameObject go)
        {
            var foxResource = Resources.Load("FoxResources") as FoxScriptable;

            foreach (var r in foxResource.fieldsForGlobalAttribute)
            {
                go.AddComponent(FoxUtility.GetType(r));
            }

            return foxResource.fieldsForGlobalAttribute.Count;
        }
    }
}


