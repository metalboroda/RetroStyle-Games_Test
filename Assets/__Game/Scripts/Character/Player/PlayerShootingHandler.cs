using System.Collections;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerShootingHandler : CharacterShootingHandler
  {
    [SerializeField] private float _shootingPointZOffset = 1;

    [field: Header("Ricochete")]
    [field: SerializeField] public int MaxRandomChance { get; private set; } = 12;
    [SerializeField] private int _lowHealthChance = 0;

    [Header("AutoShoot")]
    [SerializeField] private LayerMask _autoShootLayer;
    [SerializeField] private float _autoShootRate = 1;

    private int _randomChance;

    private Transform _shootingPoint;

    private PlayerHandler _playerHandler;

    [Inject] private DiContainer _projectileContainer;

    [Inject] private CameraManager _cameraManager;
    [Inject] private InputManager _inputManager;

    private Coroutine _autoShootCoroutine;

    private PlayerProjectile _spawnedProjectile;

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

    private void Update()
    {
      if (Application.platform == RuntimePlatform.Android)
      {
        AutoShoot();
      }
    }

    private void OnDestroy()
    {
      _playerHandler.HealthChanged -= ChangeMaxChance;
      _inputManager.ShootPressed -= Shoot;

      if (_autoShootCoroutine != null)
      {
        StopCoroutine(_autoShootCoroutine);
      }
    }

    protected override void Shoot()
    {
      _spawnedProjectile = _projectileContainer.InstantiatePrefabForComponent<PlayerProjectile>(
          Projectile, _shootingPoint.position + _shootingPoint.forward *
          _shootingPointZOffset, _shootingPoint.rotation, null
      );

      _spawnedProjectile.Init(ProjectileSpeed, ProjectilePower, _randomChance);
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

    private void AutoShoot()
    {
      Ray ray = _cameraManager.CameraMain.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

      if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _autoShootLayer))
      {
        if (_autoShootCoroutine == null)
        {
          _autoShootCoroutine = StartCoroutine(DoAutoShoot());
        }
      }
      else
      {
        if (_autoShootCoroutine != null)
        {
          StopCoroutine(_autoShootCoroutine);

          _autoShootCoroutine = null;
        }
      }
    }

    private IEnumerator DoAutoShoot()
    {
      while (true)
      {
        Shoot();

        yield return new WaitForSeconds(_autoShootRate);
      }
    }
  }
}