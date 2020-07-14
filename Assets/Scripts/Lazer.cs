using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
   [SerializeField]
   public float _lazerSpeed = 8;

    private bool isEnemyLazer = false;
    // Update is called once per frame
    void Update()
    {
        if (isEnemyLazer == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
      
       
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _lazerSpeed * Time.deltaTime);
        if (gameObject.transform.position.y <= -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _lazerSpeed * Time.deltaTime);
        if (gameObject.transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

   public void AssignEnemyLazer()
    {
        this.isEnemyLazer = true;
    }
    public bool CheckIfEnemyLazer()
    {
        return isEnemyLazer;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player" && isEnemyLazer)
        {
            Player player = _other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
        
    }


}
