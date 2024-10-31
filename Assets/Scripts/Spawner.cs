using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _spawnTime = 2f;

    private WaitForSeconds _spawnDeley;

    private void Awake()
    {
        _spawnDeley = new WaitForSeconds(_spawnTime);

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return _spawnDeley;

            SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            spawnPoint.SpawnEnemy();
        }
    }
}
