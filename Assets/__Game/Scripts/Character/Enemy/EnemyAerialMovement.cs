using DG.Tweening;
using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Game
{
  public class EnemyAerialMovement : CharacterMovement
  {
    [SerializeField] private float _minSkyHeight = 5;
    [SerializeField] private float _maxSkyHeight = 8;
    [SerializeField] private int _minPreChaseDelay = 1;
    [SerializeField] private int _maxPreChaseDelay = 3;

    [Header("")]
    [SerializeField] private float _lookDuration = 0.5f;

    private Vector3 _targetPosition;

    private CompositeDisposable _chaseDisposable = new();

    private PlayerController _playerController;

    private void Start()
    {
      _playerController = PlayerController.Instance;

      FlyToTheSky();
    }

    private void Update()
    {
      _targetPosition = _playerController.transform.position + new Vector3(0, 1, 0);

      transform.DOLookAt(_targetPosition, _lookDuration, AxisConstraint.Y);
    }

    private void OnDestroy()
    {
      _chaseDisposable.Dispose();
    }

    private void FlyToTheSky()
    {
      transform.DOMoveY(Random.Range(_minSkyHeight, _maxSkyHeight + 1), MovementSpeed)
        .SetSpeedBased(true).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
          PlayerChase();
        });
    }

    private void PlayerChase()
    {
      Observable.Timer(TimeSpan.FromSeconds(Random.Range(_minPreChaseDelay, _maxPreChaseDelay + 1)))
          .Subscribe(_ =>
          {
            Observable.EveryUpdate().Subscribe(__ =>
            {
              if (_playerController != null)
              {
                transform.DOLookAt(_targetPosition, _lookDuration, AxisConstraint.Y);
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MovementSpeed * Time.deltaTime);
              }
            }).AddTo(_chaseDisposable);
          }).AddTo(_chaseDisposable);
    }
  }
}