using TMPro;
using UnityEngine;

namespace Test_Game
{
  public class UIManager : MonoBehaviour
  {
    [Header("GameScreen")]
    [SerializeField] private TextMeshProUGUI _killCounterTxt;

    [Header("")]
    [SerializeField] private SpawnersController _spawnersController;

    private void Awake()
    {
      _spawnersController.EnemyKilled += UpdateKillCounter;
    }

    private void Start()
    {
      UpdateKillCounter(0);
    }

    private void OnDestroy()
    {
      _spawnersController.EnemyKilled -= UpdateKillCounter;
    }

    private void UpdateKillCounter(int counter)
    {
      _killCounterTxt.SetText(counter.ToString());
    }
  }
}