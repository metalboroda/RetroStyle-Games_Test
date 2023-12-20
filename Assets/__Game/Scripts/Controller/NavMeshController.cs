using UnityEngine;
using UnityEngine.AI;

namespace Test_Game
{
  public class NavMeshController : MonoBehaviour
  {
    public Vector3 GetRandomPointOnNavMesh()
    {
      int maxAttempts = 30;
      int attempts = 0;

      Vector3 randomPoint = Vector3.zero;

      do
      {
        randomPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
          return hit.position;
        }

        attempts++;

      } while (attempts < maxAttempts);

      return Vector3.zero;
    }
  }
}