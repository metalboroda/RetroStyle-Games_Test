namespace Test_Game
{
  public class PlayerDeathState : State
  {
    public PlayerDeathState(PlayerController playerController)
    {
      _playerController = playerController;
    }

    private PlayerController _playerController;
  }
}