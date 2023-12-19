using System.Collections;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class EnemyShootingHandler : CharacterShootingHandler
  {
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _shootingRate = 3;
    [Inject] private DiContainer _projectileContainer;

    private void Start()
    {
      StartCoroutine(DoShoot());
    }

    protected override void Shoot()
    {
      EnemyProjectile spawnedProjectile = _projectileContainer.InstantiatePrefabForComponent<EnemyProjectile>(
          Projectile, _shootingPoint.position, _shootingPoint.rotation, null);

      spawnedProjectile.Init(ProjectileSpeed, ProjectilePower);
    }

    private IEnumerator DoShoot()
    {
      while (true)
      {
        yield return new WaitForSeconds(_shootingRate);

        Shoot();
      }
    }
  }
}