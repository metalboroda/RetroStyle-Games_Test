using Lean.Pool;
using System;
using UnityEngine;

namespace Test_Game
{
  public class EnemyHandler : CharacterHandler, IDamageable
  {
    public event Action Dead;

    [field: Header("")]
    [field: SerializeField] public int DeathCost { get; private set; }

    [field: Header("")]
    [field: SerializeField] public EnemyController EnemyController { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out PlayerProjectile playerProjectile))
      {
        if (playerProjectile.Ricocheted == false)
        {
          EnemyController.PlayerStatsController.AddEnergy(DeathCost);
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

      Vector3 vfxPos = new Vector3(transform.position.x, transform.position.y + VFXYOffset,
        transform.position.z);

      LeanPool.Spawn(DeathVFXObj, vfxPos, Quaternion.identity);

      Destroy(gameObject);
    }
  }
}