
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using RuntimePresets;

namespace FoxEngine {
    public class FoxUtility {
        #region  class helper
        class HideInFox : MonoBehaviour {
            public static void MakeCube(Vector3 v) {
                GameObject cube = Resources.Load<GameObject>("Cube");
                Instantiate(cube, v, Quaternion.identity);
            }

            public static IEnumerator AddMotion(Action act, float time) {
                yield return new WaitForSeconds(time);
                act();
            }
            public static IEnumerator AddMotion(IEnumerator act, float time) {
                yield return new WaitForSeconds(time);
                yield return act;
            }

            //public static IEnumerator AddMotionEx(BetterEventEntry act, float time)
            //{
            //    yield return new WaitForSeconds(time);
            //    act.Invoke();
            //}
        }
        #endregion

#pragma warning disable 0649
        static HideInFox Instance => instance ? instance : instance = new GameObject("FoxInstance").AddComponent<HideInFox>();
        static HideInFox instance;
        public static GameObject GetInstance() {
            return Instance.gameObject;
        }

        public static void DoMotion(Action action, float t = 0) {
            if (action == null) return;
            Instance.StartCoroutine(HideInFox.AddMotion(action, t));
        }

        public static Coroutine AddMotion(IEnumerator action, float t = 0) {
            return Instance.StartCoroutine(HideInFox.AddMotion(action, t));
        }

        public static void StopMotion(Coroutine coroutine) {
            if (coroutine != null)
                Instance.StopCoroutine(coroutine);
        }

        //public static void DoMotionEx(BetterEventEntry action, float t)
        //{
        //    if (action == null) return;
        //    if (!instance)
        //        instance = (new GameObject()).AddComponent<FoxUtility>();
        //    instance.StartCoroutine(HideInFox.AddMotionEx(action, t));
        //}

        public static (bool, RaycastHit) EasyRayCast(Ray ray) {
            RaycastHit hit;
            return (Physics.Raycast(ray, out hit), hit);
        }

        public static (bool, RaycastHit) EasyRayCast(Ray ray, LayerMask layerMask) {
            RaycastHit hit;
            return (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask), hit);
        }

        public static (bool, RaycastHit2D) EasyRayCast2D(Ray ray) {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            return (hit.collider != null, hit);
        }

        public static (bool, RaycastHit2D) EasyRayCast2D(Ray ray, LayerMask layerMask) {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            return (hit.collider != null, hit);
        }

        public static (int, RaycastHit[]) EasyRayCastNonAlloc(Ray ray, RaycastHit[] raycastHits) {
            return (Physics.RaycastNonAlloc(ray, raycastHits), raycastHits);
        }

        /// <summary>
        /// 0 is x axis, 1 is y axis, 2 is z axis
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 PointForVirtualLine(Ray ray, int axis, float value) {
            Vector3 v = default;
            Vector3 start = ray.origin;
            Vector3 toward = ray.direction;
            float para;
            switch (axis) {
                case 0:
                    para = (value - start.x) / toward.x;
                    v = start + toward * para;
                    break;
                case 1:
                    para = (value - start.y) / toward.y;
                    v = start + toward * para;
                    break;
                case 2:
                    para = (value - start.z) / toward.z;
                    v = start + toward * para;
                    break;
            }
            return v;
        }

        public static Transform Canvas => FoxCache.GetOrCreate<Transform>("canvas_fox", () => GameObject.Find("Canvas").transform);

        public static Camera Camera => FoxCache.GetOrCreate<Camera>("camera_fox", () => GameObject.FindObjectOfType(typeof(Camera)));

        public static Type GetType(string name) {
            var targetType = Type.GetType(name);

            if (targetType != null) return targetType;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes()) {
                    if (type.Name == name) {
                        return type;
                    }
                }
            }

            return null;
        }

        public static float Distance(Transform a, Transform b) {
            return Vector3.Distance(a.position, b.position);
        }

        public static float Distance(Transform a, Vector3 b) {
            return Vector3.Distance(a.position, b);
        }

        public static float Distance(Vector3 a, Transform b) {
            return Vector3.Distance(a, b.position);
        }

        public static float Distance(Vector3 a, Vector3 b = default) {
            return Vector3.Distance(a, b);
        }

        public static Vector3 vector(Vector3 a, Vector3 b) {
            return (b - a);
        }

        public static Vector3 vector(Transform a, Transform b) {
            return (b.position - a.position);
        }

        public static Vector3 vector(Transform a, Vector3 b) {
            return (b - a.position);
        }

        public static Vector3 vector(Vector3 a, Transform b) {
            return (b.position - a);
        }

        public static bool isInDis(Vector3 a, Vector3 b, float f) {
            return (b - a).sqrMagnitude < f * f;
        }

        public static bool isInDis(Vector3 a, Transform b, float f) {
            return (b.position - a).sqrMagnitude < f * f;
        }

        public static bool isInDis(Transform a, Vector3 b, float f) {
            return (b - a.position).sqrMagnitude < f * f;
        }

        public static bool isInDis(Transform a, Transform b, float f) {
            return (b.position - a.position).sqrMagnitude < f * f;
        }

        public static void MakeCube(Vector3 v) {
            HideInFox.MakeCube(v);
        }

        public static GameObject Instantiate(GameObject g) {
            return HideInFox.Instantiate(g);
        }

        public static GameObject Instantiate(GameObject g, Vector3 vector3, Quaternion quaternion) {
            return HideInFox.Instantiate(g, vector3, quaternion);
        }

        public static GameObject Instantiate(GameObject g, Vector3 vector3) {
            return HideInFox.Instantiate(g, vector3, g.transform.rotation);
        }

        public static void Destroy(GameObject go) {
            HideInFox.Destroy(go);
        }

        public static void DestroyImmediate(GameObject go) {
            HideInFox.DestroyImmediate(go);
        }

        public static float AngleOfLine(Vector2 vel) {
            return Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
        }

        public static string AnimationStateName(string layer, string animation_name) {
            return layer + "." + animation_name;
        }

        public static Color HexColor(string name) {
            Color color;
            if (!ColorUtility.TryParseHtmlString(string.Format("#{0}", name), out color)) {
                Debug.LogAssertion("Wrong Hex Value");
            }
            return color;
        }

        public static void ReplaceAllCompWithB(GameObject A, GameObject B) {
            // B.transform.SetParent(A.transform.parent);
            var temp = HideInFox.Instantiate(B);
            foreach (var i in temp.GetComponents<Component>()) {
                A.GetOrCreateComponent(i.GetType()).TransferValuesFrom(i);
            }
#if UNITY_EDITOR
            DestroyImmediate(temp);
#else
            Destroy(temp);
#endif
        }
        public static void ReplaceAllCompWithB(Transform A, Transform B) {
            ReplaceAllCompWithB(A.gameObject, B.gameObject);
        }
    }
    #region CustomClassAssembly

    [System.Serializable]
    public struct FoxScene {
        public UnityEngine.Object scene;
        public string name { get => scene.name; }
        public static implicit operator UnityEngine.Object(FoxScene obj) {
            return obj.scene;
        }
    }

    [System.Serializable]
    public class FoxText {
        public GameObject myText;
        int id = -1;
        MonoBehaviour mono;

        public string text {
            set {
                Init();
                if (id == 0)
                    ((Text)mono).text = value;
                else if (id == 1)
                    ((TMPro.TextMeshProUGUI)mono).text = value;
            }
            get {
                Init();
                if (id == 0)
                    return ((Text)mono).text;
                else if (id == 1)
                    return ((TMPro.TextMeshProUGUI)mono).text;
                return null;
            }
        }

        void Init() {
            if (mono == null) {
                var textObj = myText.GetComponent<Text>();
                var uguiObj = myText.GetComponent<TMPro.TextMeshProUGUI>();
                if (textObj != null) {
                    mono = textObj;
                    id = 0;
                } else if (uguiObj != null) {
                    mono = uguiObj;
                    id = 1;
                }
            }
        }
    }

    #endregion
}
