using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private UIManager _uiNanager;

    public override void InstallBindings()
    {
      Container.Bind<CameraManager>().FromInstance(_cameraManager).AsSingle();
      Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
      Container.Bind<UIManager>().FromInstance(_uiNanager).AsSingle();
    }
  }
}