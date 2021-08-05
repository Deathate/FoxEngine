
using UnityEngine;

namespace FoxEngine.Linear {
    [ExecuteAlways]
    public class OrthogonalSizeExpand : MonoBehaviour {
        public float size;
        public Vector2 reference_resolution;

        private void Start() {
            Calculate();
#if !UNITY_EDITOR
            enabled = false;
#endif
        }

        private void Update() {
            Calculate();
        }
        void Calculate() {
            var xratio = reference_resolution.x / FoxUtility.Canvas.rectTransform().sizeDelta.x;
            var yratio = reference_resolution.y / FoxUtility.Canvas.rectTransform().sizeDelta.y;
            if (xratio > yratio) {

                FoxUtility.Camera.orthographicSize = size * xratio;
            } else {
                FoxUtility.Camera.orthographicSize = size / yratio;
            }
        }
    }
}
