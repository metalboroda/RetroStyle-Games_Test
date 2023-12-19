namespace Test_Game
{
  public class PlayerMovementState : State
  {
    public PlayerMovementState(PlayerController playerController)
    {
      _playerController = playerController;
      _playerMovement = _playerController.PlayerMovement;
      _playerMovementComp = _playerMovement.PlayerMovementComp;
    }

    private PlayerMovementComp _playerMovementComp;

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;

    public override void Update()
    {
      _playerMovementComp.Move(_playerMovement.MovementSpeed, _playerController.InputManager.GetMovementAxis(),
        _playerMovement.CharacterController, _playerController.CameraManager.CameraMain);
      _playerMovementComp.Rotate(_playerMovement.RotationSpeed, _playerController.InputManager.GetLookAxis().x,
        100, _playerMovement.CharacterController);
      _playerMovementComp.Gravity(_playerMovement.CharacterController);

      if (_playerMovement.IsGrounded() == false)
      {
        _playerController.StateMachine.ChangeState(new PlayerInAirState(_playerController));
      }
    }
  }
}