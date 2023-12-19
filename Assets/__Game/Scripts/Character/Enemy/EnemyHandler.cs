using Zenject;

namespace Test_Game
{
  public class EnemyHandler : CharacterHandler, IDamageable
  {
    [Inject] private SpawnersController _spawnersController;

    public void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;
        _spawnersController.RemoveEnemy(this);

        Destroy(gameObject);
      }
    }
  }
}