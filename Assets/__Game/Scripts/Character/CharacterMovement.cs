using UnityEngine;

namespace Test_Game
{
  public class CharacterMovement : MonoBehaviour
  {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 2.5f;
  }
}