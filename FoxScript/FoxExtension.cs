
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using RuntimePresets;

namespace FoxEngine {
    public static class DataTypeExtension {

        public static RectTransform rectTransform(this Transform t) => t as RectTransform;

        public static bool Print<T>(this T i) {
            MonoBehaviour.print(i);
            return true;
        }

        public static bool Debug<T>(this T i) {
            UnityEngine.Debug.Log(i);
            return true;
        }

        public static bool Print(this v3 i) {
            MonoBehaviour.print((Vector3)i);
            return true;
        }

        public static int atoi(this bool b) => Convert.ToInt32(b);
        public static int atoi(this float b) => Convert.ToInt32(b);
        public static bool atob(this int i) => i != 0;
        public static float Abs(this float i) => Mathf.Abs(i);
        public static float Clamp(this float i, float a, float b) => Mathf.Clamp(i, a, b);
        public static void Condition(this Action act, bool b) {
            if (b) act?.Invoke();
        }
        public static float Lerp(this float me, float target, float t, bool hasBlock = false, float block = 0, float limit = 0.01f) {
            if (Mathf.Abs(target - me) < limit) return target;

            float a = Mathf.Clamp01(t) * 10;
            float f = ((10 - a) * me + a * target) / 10;
            if (hasBlock && Mathf.Abs(block - me) < Mathf.Abs(f - me))
                f = block;
            return f;
        }

        public static Vector3 Lerp(this Vector3 me, Vector3 target, float t, bool hasBlock = false, Vector3 block = default, float limit = 0.01f) {
            return new Vector3(me.x.Lerp(target.x, t, hasBlock, block.x, limit), me.y.Lerp(target.y, t, hasBlock, block.y, limit), me.z.Lerp(target.z, t, hasBlock, block.z, limit));
        }

        public static Transform AlignOnAxisX(this Transform i, Transform v) {
            if (v == null) return null;
            Vector3 pos = i.position;
            pos.x = v.position.x;
            i.position = pos;
            return i;
        }
        public static Transform AlignOnAxisY(this Transform i, Transform v) {
            if (v == null) return null;
            Vector3 pos = i.position;
            pos.y = v.position.y;
            i.position = pos;
            return i;
        }
        public static Transform AlignOnAxisZ(this Transform i, Transform v) {
            if (v == null) return null;
            Vector3 pos = i.position;
            pos.z = v.position.z;
            i.position = pos;
            return i;
        }
        public static void SetColor(this GradientColorKey[] key, int numbering, Color color, float time) {
            key[numbering].color = color;
            key[numbering].time = time;
        }
        public static void SetAlpha(this GradientAlphaKey[] key, int numbering, float alpha, float time) {
            key[numbering].alpha = alpha;
            key[numbering].time = time;
        }

        public static void CopySpecialComponentFrom<T>(this GameObject _targetGO, GameObject _sourceGO) where T : Component {
            var sourceComp = _sourceGO.GetComponent<T>();
            var targetComp = _targetGO.GetOrCreateComponent(sourceComp.GetType());
            targetComp.TransferValuesFrom(sourceComp);
        }

        public static Vector3 dir(this Quaternion quaternion) {
            return quaternion * Vector3.forward;
        }
    }

    public static class ClassExtension {
        public static int toLayer(this LayerMask layerMask) {
            return (int)Mathf.Log(layerMask.value, 2);
        }
        public static Transform FindDeep(this Transform aParent, string aName) {
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(aParent);
            while (queue.Count > 0) {
                var c = queue.Dequeue();
                if (c.name == aName)
                    return c;
                foreach (Transform t in c)
                    queue.Enqueue(t);
            }
            return null;
        }
    }
    public static class GameObjectExtensions {
        public static Component GetOrCreateComponent(this GameObject target, Type type) {
            var component = target.GetComponent(type);

            if (component == null) {
                component = target.AddComponent(type);
            }

            return component;
        }

        public static T GetOrCreateComponent<T>(this GameObject target) where T : Component {
            return (T)target.GetOrCreateComponent(typeof(T));
        }

    }

    public static class ListExtension {
        public static bool Print<T>(this List<T> l) {
            if (l == null) {
                MonoBehaviour.print("NULL");
                return true;
            }
            foreach (var r in l)
                MonoBehaviour.print(r);

            return true;
        }
        public static IEnumerable<(T, int)> Enumerate<T>(this IEnumerable<T> input, int start = 0) {
            int i = start;
            foreach (var t in input) {
                yield return (t, i++);
            }
        }
        public static List<int> FindAllIndexof<T>(this IEnumerable<T> values, T val) {
            var v = values.Select((key, i) => Equals(key, val) ? i : -1).Where(x => x != -1).ToList();
            return v.Count > 0 ? v : null;
        }
        public static void ForEach<T>(this T[] values, Action<T> action) {
            foreach (var r in values) {
                action(r);
            }
        }
    }

    public static class CoroutineExtension {
        public static Coroutine Run(this IEnumerator enumerator) {
            return FoxUtility.AddMotion(enumerator, 0);
        }

        public static void Stop(this Coroutine coroutine) {
            FoxUtility.StopMotion(coroutine);
        }
    }

    public struct v2 {
        float x, y;
        public v2(float x, float y) {
            this.x = x;
            this.y = y;
        }
        public v2(Vector2 v) {
            this.x = v.x;
            this.y = v.y;
        }
        public v2 SetX(float f) {
            x = f;
            return this;
        }
        public v2 SetY(float f) {
            y = f;
            return this;
        }
        public v2 AltX(float f) {
            x *= f;
            return this;
        }
        public v2 AltY(float f) {
            y *= f;
            return this;
        }

        public static v2 mk(float x, float y) {
            return new v2(x, y);
        }
        public static v2 mk(Vector2 v) {
            return new v2(v.x, v.y);
        }
        public static v2 mk(Vector3 v) {
            return new v2(v.x, v.y);
        }

        public override string ToString() {
            return String.Format("( {0}, {1} )", this.x, this.y);
        }

        public static implicit operator Vector2(v2 v) {
            return new Vector2(v.x, v.y);
        }
        public static v2 operator *(v2 a, float b) {
            return new v2(a.x * b, a.y * b);
        }
        public static v2 operator *(float b, v2 a) {
            return new v2(a.x * b, a.y * b);
        }
        public static v2 operator +(v2 a, v2 b) {
            return new v2(a.x + b.x, a.y + b.y);
        }
        public static v2 operator /(v2 a, float f) {
            return new v2(a.x / f, a.y / f);
        }
        #region ShortHand
        /// <summary>
        /// Vector2(0,0)
        /// </summary>
        public static v2 zero { get { return new v2(0, 0); } }
        /// <summary>
        /// Vector2(1,1)
        /// </summary>
        public static v2 one { get { return new v2(1, 1); } }
        /// <summary>
        /// Vector2(0,1)
        /// </summary>
        public static v2 up { get { return new v2(0, 1); } }
        /// <summary>
        /// Vector2(0,-1)
        /// </summary>
        public static v2 down { get { return new v2(0, -1); } }
        /// <summary>
        /// Vector2(-1,0)
        /// </summary>
        public static v2 left { get { return new v2(-1, 0); } }
        /// <summary>
        /// Vector2(1,0)
        /// </summary>
        public static v2 right { get { return new v2(1, 0); } }
        #endregion
    }
    public struct v3 {
        public float x, y, z;
        public v3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public v3(Vector3 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }
        public v3(Vector2 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = 0;
        }
        public v3 SetX(float f) {
            x = f;
            return this;
        }
        public v3 SetY(float f) {
            y = f;
            return this;
        }
        public v3 SetZ(float f) {
            z = f;
            return this;
        }
        public v3 AltX(float f) {
            x *= f;
            return this;
        }
        public v3 AltY(float f) {
            y *= f;
            return this;
        }
        public v3 AltZ(float f) {
            z *= f;
            return this;
        }

        public static v3 mk(float x, float y, float z) {
            return new v3(x, y, z);
        }
        public static v3 mk(Vector3 vector) {
            return new v3(vector);
        }
        public static v3 mk(Vector2 vector) {
            return new v3(vector);
        }

        public static implicit operator Vector3(v3 v) {
            return new Vector3(v.x, v.y, v.z);
        }
        public static implicit operator Vector2(v3 v) {
            return new Vector3(v.x, v.y, 0);
        }
        public static v3 operator *(v3 a, float b) {
            return new v3(a.x * b, a.y * b, a.z * b);
        }
        public static v3 operator *(Vector3 a, v3 b) {
            return new v3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static v3 operator *(v3 a, Vector3 b) {
            return new v3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static v3 operator +(v3 a, v3 b) {
            return new v3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        #region ShortHand
        /// <summary>
        /// Vector3(0,0,1)
        /// </summary>
        public static v3 forward { get { return new v3(0, 0, 1); } }
        /// <summary>
        /// Vector3(0,0,-1)
        /// </summary>
        public static v3 back { get { return new v3(0, 0, -1); } }
        /// <summary>
        /// Vector3(0,1,0)
        /// </summary>
        public static v3 up { get { return new v3(0, 1, 0); } }
        /// <summary>
        /// Vector3(0,-1,0)
        /// </summary>
        public static v3 down { get { return new v3(0, -1, 0); } }
        /// <summary>
        /// Vector3(1,0,0)
        /// </summary>
        public static v3 right { get { return new v3(1, 0, 0); } }
        /// <summary>
        /// Vector3(-1,0,0)
        /// </summary>
        public static v3 left { get { return new v3(-1, 0, 0); } }
        /// <summary>
        /// Vector3(0,0,0)
        /// </summary>
        public static v3 zero { get { return new v3(0, 0, 0); } }
        /// <summary>
        /// Vector3(1,1,1)
        /// </summary>
        public static v3 one { get { return new v3(1, 1, 1); } }
        /// <summary>
        /// Vector3(1,1,0)
        /// </summary>
        public static v3 forwardR { get { return new v3(1, 1, 0); } }
        /// Vector3(1,0,1)
        /// </summary>
        public static v3 upR { get { return new v3(1, 0, 1); } }
        /// <summary>
        /// Vector3(0,1,1)
        /// </summary>
        public static v3 rightR { get { return new v3(0, 1, 1); } }
        #endregion
    }

    public static class VectorExtension {
        public static Vector3 SetX(this Vector3 v, float f) {
            v.x = f;
            return v;
        }

        public static Vector3 SetY(this Vector3 v, float f) {
            v.y = f;
            return v;
        }

        public static Vector3 SetZ(this Vector3 v, float f) {
            v.z = f;
            return v;
        }

        public static Vector3 AltX(this Vector3 v, float f) {
            v.x *= f;
            return v;
        }

        public static Vector3 AltY(this Vector3 v, float f) {
            v.y *= f;
            return v;
        }

        public static Vector3 AltZ(this Vector3 v, float f) {
            v.z *= f;
            return v;
        }

        public static Vector2 SetX(this Vector2 v, float f) {
            v.x = f;
            return v;
        }

        public static Vector2 SetY(this Vector2 v, float f) {
            v.y = f;
            return v;
        }

        public static Vector2 AltX(this Vector2 v, float f) {
            v.x *= f;
            return v;
        }

        public static Vector2 AltY(this Vector2 v, float f) {
            v.y *= f;
            return v;
        }
    }

    public class Method {
        public static int size(object o) {
            return System.Runtime.InteropServices.Marshal.SizeOf(o);
        }
    }
}
