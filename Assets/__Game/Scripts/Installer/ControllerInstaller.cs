using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private PlayerStatsController _playerStatsController;
    [SerializeField] private SpawnersController _spawnersController;

    public override void InstallBindings()
    {
      Container.Bind<PlayerStatsController>().FromInstance(_playerStatsController).AsSingle();
      Container.Bind<SpawnersController>().FromInstance(_spawnersController).AsSingle();
    }
  }
}