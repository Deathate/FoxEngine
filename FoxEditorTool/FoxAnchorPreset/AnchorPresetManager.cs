
using UnityEngine;
using FoxEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace FoxAnchorPreset {
    [ExecuteAlways]
    public class AnchorPresetManager : MonoBehaviour {

        public List<AnchorPreset> list;

        private void Start() {
#if !UNITY_EDITOR
            foreach (var item in list) {
                item.Calculate();
            }
#endif
        }

        private void Update() {
            foreach (var item in list) {
                item.Calculate();
            }
        }
    }
}
