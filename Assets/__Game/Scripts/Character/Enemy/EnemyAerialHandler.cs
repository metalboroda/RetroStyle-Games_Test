using UnityEngine;

namespace Test_Game
{
  public class EnemyAerialHandler : EnemyHandler
  {
    [SerializeField] private int _power;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(_power);
      }

      Kill();
    }
  }
}