using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test_Game
{
  public class GameController : MonoBehaviour
  {
    public event Action<GameState> StateChanged;

    [SerializeField] private InputManager _inputManager;

    public GameState GameState { get; private set; }

    private void Awake()
    {
      Time.timeScale = 1;

      _inputManager.PausePressed += PausePlayGame;
    }

    private void Start()
    {
      ChangeState(GameState.Game);
    }

    private void OnDestroy()
    {
      _inputManager.PausePressed -= PausePlayGame;
    }

    public void ChangeState(GameState newState)
    {
      if (newState != GameState)
      {
        GameState = newState;

        StateChanged?.Invoke(newState);
      }
    }

    public void PausePlayGame()
    {
      if (GameState == GameState.Lose) return;

      if (GameState == GameState.Game)
      {
        ChangeState(GameState.Pause);

        Time.timeScale = 0;

        _inputManager.DisableOnFeetControls();
      }
      else if (GameState == GameState.Pause)
      {
        ChangeState(GameState.Game);

        Time.timeScale = 1;

        _inputManager.EnableOnFeetControls();
      }
    }

    public void RestartGame()
    {
      _inputManager.EnableOnFeetControls();

      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}