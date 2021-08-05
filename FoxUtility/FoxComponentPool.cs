
using UnityEngine;
using System.Collections.Generic;
using System;

namespace FoxEngine {
    public class FoxObjectPool {
        static Dictionary<string, FoxObjectPool> regedit = new Dictionary<string, FoxObjectPool>();
        #region Singleton
        GameObject main;
        GameObject mainInstance {
            get {
                if (main == null) {
                    main = new GameObject(name + "Pool");
                    main.transform.SetParent(FoxUtility.GetInstance().transform);
                }
                return main;
            }
        }
        #endregion
        Stack<GameObject> pool = new Stack<GameObject>();

        GameObject reference;
        string name;
        Type[] components;

        public FoxObjectPool(GameObject g) {
            reference = g;
            reference.hideFlags = HideFlags.HideInHierarchy;
            name = reference.name;
        }
        public FoxObjectPool(string name, params Type[] components) {
            this.name = name;
            this.components = components;
        }

        public static void Register(string s, GameObject g) {
            regedit.Add(s, new FoxObjectPool(g));
        }
        public static bool Contains(string s) => regedit.ContainsKey(s);
        public static FoxObjectPool Get(string s) => regedit[s];
        public static FoxObjectPool GetOrCreate(string s, GameObject g) {
            if (!Contains(s))
                Register(s, g);
            return Get(s);
        }


        public GameObject Rent() {
            if (pool.Count == 0) {
                Restitution(NewUnit());
            }
            var o = pool.Pop();
            o.SetActive(true);

            return o;
        }
        GameObject NewUnit() {
            if (reference == null) {
                reference = new GameObject("runtime", components);
                reference.hideFlags = HideFlags.HideInHierarchy;
            }
            var g = FoxUtility.Instantiate(reference);
            g.transform.SetParent(mainInstance.transform);
            return g;
        }

        public void Restitution(GameObject g) {
            g.SetActive(false);
            pool.Push(g);
        }
    }
    public class FoxComponentPool<T> where T : Behaviour {
        #region Singleton
        GameObject main;
        GameObject mainInstance {
            get {
                if (main == null) {
                    main = new GameObject(typeof(T).Name + "Pool");
                    main.transform.SetParent(FoxUtility.GetInstance().transform);
                }
                return main;
            }
        }
        #endregion
        Stack<T> pool = new Stack<T>();

        public T Rent() {
            if (pool.Count == 0) {
                Restitution(mainInstance.AddComponent<T>());
            }
            var o = pool.Pop();
            o.enabled = true;
            return o;
        }

        public void Restitution(T t) {
            t.enabled = false;
            pool.Push(t);
        }
    }
}
