using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombSecondary : MonoBehaviour
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
    public void SineMovement()
    {
        _pos += transform.up * Time.deltaTime * _BombSpeed;
        transform.position = _pos + _axis * Mathf.Sin(Time.time * frequency) * magnitude;

        if (gameObject.transform.position.y >= 8)
        {
            Destroy(this.gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
