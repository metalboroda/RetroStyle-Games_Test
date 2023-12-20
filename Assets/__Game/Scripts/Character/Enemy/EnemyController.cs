using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class EnemyController : MonoBehaviour
  {
    [field: Header("")]
    [field: SerializeField] public EnemyHandler EnemyHandler { get; private set; }
    [field: SerializeField] public CharacterMovement EnemyMovement { get; private set; }

    [Inject] public PlayerStatsController PlayerStatsController { get; private set; }
    [Inject] public SpawnersController SpawnersController { get; private set; }
  }
}