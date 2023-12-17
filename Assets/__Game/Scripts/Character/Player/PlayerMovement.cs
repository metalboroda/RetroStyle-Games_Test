using System.Collections;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerMovement : CharacterMovement
  {
    [field: SerializeField] public int RotationSpeed { get; private set; } = 25;

    [field: Header("Jump")]
    [field: SerializeField] public float JumpForce { get; private set; } = 5;

    [Header("Ground check")]
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    [field: Header("")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [Header("")]
    [SerializeField] private PlayerController _playerController;

    public float GravityMult { get; private set; }

    public Camera CameraMain { get; private set; }

    [Inject] public CameraManager CameraManager { get; private set; }
    [Inject] public InputManager InputManager { get; private set; }

    private void Awake()
    {
      InputManager.JumpPressed += Jump;
    }

    private void OnDestroy()
    {
      InputManager.JumpPressed -= Jump;
    }

    public bool IsGrounded()
    {
      Vector3 playerPosition = transform.position;
      bool isGrounded = Physics.CheckSphere(playerPosition, _groundCheckRadius, _groundLayer);

      return isGrounded;
    }

    private void Jump()
    {
      if (IsGrounded() == true)
      {
        _playerController.StateMachine.ChangeState(new PlayerJumpState(_playerController));
      }
    }
  }
}