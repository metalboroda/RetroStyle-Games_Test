using UnityEngine;

namespace Test_Game
{
  public class Projectile : MonoBehaviour
  {
    protected float Speed;
    protected int Power;

    [Header("")]
    [SerializeField] protected LayerMask IgnoreLayer;

    [Header("VFX")]
    [SerializeField] protected GameObject DestroyVFXObj;

    protected virtual void Fly() { }
  }
}