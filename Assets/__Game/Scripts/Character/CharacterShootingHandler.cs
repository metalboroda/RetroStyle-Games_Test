using UnityEngine;

namespace Test_Game
{
  public abstract class CharacterShootingHandler : MonoBehaviour
  {
    [SerializeField] protected Projectile Projectile;
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    [field: SerializeField] public int ProjectilePower { get; private set; }

    protected virtual void Shoot() { }
  }
}