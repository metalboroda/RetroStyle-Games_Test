using UnityEngine;

namespace Test_Game
{
  public class EnemyAerialHandler : EnemyHandler
  {
    [SerializeField] private int _power;

    [Header("")]
    [SerializeField] private LayerMask _collisionDeathLayer;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(_power);
      }

      if (other.GetComponent<PlayerHandler>() != null)
      {
        EnemyController.SpawnersController.RemoveEnemy(this);
        Kill();
      }

      if ((_collisionDeathLayer.value & (1 << other.gameObject.layer)) != 0)
      {
        Kill();
      }
    }
  }
}