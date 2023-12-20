using System;
using UnityEngine;

namespace Test_Game
{
  public class EnemyHandler : CharacterHandler, IDamageable
  {
    public event Action Dead;

    [Header("")]
    [SerializeField] private int _deathCost;

    [Header("")]
    [SerializeField] private EnemyController _enemyController;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out PlayerProjectile playerProjectile))
      {
        if (playerProjectile.Ricocheted == false)
        {
          _enemyController.PlayerStatsController.AddEnergy(_deathCost);
        }
        else
        {
          _enemyController.PlayerStatsController.AddRandomEnergyHealth();
        }
      }
    }

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        _enemyController.SpawnersController.RemoveEnemy(this);

        Kill();
      }
    }

    public void Kill()
    {
      Dead?.Invoke();

      Destroy(gameObject);
    }
  }
}