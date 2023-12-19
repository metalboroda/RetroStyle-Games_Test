using UnityEngine;

namespace Test_Game
{
  public class PlayerHandler : CharacterHandler, IDamageable
  {
    [SerializeField] private int _maxEnergy = 100;

    private int _energy = 50;

    [Header("")]
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
      _playerController.PlayerStatsController.EnergyAdded += AddEnergy;
      _playerController.InputManager.UltaPressed += Ulta;
    }

    private void OnDestroy()
    {
      _playerController.PlayerStatsController.EnergyAdded -= AddEnergy;
      _playerController.InputManager.UltaPressed -= Ulta;
    }

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    private void AddEnergy(int energy)
    {
      _energy += energy;

      if (_energy >= _maxEnergy)
      {
        _energy = _maxEnergy;
      }
    }

    private void Ulta()
    {
      if (_energy == _maxEnergy)
      {
        _playerController.SpawnersController.KillAllEnemies();
      }
    }
  }
}