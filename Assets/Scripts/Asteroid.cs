using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnmanager;
    // Start is called before the first frame update
    void Start()
    {
        if (_explosionPrefab == null)
        {
            Debug.LogError("Please assign the Explosion Prefab in the inspector");
        }

        _spawnmanager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (_spawnmanager == null)
        {
            Debug.LogError("Could not find Spawn manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rotate on zed axis
        transform.Rotate(Vector3.forward* _rotateSpeed * Time.deltaTime);
    }

    

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Lazer" || _other.gameObject.tag == "ClusterBomb")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(_other.gameObject);
            _spawnmanager.StartSpawning();
            Destroy(this.gameObject ,0.25f);
            
        }
        
    }
}
