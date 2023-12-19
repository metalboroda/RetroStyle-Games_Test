using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerController : MonoBehaviour
  {
    [field: SerializeField] public PlayerHandler PlayerHandler { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

    public StateMachine StateMachine { get; private set; } = new();

    [Inject] public InputManager InputManager { get; private set; }
    [Inject] public CameraManager CameraManager { get; private set; }
    [Inject] public PlayerStatsController PlayerStatsController { get; private set; }
    [Inject] public SpawnersController SpawnersController { get; private set; }

    private void Awake()
    {
      StateMachine.Init(new PlayerMovementState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }
  }
}