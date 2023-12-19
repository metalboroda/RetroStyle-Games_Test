using UnityEngine;

namespace Test_Game
{
  public class EnemyController : MonoBehaviour
  {
    [field: Header("")]
    [field: SerializeField] public EnemyHandler EnemyHandler { get; private set; }
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
  }
}