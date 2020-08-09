using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    float _speed = 4f;
    Vector3 _dir = Vector3.down;

    [SerializeField]
    int AmmoToAdd = 0;
  
    [SerializeField]
    private int powerupID;   
    // Id for powerups 
    //0 = tripple shot 
    //1 = speed 
    //2 = shileds 
    //3 = Ammo 
    //4 = Health 
    //5 = Cluster Bomb

    

    [SerializeField]
    private AudioClip _collectClip;


   

    

    // Update is called once per frame
    void Update()
    {
        Movement(_dir, _speed);
       

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();
           
            if (playerScript != null)
            {
                
                AudioSource.PlayClipAtPoint(_collectClip, this.transform.position, 1.0f);

                switch (powerupID)
                {
                    case 0:
                        playerScript.TrippleShotActive();
                         break;
                    case 1:
                        playerScript.SpeedBostActive();
                        break;
                    case 2:
                        playerScript.ShieldActive();
                        break;
                    case 3:
                        playerScript.AddAmmo(AmmoToAdd);
                        break;
                    case 4:
                        playerScript.AddLife();
                        break;
                    case 5:
                        playerScript.ClusterBombActive();
                        break;
                    default:
                        Debug.Log("Default VAlue");
                        break;
                       
                }
                                                            
            }
            else
            {
                Debug.Log("Could not find Player script");
            }
            Destroy(this.gameObject);
        }
    }



    void Movement (Vector3 _dir, float _speed)
    {
        transform.Translate(_dir * _speed * Time.deltaTime);
        if (isPastBottom())
        {
            Destroy(this.gameObject);
        }
    }

    private bool isPastBottom()
    {
        if (transform.position.y < -5.4)
        {
            return true;
        }
        return false;
    }
}
