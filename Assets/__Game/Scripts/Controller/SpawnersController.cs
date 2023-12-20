using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class SpawnersController : MonoBehaviour
  {
    [SerializeField] private EnemyHandler _blueEnemyPrefab;
    [SerializeField] private EnemyHandler _redEnemyPrefab;

    [Header("")]
    [SerializeField] private float _initialSpawnInterval = 10f;
    [SerializeField] private int _maxEnemies = 30;

    [Header("")]
    [SerializeField] private NavMeshController _navMeshController;

    public List<EnemyHandler> SpawnedEnemies = new();

    private float _currentSpawnInterval;
    private float _minSpawnInterval = 6f;

    [Inject] private DiContainer _spawnContainer;

    private void Start()
    {
      _currentSpawnInterval = _initialSpawnInterval;

      StartCoroutine(SpawnEnemiesRoutine());
    }

    public void RemoveEnemy(EnemyHandler enemyHandler)
    {
      if (SpawnedEnemies.Contains(enemyHandler))
      {
        SpawnedEnemies.Remove(enemyHandler);
      }
    }

    public EnemyHandler GetSecondClosestEnemy(Vector3 position)
    {
      if (SpawnedEnemies.Count < 2)
      {
        return null;
      }

      EnemyHandler closestEnemy = SpawnedEnemies[0];
      float closestDistance = Vector3.Distance(position, closestEnemy.transform.position);
      EnemyHandler secondClosestEnemy = SpawnedEnemies[1];
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

      for (int i = 2; i < SpawnedEnemies.Count; i++)
      {
        float distance = Vector3.Distance(position, SpawnedEnemies[i].transform.position);

        if (distance < closestDistance)
        {
          secondClosestEnemy = closestEnemy;
          secondClosestDistance = closestDistance;

          closestEnemy = SpawnedEnemies[i];
          closestDistance = distance;
        }
        else if (distance < secondClosestDistance)
        {
          secondClosestEnemy = SpawnedEnemies[i];
          secondClosestDistance = distance;
        }
      }

      return secondClosestEnemy;
    }

    public void KillAllEnemies()
    {
      if (SpawnedEnemies.Count == 0) return;

      SpawnedEnemies.ForEach(enemy =>
      {
        enemy.Kill();
      });

      SpawnedEnemies.Clear();
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
      if (SpawnedEnemies.Count < _maxEnemies)
      {
        EnemyHandler blueEnemy = _spawnContainer.InstantiatePrefabForComponent<EnemyHandler>(_blueEnemyPrefab,
          _navMeshController.GetRandomPointOnNavMesh(), Quaternion.identity, null);

        SpawnedEnemies.Add(blueEnemy);
      }
    }

    private void SpawnRedEnemy()
    {
      if (SpawnedEnemies.Count < _maxEnemies)
      {
        Vector3 spawnPosition = _navMeshController.GetRandomPointOnNavMesh();

        spawnPosition.y += 1.0f;

        EnemyHandler redEnemy = _spawnContainer.InstantiatePrefabForComponent<EnemyHandler>(_redEnemyPrefab,
          spawnPosition, Quaternion.identity, null);

        SpawnedEnemies.Add(redEnemy);
      }
    }

    private void UpdateSpawnInterval()
    {
      _currentSpawnInterval = Mathf.Max(_minSpawnInterval, _currentSpawnInterval - 2f);
    }
  }
}