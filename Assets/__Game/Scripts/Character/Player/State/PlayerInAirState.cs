namespace Test_Game
{
  public class PlayerInAirState : State
  {
    public PlayerInAirState(PlayerController playerController)
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
      _playerMovementComp.Move(_playerMovement.MovementSpeed, _playerMovement.InputManager.GetMovementAxis(),
        _playerMovement.CharacterController, _playerMovement.CameraManager.CameraMain);
      _playerMovementComp.Rotate(_playerMovement.RotationSpeed, _playerMovement.InputManager.GetLookAxis().x,
        100, _playerMovement.CharacterController);
      _playerMovementComp.Gravity(_playerMovement.CharacterController);

      if (_playerMovement.IsGrounded() == true)
      {
        _playerController.StateMachine.ChangeState(new PlayerMovementState(_playerController));
      }
    }
  }
}