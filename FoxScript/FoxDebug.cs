
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace FoxEngine {

    public class FoxDebug {
        public static void DebugDrawBox(Vector2 point, Vector2 size, float angle = 0, Color color = default, float duration = 1f) {

            var orientation = Quaternion.Euler(0, 0, angle);

            // Basis vectors, half the size in each direction from the center.
            Vector2 right = orientation * Vector2.right * size.x / 2f;
            Vector2 up = orientation * Vector2.up * size.y / 2f;

            // Four box corners.
            var topLeft = point + up - right;
            var topRight = point + up + right;
            var bottomRight = point - up + right;
            var bottomLeft = point - up - right;

            // Now we've reduced the problem to drawing lines.
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }
#if UNITY_EDITOR
        public static void EditorStop() {
            UnityEditor.EditorApplication.isPaused = true;
        }

        public static void EditorKeep() {
            UnityEditor.EditorApplication.isPaused = false;
        }
#endif
        public static bool CheckInternetInternal() {
            return !(Application.internetReachability == NetworkReachability.NotReachable);
        }

        public static IEnumerator CheckInternetExteral(Action<bool> action) {
            var request = UnityWebRequest.Get("http://www.aaronprogram.tk/one%20word.html");
            yield return request.SendWebRequest();
            if (request.downloadedBytes == 0)
                action(false);
            else
                action(true);
        }
    }
}