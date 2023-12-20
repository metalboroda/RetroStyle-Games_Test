using System;
using UnityEngine;

namespace Test_Game
{
  public class PlayerHandler : CharacterHandler, IDamageable
  {
    public event Action<int> HealthChanged;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxEnergy = 100;
    [SerializeField] private int _currentEnergy = 50;

    [Header("")]
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
      _playerController.UIManager.UpdateHealthBar(Health);
      _playerController.UIManager.UpdateEnergyBar(_currentEnergy);

      _playerController.PlayerStatsController.HealthAdded += AddHealth;
      _playerController.PlayerStatsController.EnergyAdded += AddEnergy;
      _playerController.InputManager.UltaPressed += Ulta;
    }

    private void OnDestroy()
    {
      _playerController.PlayerStatsController.HealthAdded -= AddHealth;
      _playerController.PlayerStatsController.EnergyAdded -= AddEnergy;
      _playerController.InputManager.UltaPressed -= Ulta;
    }

    public void Damage(int damage)
    {
      Health -= damage;

      HealthChanged?.Invoke(Health);
      _playerController.UIManager.UpdateHealthBar(Health);

      if (Health <= 0)
      {
        Health = 0;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    private void AddHealth(int health)
    {
      Health += health;

      if (Health >= _maxHealth)
      {
        Health = _maxHealth;
      }

      _playerController.UIManager.UpdateHealthBar(Health);
    }

    private void AddEnergy(int energy)
    {
      _currentEnergy += energy;

      if (_currentEnergy >= _maxEnergy)
      {
        _currentEnergy = _maxEnergy;
      }

      _playerController.UIManager.UpdateEnergyBar(_currentEnergy);
    }

    private void Ulta()
    {
      if (_currentEnergy == _maxEnergy)
      {
        _playerController.SpawnersController.KillAllEnemies();
        _currentEnergy = 0;

        _playerController.UIManager.UpdateEnergyBar(_currentEnergy);
      }
    }
  }
}