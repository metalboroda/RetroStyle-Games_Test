using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private InputManager _inputManager;

    public override void InstallBindings()
    {
      Container.Bind<CameraManager>().FromInstance(_cameraManager).AsSingle();
      Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
    }
  }
}