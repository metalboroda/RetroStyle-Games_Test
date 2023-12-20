using System;
using UnityEngine;

namespace Test_Game
{
  public class PlayerHandler : CharacterHandler, IDamageable
  {
    public event Action<int> HealthChanged;

    [SerializeField] private int _maxEnergy = 100;
    [SerializeField] private int _currentEnergy = 50;

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

      HealthChanged?.Invoke(Health);

      if (Health <= 0)
      {
        Health = 0;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    private void AddEnergy(int energy)
    {
      _currentEnergy += energy;

      if (_currentEnergy >= _maxEnergy)
      {
        _currentEnergy = _maxEnergy;
      }
    }

    private void Ulta()
    {
      if (_currentEnergy == _maxEnergy)
      {
        _playerController.SpawnersController.KillAllEnemies();
        _currentEnergy = 0;
      }
    }
  }
}