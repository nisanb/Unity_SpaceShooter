using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPUPrefab;
    [SerializeField]
    private GameObject _speedPUPrefab;
    [SerializeField]
    private GameObject _shieldPUPrefab;

    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started SpawnManager");
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnPowerupRoutine()
    {
        // every 3-7 seconds, spawn in a powerup
        GameObject[] PowerUps = { _tripleShotPUPrefab, _speedPUPrefab, _shieldPUPrefab}; //_shieldPUPrefab};

        while (!_stopSpawning)
        {
            GameObject powerupPrefab = PowerUps[UnityEngine.Random.Range(0, 3)];

            Debug.Log("Spawning powerup");
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), 8.0f, 0);
            
            GameObject newPowerup = Instantiate(powerupPrefab, posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;

            
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 7));
        }
    }

    //spawn game objects every 5 seconds
    // Crate a coroutine of type IEnumerator -- Yield Events
    // while loop
    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            Debug.Log("Spawning enemy");
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), 8.0f, 0);
            // instantiate enemy prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            //yielf wait for 5 seconds
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.2f, 5.2f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
