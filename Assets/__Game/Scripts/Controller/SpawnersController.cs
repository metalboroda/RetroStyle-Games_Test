using System.Collections.Generic;
using UnityEngine;

namespace Test_Game
{
  public class SpawnersController : MonoBehaviour
  {
    [field: SerializeField] public List<EnemyHandler> SpawnedEnemies = new();

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

      // Ensure closestEnemy and secondClosestEnemy are correctly assigned
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
      SpawnedEnemies.ForEach(enemy =>
      {
        enemy.Kill();
      });

      SpawnedEnemies.Clear();
    }
  }
}