using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4f;
    Vector3 _dir = Vector3.down;

    [SerializeField]
    GameObject _LazerPrefab;

    [SerializeField]
    private float fireRate =3.0f;
    private float _canFire = -1;

    Player _player;
    // get animator componant 
    private Animator _anim;
    // Get audio Source 
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {

        if (_LazerPrefab == null)
        {
            Debug.LogError("Please Assign lazer prefab");
        }
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Could not find player");
        }
        // Assign the componant
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("COuld not find enemy animator");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("No audio source on Enemy prefab");
        }

       
    }

    // Update is called once per frame
    void Update()
    {
       
        Movement(_dir, _speed);
        if (Time.time > _canFire && GetComponent<Collider2D>() != null) // we check for a collider to see it the object has been destroyed 
        {
            fireRate = UnityEngine.Random.Range(3f, 8f);
            Vector3 offset = new Vector3(0, -1.59f, 0);

            _canFire = Time.time + fireRate;
            GameObject enemyLazer = Instantiate(_LazerPrefab, transform.position + offset, Quaternion.identity);
            Lazer[] lazers = enemyLazer.GetComponentsInChildren<Lazer>();
            for (int i =0; i < lazers.Length; i++)
            {
                lazers[i].AssignEnemyLazer();
            }

           
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        // if other is player
        if(other.gameObject.tag == "Player")
        {
            //Damage player
           
            if (_player != null)
            {
                _player.Damage();
            }
            // trigger animation 
            _anim.SetTrigger("OnEnemyDeath");
            //wait for animation to finsih?
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }

        if (other.gameObject.tag == "Lazer")
        {
            Lazer hitLazer = other.gameObject.GetComponent<Lazer>();
            if (hitLazer != null)
            {
                if (!hitLazer.CheckIfEnemyLazer())
                {
                    Destroy(other.gameObject);
                    _player.Score(10);

                    //trigger Anim
                    _anim.SetTrigger("OnEnemyDeath");
                    _speed = 0;
                    _audioSource.Play();
                    Destroy(GetComponent<Collider2D>());
                    Destroy(this.gameObject, 2.8f);
                }
            }

           
        }
        
        
    }

    private void Movement(Vector3 dir, float speed)
    {
        transform.Translate(dir * speed * Time.deltaTime);
        if (isPastBottom())
        {
            RespawnPosition();
        }
    }

    private void RespawnPosition()
    {
        float randomX = UnityEngine.Random.Range(-9.46f, 9.3f);
        transform.position = new Vector3( randomX, 7f, transform.position.z);
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
