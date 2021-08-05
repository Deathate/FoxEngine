
using System.Collections.Generic;
using System;
namespace FoxEngine {
    public class FoxCache {
        public static Dictionary<string, UnityEngine.Object> dict = new Dictionary<string, UnityEngine.Object>();
        public static bool Contains(string s) => dict.ContainsKey(s);
        public static void Register(string s, UnityEngine.Object obj) => dict.Add(s, obj);
        public static T GetOrCreate<T>(string s, Func<UnityEngine.Object> obj) {
            if (!Contains(s)) Register(s, obj?.Invoke());
            return (T)(object)dict[s];
        }
    }
}