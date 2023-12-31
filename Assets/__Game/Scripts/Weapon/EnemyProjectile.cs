using DG.Tweening;
using Lean.Pool;
using UniRx;
using UnityEngine;

namespace Test_Game
{
  public class EnemyProjectile : Projectile
  {
    private Tweener _flyTween;
    private Tweener _lookTween;
    private CompositeDisposable _chaseDisposable = new();
    private CompositeDisposable _flyDisposable = new();

    private PlayerController _playerController;
    private PlayerArenaOffTeleport _playerArenaOffTeleport;

    public void Init(float speed, int power)
    {
      Speed = speed;
      Power = power;
    }

    private void Start()
    {
      _playerController = PlayerController.Instance;
      _playerArenaOffTeleport = PlayerArenaOffTeleport.Instance;
      _playerArenaOffTeleport.PlayerTeleported += FlyForward;

      Fly();
    }

    private void OnDestroy()
    {
      _playerArenaOffTeleport.PlayerTeleported -= FlyForward;
    }

    private void OnTriggerEnter(Collider other)
    {
      if ((IgnoreLayer.value & (1 << other.gameObject.layer)) != 0)
      {
        return;
      }

      if (other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(Power);
      }

      _flyDisposable.Clear();
      _chaseDisposable.Dispose();
      _flyDisposable.Dispose();

      DestroyProjectile();
    }

    protected override void Fly()
    {
      Observable.EveryUpdate().Subscribe(_ =>
      {
        if (_playerController != null)
        {
          Vector3 targetPosition = _playerController.transform.position + new Vector3(0, 1, 0);

          _lookTween = transform.DOLookAt(targetPosition, 0, AxisConstraint.Y);
          transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }
      }).AddTo(_chaseDisposable);
    }

    private void FlyForward()
    {
      DOTween.Kill(_flyTween);
      DOTween.Kill(_lookTween);

      _chaseDisposable.Clear();

      Observable.EveryUpdate().Subscribe(_ =>
      {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
      }).AddTo(_flyDisposable);
    }

    public void DestroyProjectile()
    {
      DOTween.Kill(_flyTween);
      DOTween.Kill(_lookTween);

      _chaseDisposable.Dispose();
      _flyDisposable.Dispose();

      LeanPool.Spawn(DestroyVFXObj, transform.position, Quaternion.identity);

      Destroy(gameObject);
    }
  }
}