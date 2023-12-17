namespace Test_Game
{
  public class PlayerMovementState : State
  {
    public PlayerMovementState(PlayerController playerController)
    {
      _playerController = playerController;
      _playerMovement = _playerController.PlayerMovement;
    }

    private PlayerMovementComp _playerMovementComp = new();

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;

    public override void Update()
    {
      _playerMovementComp.Move(_playerMovement.MovementSpeed, _playerMovement.InputManager.GetMovementAxis(),
        _playerMovement.CharacterController, _playerMovement.CameraManager.CameraMain);
      _playerMovementComp.Rotate(_playerMovement.RotationSpeed, _playerMovement.InputManager.GetLookAxis().x,
        100, _playerMovement.CharacterController);
      _playerMovementComp.Gravity(1, _playerMovement.CharacterController);
    }
  }
}