using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    float _SpawnRate = 5f;
    [SerializeField]
    public GameObject EnemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpwaning = false;
    [SerializeField]
    private GameObject[] _Poweups;

    // Start is called before the first frame update
   

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while (_stopSpwaning ==false && EnemyPrefab !=null)
        {
            
            float randomX = UnityEngine.Random.Range(-9.46f, 9.3f);
            GameObject NewEnemy= Instantiate(EnemyPrefab, new Vector3(randomX, 7f, 0f), Quaternion.identity);
            NewEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_SpawnRate);

        }
       
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.5f);

        while (_stopSpwaning == false)
        {
            
             float RandomSpawnTime = UnityEngine.Random.Range(3f, 6f);
          
            float randomX = UnityEngine.Random.Range(-9.46f, 9.3f);
            int randomPowerUp = UnityEngine.Random.Range(0, 5);
            Instantiate(_Poweups[randomPowerUp], new Vector3(randomX, 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(RandomSpawnTime);
        }
       
    }

    public void OnPlayerDeath()
    {
        _stopSpwaning = true;
    }
}
