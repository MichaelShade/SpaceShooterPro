using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("No AudioSOurce on explosion Prefab");
        }

        _audioSource.Play();
        Destroy(this.gameObject, 3f);
    }

   
}
