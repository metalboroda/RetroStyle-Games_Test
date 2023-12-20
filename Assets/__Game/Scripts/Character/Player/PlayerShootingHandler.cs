using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerShootingHandler : CharacterShootingHandler
  {
    [SerializeField] private float _shootingPointZOffset = 1;

    [field: Header("Ricochete")]
    [field: SerializeField] public int MaxRandomChance { get; private set; } = 10;
    [SerializeField] private int _lowHealthChance = 0;

    private int _randomChance;

    private Transform _shootingPoint;

    private PlayerHandler _playerHandler;

    [Inject] private DiContainer _projectileContainer;

    [Inject] private CameraManager _cameraManager;
    [Inject] private InputManager _inputManager;

    private void Awake()
    {
      _randomChance = MaxRandomChance;

      _playerHandler = GetComponent<PlayerHandler>();
      _playerHandler.HealthChanged += ChangeMaxChance;

      _inputManager.ShootPressed += Shoot;
    }

    private void Start()
    {
      _shootingPoint = _cameraManager.CameraMain.transform;
    }

    private void OnDestroy()
    {
      _playerHandler.HealthChanged -= ChangeMaxChance;
      _inputManager.ShootPressed -= Shoot;
    }

    protected override void Shoot()
    {
      PlayerProjectile spawnedProjectile = _projectileContainer.InstantiatePrefabForComponent<PlayerProjectile>(
          Projectile, _shootingPoint.position + _shootingPoint.forward *
          _shootingPointZOffset, _shootingPoint.rotation, null
      );

      spawnedProjectile.Init(ProjectileSpeed, ProjectilePower, _randomChance);
    }

    private void ChangeMaxChance(int health)
    {
      if (health <= 25)
      {
        _randomChance = _lowHealthChance;
      }
      else
      {
        _randomChance = MaxRandomChance;
      }
    }
  }
}