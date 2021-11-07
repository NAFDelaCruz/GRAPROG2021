using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public PlayerController PlayerControllerScript;
    public Camera MainCamera;
    public float ParallaxEffect;

    private float _length, _startPos;

    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void FixedUpdate()
    {
        float temp = (MainCamera.transform.position.x * (1 - ParallaxEffect));
        float dist = (MainCamera.transform.position.x * ParallaxEffect);

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;
    }
}
