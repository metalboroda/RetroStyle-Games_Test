using UnityEngine;

namespace Test_Game
{
  public class GameController : MonoBehaviour
  {
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
      StateMachine = new();
    }
  }
}