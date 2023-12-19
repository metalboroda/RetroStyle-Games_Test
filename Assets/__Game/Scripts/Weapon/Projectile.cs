using UnityEngine;

namespace Test_Game
{
  public class Projectile : MonoBehaviour
  {
    protected float Speed;
    protected int Power;

    [Header("")]
    [SerializeField] protected float AutoDestroyTime = 5;

    protected virtual void Fly() { }
  }
}