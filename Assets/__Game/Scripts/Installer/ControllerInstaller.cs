using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private GameController _gameController;
    [SerializeField] private NavMeshController _navMeshController;
    [SerializeField] private PlayerStatsController _playerStatsController;
    [SerializeField] private SpawnersController _spawnersController;

    public override void InstallBindings()
    {
      Container.Bind<GameController>().FromInstance(_gameController).AsSingle();
      Container.Bind<NavMeshController>().FromInstance(_navMeshController).AsSingle();
      Container.Bind<PlayerStatsController>().FromInstance(_playerStatsController).AsSingle();
      Container.Bind<SpawnersController>().FromInstance(_spawnersController).AsSingle();
    }
  }
}