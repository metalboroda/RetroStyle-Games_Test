using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerHandler : CharacterHandler, IDamageable
  {
    [SerializeField] private int _energy = 50;

    [Header("")]
    [SerializeField] private PlayerController _playerController;

    [Inject] private PlayerStatsController _playerStatsController;

    private void Awake()
    {
      _playerStatsController.EnergyAdded += AddEnergy;
    }

    private void OnDestroy()
    {
      _playerStatsController.EnergyAdded -= AddEnergy;
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
    }
  }
}