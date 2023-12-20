namespace Test_Game
{
  public class PlayerDeathState : State
  {
    public PlayerDeathState(PlayerController playerController)
    {
      _playerController = playerController;
    }

    private PlayerController _playerController;

    public override void Enter()
    {
      _playerController.InputManager.DisableOnFeetControls();
      _playerController.PlayerMovement.CinemachineInputProvider.enabled = false;
      _playerController.GameController.ChangeState(GameState.Lose);
    }
  }
}