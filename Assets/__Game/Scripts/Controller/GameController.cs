using System;
using UnityEngine;

namespace Test_Game
{
  public class GameController : MonoBehaviour
  {
    public event Action<GameState> StateChanged;

    [SerializeField] private InputManager _inputManager;

    public GameState GameState { get; private set; }

    private void Awake()
    {
      _inputManager.PausePressed += PauseGame;
    }

    private void Start()
    {
      ChangeState(GameState.Game);
    }

    private void OnDestroy()
    {
      _inputManager.PausePressed -= PauseGame;
    }

    public void ChangeState(GameState newState)
    {
      if (newState != GameState)
      {
        GameState = newState;

        StateChanged?.Invoke(newState);
      }
    }

    private void PauseGame()
    {
      if (GameState == GameState.Game)
      {
        Time.timeScale = 0;

        ChangeState(GameState.Pause);
      }
      else if (GameState == GameState.Pause)
      {
        Time.timeScale = 1;

        ChangeState(GameState.Game);
      }
    }
  }
}