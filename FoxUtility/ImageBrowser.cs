
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class ImageBrowser : MonoBehaviour
{
    [ShowInInspector]
    public bool _enable;

    [DrawWithUnity, SerializeField]
    private Sprite[] _browser = new Sprite[1];

    int _range { get => transform.childCount - 1; }
    [ShowInInspector, PropertyRange(-1, "_range")]
    public int _order;
    private int _order_record;

    private void Start()
    {
        _order_record = -2;
        _order = -1;
        Browse();
    }

    private void Update() 
    {
        if (!_enable)
            return;
        Browse();
    }

    void Browse()
    {
        if (_order != _order_record)
        {
            SetChildsActive(_order);
            _order_record = _order;
        }
    }

    void SetChildsActive(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        if (index == -1)
            return;
            transform.GetChild(index).gameObject.SetActive(true);
    }
}
