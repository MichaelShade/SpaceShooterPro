using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBomb : MonoBehaviour
{

    [SerializeField]
    float _BombSpeed = 6f;

    private Vector3 _axis;
    private Vector3 _pos;

    [SerializeField]
    private float frequency = 20f; //Speed of sine Movement
    [SerializeField]
    private float magnitude = 0.5f; // Size of sine Movement



    // Start is called before the first frame update
    void Awake()
    {
        _pos = transform.position;
        _axis = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        SineMovement();
    }

    private void SineMovement()
    {
        _pos += transform.up * Time.deltaTime * _BombSpeed;
        transform.position = _pos + _axis * Mathf.Sin(Time.time * frequency) * magnitude;


    }

    // Will need an ontrigger enter to make a cluster boom boom
}
