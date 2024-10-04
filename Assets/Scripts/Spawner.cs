using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _spawnTime = 2f;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private WaitForSeconds _spawnDeley;
    private Coroutine _spawnEnemyCorutine;
    private ObjectPool<Enemy> _enemyPool;
    private List<Enemy> _signedEnemies;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
        createFunc: () =>  CreateEnemy(),
        actionOnGet: (enemy) => LaunchEnemy(enemy),
        actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
        actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);

        _spawnDeley = new WaitForSeconds(_spawnTime);
        _signedEnemies = new List<Enemy>();
    }

    private void OnEnable()
    {
        _spawnEnemyCorutine = StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnEnemyCorutine);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEnemies();
    }

    private void UnsubscribeFromEnemies()
    {
        foreach (Enemy enemy in _signedEnemies)
        {
            enemy.Died -= ReleaseEnemy;
        }
    }

    private void LaunchEnemy(Enemy enemy)
    {
        SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        enemy.transform.position = spawnPoint.transform.position;
        enemy.transform.rotation = spawnPoint.TurningEnemy;
        enemy.gameObject.SetActive(true);
        _signedEnemies.Add(enemy);
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.Died += ReleaseEnemy;

        return enemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            _enemyPool.Get();
            yield return _spawnDeley;
        }
    }
}
