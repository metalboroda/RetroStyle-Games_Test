using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerProjectile : Projectile
  {
    [SerializeField] private int _maxRicochetCount = 2;

    public bool Ricocheted { get; private set; }

    private int _ricochetCount = 0;
    private bool _flyToTarget;
    private int _randChance;
    private int _maxRandChance;

    private CompositeDisposable _moveToTargetDisposable = new();

    [Inject] private SpawnersController _spawnersController;

    public void Init(float speed, int power, int maxRandChance)
    {
      Speed = speed;
      Power = power;
      _maxRandChance = maxRandChance;

      GenerateRandomChance(maxRandChance);
    }

    protected void Update()
    {
      Fly();
    }

    private void OnDestroy()
    {
      _moveToTargetDisposable.Dispose();
    }

    private void OnTriggerEnter(Collider other)
    {
      if ((IgnoreLayer.value & (1 << other.gameObject.layer)) != 0)
      {
        return;
      }

      if (other.TryGetComponent(out EnemyProjectile enemyProjectile))
      {
        enemyProjectile.DestroyProjectile();

        DestroyProjectile();
      }

      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(Power);
        _ricochetCount++;

        TryToFindNewTarget();
      }
      else
      {
        TryToRicochet();
      }

      if (_ricochetCount >= _maxRicochetCount)
      {
        DestroyProjectile();

        return;
      }
    }

    private void GenerateRandomChance(int value)
    {
      _randChance = Random.Range(0, value + 1);
    }

    protected override void Fly()
    {
      if (!_flyToTarget)
      {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
      }
    }

    private void TryToRicochet()
    {
      if (_randChance != _maxRandChance)
      {
        DestroyProjectile();

        return;
      }

      _ricochetCount++;
      Ricocheted = true;

      Ray ray = new(transform.position, transform.forward);

      if (Physics.Raycast(ray, out RaycastHit hit))
      {
        Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);
        transform.forward = reflectDirection;
        transform.position = hit.point + hit.normal * 0.1f;
      }

      transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void TryToFindNewTarget()
    {
      if (_randChance != _maxRandChance)
      {
        if (Random.Range(0, _maxRandChance + 1) != _maxRandChance)
        {
          DestroyProjectile();

          return;
        }
      }
      else
      {
        EnemyHandler closestEnemy = _spawnersController.GetSecondClosestEnemy(transform.position);

        if (closestEnemy == null)
        {
          DestroyProjectile();

          return;
        }

        _flyToTarget = true;
        Ricocheted = true;

        Observable.EveryUpdate().Subscribe(_ =>
        {
          if (closestEnemy != null)
          {
            MoveTowardsTarget(closestEnemy.transform.position);
          }
          else
          {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
          }
        }).AddTo(_moveToTargetDisposable);
      }
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
      targetPosition.y += 1;

      transform.LookAt(targetPosition);
      transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
      _moveToTargetDisposable.Dispose();

      Destroy(gameObject);
    }
  }
}