using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class EnemyHandler : CharacterHandler, IDamageable
  {
    [Header("")]
    [SerializeField] private int _deathCost;

    [Inject] private PlayerStatsController _playerStatsController;
    [Inject] private SpawnersController _spawnersController;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out PlayerProjectile playerProjectile))
      {
        if (playerProjectile.Ricocheted == false)
        {
          _playerStatsController.AddEnergy(_deathCost);
        }
        else
        {
          _playerStatsController.AddRandomEnergyHealth();
        }
      }
    }

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        _spawnersController.RemoveEnemy(this);

        Destroy(gameObject);
      }
    }
  }
}