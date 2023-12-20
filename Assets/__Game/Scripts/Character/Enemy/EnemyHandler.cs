using System;
using UnityEngine;

namespace Test_Game
{
  public class EnemyHandler : CharacterHandler, IDamageable
  {
    public event Action Dead;

    [Header("")]
    [SerializeField] private int _deathCost;

    [field: Header("")]
    [field: SerializeField] public EnemyController EnemyController { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out PlayerProjectile playerProjectile))
      {
        if (playerProjectile.Ricocheted == false)
        {
          EnemyController.PlayerStatsController.AddEnergy(_deathCost);
        }
        else
        {
          EnemyController.PlayerStatsController.AddRandomEnergyHealth();
        }
      }
    }

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        EnemyController.SpawnersController.RemoveEnemy(this);

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