using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerJumpState : State
  {
    public PlayerJumpState(PlayerController playerController)
    {
      _playerController = playerController;
      _playerMovement = _playerController.PlayerMovement;
      _playerMovementComp = new();
    }

    private float _checkGroundTick;

    private PlayerMovementComp _playerMovementComp;

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;

    public override void Enter()
    {
      _checkGroundTick = 0;
    }

    public override void Update()
    {
      _checkGroundTick += Time.deltaTime;

      if (_checkGroundTick >= 0.2f)
      {
        if (_playerMovement.IsGrounded() == true)
        {
          _playerController.StateMachine.ChangeState(new PlayerMovementState(_playerController));
        }
      }

      _playerMovementComp.Move(_playerMovement.MovementSpeed, _playerMovement.InputManager.GetMovementAxis(),
        _playerMovement.CharacterController, _playerMovement.CameraManager.CameraMain);
      _playerMovementComp.Rotate(_playerMovement.RotationSpeed, _playerMovement.InputManager.GetLookAxis().x,
        100, _playerMovement.CharacterController);
      _playerMovementComp.Gravity(1, _playerMovement.CharacterController);
      _playerMovementComp.Jump(_playerMovement.JumpForce, _playerMovement.JumpImpulse,
        _playerMovement.CharacterController, _playerMovement.InputManager.GetMovementAxis(),
        _playerMovement.CameraManager.CameraMain);
    }
  }
}