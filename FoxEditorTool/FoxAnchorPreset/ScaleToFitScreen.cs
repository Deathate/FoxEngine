
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using FoxEngine;

namespace FoxAnchorPreset {
    [ExecuteAlways]
    public class ScaleToFitScreen : MonoBehaviour {

        public Vector2 reference_resolution;

        [Button]
        void Reset() => reference_resolution = (FoxUtility.Canvas.rectTransform()).rect.size;

        Vector2 GetMaxSize(Vector2 canvas, Vector2 body) {
            Func<Vector2, float> Ratio_rect = (rect) => rect.x / rect.y;
            var canvas_ratio = Ratio_rect(canvas);
            var image_ratio = Ratio_rect(body);
            float width = (canvas_ratio > image_ratio ? canvas.y * image_ratio : canvas.x);
            float height = width / image_ratio;
            return new Vector2(width, height);
        }

        void Calculate() {
            if (reference_resolution == Vector2.zero) return;
            var scaleto = GetMaxSize(FoxUtility.Canvas.rectTransform().rect.size, reference_resolution);
            transform.localScale = Vector3.one * (scaleto.x / reference_resolution.x);
        }

        private void Start() {
#if !UNITY_EDITOR
            if (Application.isPlaying) {
                Calculate();
                enabled = false;
            }
#endif
        }

        private void Update() {
            Calculate();
        }
    }
}
