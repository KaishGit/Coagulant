using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject White;

    private Vector3 _InnerPosition;

    void Start()
    {
        _InnerPosition = new Vector3();
        _InnerPosition.z = transform.position.z;
        //_InnerPosition.z = -10;
    }

    void Update()
    {
        _InnerPosition.x = White.transform.position.x;
        _InnerPosition.y = White.transform.position.y;
       
        transform.position = _InnerPosition;
    }
}