
using UnityEngine;
using FoxEngine;

public class EasyCameraFollow : MonoBehaviour {
    Camera _camera;
    Vector3 _delta_pos;
    public Transform _target;

    private void Start() {
        _camera = FindObjectOfType<Camera>();
        _delta_pos = transform.position.SetX(0).SetY(Assets._i.global_property.camera_y_distance);
        _target.GetComponent<Character_Controller>().SetEnable(true);
    }

    private void LateUpdate() {
#if UNITY_EDITOR
        _delta_pos = transform.position.SetX(0).SetY(Assets._i.global_property.camera_y_distance);
#endif
        transform.position = _target.position.SetZ(0) + _delta_pos;
    }
}
