using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Vector3 _spawnCenter;
    [SerializeField]
    private float _spawnRadius;

    [SerializeField]
    private float _cooldawn = 5;

    private Coroutine _spawnCoroutine;

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            _spawnCenter.x + Random.Range(-_spawnRadius,_spawnRadius),
            _spawnCenter.y,
            _spawnCenter.z + Random.Range(-_spawnRadius, _spawnRadius)
        );
    }

    public void Spawn()
    {
        _spawnCoroutine = StartCoroutine(WaitSpawn());
    }

    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(_cooldawn);

        SpawnEnemy();
    }
}
