using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float _randomSpawn = 9;
    private int _countEnemies;
    private int _waveNumber = 1;

    void Start()
    {
        EnemySpawnWaves(_waveNumber);
        Instantiate(powerUpPrefab, GetSpawnPosition() + new Vector3(0, 1, 0), powerUpPrefab.transform.rotation);
    }

    private void Update()
    {
        _countEnemies = FindObjectsOfType<Enemy>().Length;
        if (_countEnemies == 0)
        {
            _waveNumber++;
            EnemySpawnWaves(_waveNumber);
            Instantiate(powerUpPrefab, GetSpawnPosition() + new Vector3(0, 1, 0), powerUpPrefab.transform.rotation);
        }
    }

    void EnemySpawnWaves(int countEnemy)
    {
        for (int i = 0; i < countEnemy; i++)
        {
            Instantiate(enemyPrefab, GetSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float spawnRange = Random.Range(-_randomSpawn, _randomSpawn);
        Vector3 spawnPosition = new Vector3(spawnRange, 0, spawnRange);
        return spawnPosition;
    }
}