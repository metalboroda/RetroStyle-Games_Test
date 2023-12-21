using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test_Game
{
  public class UIManager : MonoBehaviour
  {
    [Header("Screens")]
    [SerializeField] private List<GameObject> _screens = new();

    [Header("GameScreen")]
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private TextMeshProUGUI _gameKillCounterTxt;
    [SerializeField] private Image _gameHealth;
    [SerializeField] private Image _gameEnergy;
    [SerializeField] private Image _damageVignette;
    [SerializeField] private float _damageFadeDuration = 0.5f;

    [Header("MenuScreen")]
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private Button _menuStartBtn;
    [SerializeField] private Button _menuRestartBtn;
    [SerializeField] private Button _menuExitBtn;

    [Header("LoseScreen")]
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Button _loseRestartBtn;
    [SerializeField] private Button _loseExitBtn;

    [Header("")]
    [SerializeField] private GameController _gameController;
    [SerializeField] private SpawnersController _spawnersController;

    private Tweener _damageTween;

    private void Awake()
    {
      _gameController.StateChanged += HandleCursor;
      _gameController.StateChanged += EnableMenuScreen;
      _gameController.StateChanged += EnableLoseScreen;
      _spawnersController.EnemyKilled += UpdateKillCounter;
    }

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;

      SubscribeButtons();
      UpdateKillCounter(0);
    }

    private void OnDestroy()
    {
      _gameController.StateChanged -= HandleCursor;
      _gameController.StateChanged -= EnableMenuScreen;
      _gameController.StateChanged -= EnableLoseScreen;
      _spawnersController.EnemyKilled -= UpdateKillCounter;
    }

    private void SubscribeButtons()
    {
      _menuStartBtn.onClick.AddListener(() => { _gameController.PausePlayGame(); });
      _menuRestartBtn.onClick.AddListener(() => { _gameController.RestartGame(); });
      _menuExitBtn.onClick.AddListener(() => { Application.Quit(); });

      _loseRestartBtn.onClick.AddListener(() => { _gameController.RestartGame(); });
      _loseExitBtn.onClick.AddListener(() => { Application.Quit(); });
    }

    public void UpdateHealthBar(int health)
    {
      _gameHealth.fillAmount = (float)health / 100f;
    }

    public void UpdateEnergyBar(int energy)
    {
      _gameEnergy.fillAmount = (float)energy / 100f;
    }

    private void HandleCursor(GameState gameState)
    {
      if (gameState == GameState.Game)
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else
      {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
      }
    }

    private void UpdateKillCounter(int counter)
    {
      _gameKillCounterTxt.SetText(counter.ToString());
    }

    private void EnableMenuScreen(GameState gameState)
    {
      if (gameState == GameState.Lose) return;

      if (gameState == GameState.Pause)
      {
        EnableScreen(_menuScreen);
      }
      else
      {
        EnableScreen(_gameScreen);
      }
    }

    private void EnableLoseScreen(GameState gameState)
    {
      if (gameState != GameState.Lose) return;

      if (gameState == GameState.Lose)
      {
        EnableScreen(_loseScreen);
      }
    }

    private void EnableScreen(GameObject screenToEnable)
    {
      foreach (var screen in _screens)
      {
        if (screen == screenToEnable)
        {
          screen.SetActive(true);
        }
        else
        {
          screen.SetActive(false);
        }
      }
    }

    public void ShowDamage()
    {
      if (_damageTween != null)
      {
        DOTween.Kill(_damageTween);
      }

      _damageVignette.color = new Color(_damageVignette.color.r, _damageVignette.color.g, _damageVignette.color.b, 1f);

      _damageTween = _damageVignette.DOFade(1, _damageFadeDuration / 2.5f)
          .OnComplete(() =>
          {
            DOVirtual.DelayedCall(0.15f, () =>
            {
              _damageVignette.DOFade(0, _damageFadeDuration);
            });
          });
    }
  }
}