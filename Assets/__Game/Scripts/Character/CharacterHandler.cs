using UnityEngine;

namespace Test_Game
{
  public abstract class CharacterHandler : MonoBehaviour
  {
    [SerializeField] public int Health = 100;

    [Header("VFX")]
    [SerializeField] protected GameObject DeathVFXObj;
    [SerializeField] protected float VFXYOffset;
  }
}