
using UnityEngine;
using FoxEngine;
using Sirenix.OdinInspector;

namespace FoxAnchorPreset {

    public class AnchorPreset : MonoBehaviour {
        public enum _Preset {
            top,
            bottom,
            left,
            right,
            topleft,
            topright,
            bottomleft,
            bottomright,
        }
        public _Preset preset;
        bool isSet;
        public Vector3 pos;

        public void Calculate() {

            transform.position = GetPresetPos(preset) + pos * transform.lossyScale.x;
        }

        public Vector3 GetPresetPos(_Preset preset) {
            var canvas_size = FoxUtility.Canvas.rectTransform().sizeDelta;
            var middle = FoxUtility.Canvas.transform.position;
            var halfsize = canvas_size / 2;
            switch (preset) {
                case _Preset.topleft:
                    return middle + v3.mk(-halfsize.x, halfsize.y, 0);
                case _Preset.topright:
                    return middle + v3.mk(halfsize.x, halfsize.y, 0);
                case _Preset.left:
                    return middle + v3.mk(-halfsize.x, 0, 0);
                case _Preset.right:
                    return middle + v3.mk(halfsize.x, 0, 0);
                case _Preset.bottomleft:
                    return middle + v3.mk(-halfsize.x, -halfsize.y, 0);
                case _Preset.bottomright:
                    return middle + v3.mk(halfsize.x, -halfsize.y, 0);
                case _Preset.top:
                    return middle + v3.mk(0, halfsize.y, 0);
                case _Preset.bottom:
                    return middle + v3.mk(0, -halfsize.y, 0);
            }
            return Vector3.zero;
        }
    }
}
