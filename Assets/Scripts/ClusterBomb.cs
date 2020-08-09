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

            var render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.enabled = false;
            }
            // instantiate bombs
            StartCoroutine(SpawnBombRoutine(_other.transform));
          
        }
    }

    IEnumerator SpawnBombRoutine(Transform enemypos)
    {
        yield return new WaitForSeconds(.2f);
        Instantiate(RighBomb, enemypos.position, RighBomb.transform.rotation);
        Instantiate(LeftBomb, enemypos.position, LeftBomb.transform.rotation);
        Destroy(this.gameObject);

    }
}
