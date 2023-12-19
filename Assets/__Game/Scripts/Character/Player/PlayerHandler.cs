using UnityEngine;

namespace Test_Game
{
  public class PlayerHandler : CharacterHandler, IDamageable
  {
    //[SerializeField] private int _energy = 50;

    [Header("")]
    [SerializeField] private PlayerController _playerController;

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }
  }
}