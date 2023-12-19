using UnityEngine;

namespace Test_Game
{
  public class EnemyProjectile : Projectile
  {
    public void Init(float speed, int power)
    {
      Speed = speed;
      Power = power;
    }

    private void Update()
    {
      Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(Power);
      }

      Destroy(gameObject);
    }

    protected override void Fly()
    {
      transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
  }
}