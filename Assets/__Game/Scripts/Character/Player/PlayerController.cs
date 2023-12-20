using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerController : MonoBehaviour
  {
    public static PlayerController Instance { get; private set; }

    [field: SerializeField] public PlayerHandler PlayerHandler { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

    public StateMachine StateMachine { get; private set; } = new();

    [Inject] public GameController GameController { get; private set; }
    [Inject] public InputManager InputManager { get; private set; }
    [Inject] public CameraManager CameraManager { get; private set; }
    [Inject] public PlayerStatsController PlayerStatsController { get; private set; }
    [Inject] public SpawnersController SpawnersController { get; private set; }
    [Inject] public UIManager UIManager { get; private set; }

    private void Awake()
    {
      Instance = this;
      StateMachine.Init(new PlayerMovementState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }
  }
}