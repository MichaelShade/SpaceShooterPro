using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBomb : ClusterBombSecondary
{

  
    [SerializeField]
    private Transform RightBombSpawnPoint;
    [SerializeField]
    private Transform LeftBombSpawnPoint;

    [SerializeField]
    private GameObject LeftBomb;
    [SerializeField]
    private GameObject RighBomb;



    // Start is called before the first frame update
    private void Start()
    {
        if (RighBomb == null)
        {
            Debug.LogError("Please Assign RightBomb");
        }
        if (LeftBomb == null)
        {
            Debug.LogError("Please Assign RightBomb");
        }
    }
  

    // Update is called once per frame
    void Update()
    {
        base.SineMovement();
    }

  

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Enemy")
        {
            // instantiate bombs
            Instantiate(RighBomb, RightBombSpawnPoint.transform.position, RighBomb.transform.rotation);
            Instantiate(LeftBomb, LeftBombSpawnPoint.transform.position, LeftBomb.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
