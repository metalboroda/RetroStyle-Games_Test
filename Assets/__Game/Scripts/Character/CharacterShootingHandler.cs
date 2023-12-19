using UnityEngine;

namespace Test_Game
{
  public abstract class CharacterShootingHandler : MonoBehaviour
  {
    [SerializeField] protected PlayerProjectile Projectile;
    [SerializeField] protected float ProjectileSpeed;
    [SerializeField] protected int ProjectilePower;

    protected virtual void Shoot() { }
  }
}