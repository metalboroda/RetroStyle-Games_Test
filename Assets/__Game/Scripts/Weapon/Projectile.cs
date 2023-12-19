using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] private int _maxRicochetCount = 2;

    public bool Ricocheted { get; private set; }

    private float _speed;
    private int _power;
    private int _ricochetCount = 0;
    private bool _flyToTarget;
    private int _randChance;

    private CompositeDisposable _flyToTargetDisposable = new();

    [Inject] private SpawnersController _spawnersController;

    public void Init(float speed, int power)
    {
      _speed = speed;
      _power = power;
    }

    private void Start()
    {
      RandomChance();
      StartCoroutine(DoDestroyProjectile(8));
    }

    private void Update()
    {
      Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(_power);
        _ricochetCount++;

        if (_randChance != 0)
        {
          if (_ricochetCount >= _maxRicochetCount)
          {
            DestroyProjectile();
          }
          else
          {
            TryToFindNewTarget();
          }
        }
      }
      else
      {
        if (_ricochetCount >= _maxRicochetCount)
        {
          DestroyProjectile();
        }
        else
        {
          TryToRicochet();
        }
      }
    }

    private void RandomChance()
    {
      _randChance = Random.Range(0, 6);
    }

    private void Fly()
    {
      if (_flyToTarget == false)
      {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
      }
    }

    private void TryToRicochet()
    {
      if (_randChance == 0)
      {
        DestroyProjectile();
      }
      else
      {
        _ricochetCount++;
        Ricocheted = true;

        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
          Vector3 reflectDirection = Vector3.Reflect(ray.direction, hit.normal);

          transform.forward = reflectDirection;
          transform.position = hit.point + hit.normal * 0.1f;
        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
      }
    }

    private void TryToFindNewTarget()
    {
      if (_randChance == 0)
      {
        DestroyProjectile();
      }
      else
      {
        EnemyHandler closestEnemy = _spawnersController.GetSecondClosestEnemy(transform.position);

        if (closestEnemy == null)
        {
          DestroyProjectile();
        }
        else
        {
          _flyToTarget = true;

          Observable.EveryUpdate().Subscribe(_ =>
          {
            if (closestEnemy != null)
            {
              MoveTowardsTarget(closestEnemy.transform.position);
            }
          }).AddTo(_flyToTargetDisposable);
        }
      }
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
      targetPosition.y += 1;

      transform.LookAt(targetPosition);
      transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
      _flyToTargetDisposable.Dispose();

      Destroy(gameObject);
    }

    private IEnumerator DoDestroyProjectile(float delay)
    {
      yield return new WaitForSeconds(delay);

      _flyToTargetDisposable.Dispose();

      Destroy(gameObject);
    }
  }
}