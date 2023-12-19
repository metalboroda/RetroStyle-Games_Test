using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private SpawnersController _spawnersController;

    public override void InstallBindings()
    {
      Container.Bind<SpawnersController>().FromInstance(_spawnersController).AsSingle();
    }
  }
}