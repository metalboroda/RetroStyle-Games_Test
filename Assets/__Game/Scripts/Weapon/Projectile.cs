using UnityEngine;

namespace Test_Game
{
  public class Projectile : MonoBehaviour
  {
    protected float Speed;
    protected int Power;

    [Header("")]
    [SerializeField] protected LayerMask IgnoreLayer;

    protected virtual void Fly() { }
  }
}