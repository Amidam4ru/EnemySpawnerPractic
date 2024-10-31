using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Target _target;

    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Enemy> _enemyPool;
    private List<Enemy> _signedEnemies;

    public Target Target => _target;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
        createFunc: () => CreateEnemy(),
        actionOnGet: (enemy) => LaunchEnemy(enemy),
        actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
        actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);

        _signedEnemies = new List<Enemy>();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEnemies();
    }

    public void SpawnEnemy()
    {
        _enemyPool.Get();
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.SetTarget(Target);
        enemy.Finished += ReleaseEnemy;
        _signedEnemies.Add(enemy);

        return enemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void UnsubscribeFromEnemies()
    {
        foreach (Enemy enemy in _signedEnemies)
        {
            enemy.Finished -= ReleaseEnemy;
        }
    }

    private void LaunchEnemy(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }
}
