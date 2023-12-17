using UnityEngine;

namespace Test_Game
{
  public class PlayerController : MonoBehaviour
  {
    [field: SerializeField] public PlayerHandler PlayerHandler { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

    public StateMachine StateMachine { get; private set; } = new();

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