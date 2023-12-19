using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerShootingHandler : CharacterShootingHandler
  {
    [SerializeField] private float _shootingPointZOffset = 1;

    private Transform _shootingPoint;

    [Inject] private DiContainer _projectileContainer; // Inject DiContainer directly

    [Inject] private CameraManager _cameraManager;
    [Inject] private InputManager _inputManager;

    private void Awake()
    {
      _inputManager.ShootPressed += Shoot;
    }

    private void Start()
    {
      _shootingPoint = _cameraManager.CameraMain.transform;
    }

    private void OnDestroy()
    {
      _inputManager.ShootPressed -= Shoot;
    }

    protected override void Shoot()
    {
      Projectile spawnedProjectile = _projectileContainer.InstantiatePrefabForComponent<Projectile>(
          Projectile, _shootingPoint.position + _shootingPoint.forward *
          _shootingPointZOffset, _shootingPoint.rotation, null
      );

      spawnedProjectile.Init(ProjectileSpeed, ProjectilePower);
    }
  }
}