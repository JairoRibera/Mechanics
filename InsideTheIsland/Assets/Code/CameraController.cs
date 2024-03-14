using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float minHeight, maxHeight;
    private Vector2 _lastPos;
    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
        Vector2 _amountToMove = new Vector3(transform.position.x - _lastPos.x, transform.position.y - _lastPos.y);
        _lastPos = transform.position;

    }
}
