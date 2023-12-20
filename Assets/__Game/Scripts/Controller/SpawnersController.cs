using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class SpawnersController : MonoBehaviour
  {
    public event Action<int> EnemyKilled;

    [SerializeField] private EnemyHandler _blueEnemyPrefab;
    [SerializeField] private EnemyHandler _redEnemyPrefab;

    [Header("")]
    [SerializeField] private float _initialSpawnInterval = 10f;
    [SerializeField] private int _maxEnemies = 30;

    [Header("")]
    [SerializeField] private NavMeshController _navMeshController;

    private List<EnemyHandler> _spawnedEnemies = new();

    private float _currentSpawnInterval;
    private float _minSpawnInterval = 6f;
    private int _killedEnemiesCounter;

    [Inject] private DiContainer _spawnContainer;

    private void Start()
    {
      _currentSpawnInterval = _initialSpawnInterval;

      StartCoroutine(SpawnEnemiesRoutine());
    }

    public void RemoveEnemy(EnemyHandler enemyHandler)
    {
      if (_spawnedEnemies.Contains(enemyHandler))
      {
        _killedEnemiesCounter++;
        _spawnedEnemies.Remove(enemyHandler);

        EnemyKilled?.Invoke(_killedEnemiesCounter);
      }
    }

    public EnemyHandler GetSecondClosestEnemy(Vector3 position)
    {
      if (_spawnedEnemies.Count < 2)
      {
        return null;
      }

      EnemyHandler closestEnemy = _spawnedEnemies[0];
      float closestDistance = Vector3.Distance(position, closestEnemy.transform.position);
      EnemyHandler secondClosestEnemy = _spawnedEnemies[1];
      float secondClosestDistance = Vector3.Distance(position, secondClosestEnemy.transform.position);

      if (closestDistance > secondClosestDistance)
      {
        EnemyHandler temp = closestEnemy;

        closestEnemy = secondClosestEnemy;
        secondClosestEnemy = temp;

        float tempDistance = closestDistance;

        closestDistance = secondClosestDistance;
        secondClosestDistance = tempDistance;
      }

      for (int i = 2; i < _spawnedEnemies.Count; i++)
      {
        float distance = Vector3.Distance(position, _spawnedEnemies[i].transform.position);

        if (distance < closestDistance)
        {
          secondClosestEnemy = closestEnemy;
          secondClosestDistance = closestDistance;

          closestEnemy = _spawnedEnemies[i];
          closestDistance = distance;
        }
        else if (distance < secondClosestDistance)
        {
          secondClosestEnemy = _spawnedEnemies[i];
          secondClosestDistance = distance;
        }
      }

      return secondClosestEnemy;
    }

    public void KillAllEnemies()
    {
      if (_spawnedEnemies.Count == 0) return;

      _spawnedEnemies.ForEach(enemy =>
      {
        enemy.Kill();
      });

      _spawnedEnemies.Clear();
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
      while (true)
      {
        yield return new WaitForSeconds(_currentSpawnInterval);

        SpawnBlueEnemy();

        for (int i = 0; i < 4; i++)
        {
          SpawnRedEnemy();
        }

        UpdateSpawnInterval();
      }
    }

    private void SpawnBlueEnemy()
    {
      if (_spawnedEnemies.Count < _maxEnemies)
      {
        EnemyHandler blueEnemy = _spawnContainer.InstantiatePrefabForComponent<EnemyHandler>(_blueEnemyPrefab,
          _navMeshController.GetRandomPointOnNavMesh(), Quaternion.identity, null);

        _spawnedEnemies.Add(blueEnemy);
      }
    }

    private void SpawnRedEnemy()
    {
      if (_spawnedEnemies.Count < _maxEnemies)
      {
        Vector3 spawnPosition = _navMeshController.GetRandomPointOnNavMesh();

        spawnPosition.y += 1.0f;

        EnemyHandler redEnemy = _spawnContainer.InstantiatePrefabForComponent<EnemyHandler>(_redEnemyPrefab,
          spawnPosition, Quaternion.identity, null);

        _spawnedEnemies.Add(redEnemy);
      }
    }

    private void UpdateSpawnInterval()
    {
      _currentSpawnInterval = Mathf.Max(_minSpawnInterval, _currentSpawnInterval - 2f);
    }
  }
}