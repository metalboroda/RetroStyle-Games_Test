using DG.Tweening;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Test_Game
{
  public class EnemyMovement : CharacterMovement
  {
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private int _stoppingDistanceTimeRate = 5;
    [SerializeField] private float _minStoppingDistance = 8;
    [SerializeField] private float _maxStoppingDistance = 16;

    [Header("Look")]
    [SerializeField] private float _lookDuration = 0.5f;

    [Header("")]
    [SerializeField] private EnemyController _enemyController;

    private float _defaultStoppingDistance;

    private Coroutine _changeStoppingDistanceRoutine;

    private PlayerController _playerController;

    private void Awake()
    {
      _enemyController.EnemyHandler.Dead += StopChasingRoutine;
    }

    private void Start()
    {
      _playerController = PlayerController.Instance;
      _agent.stoppingDistance = Random.Range(_minStoppingDistance, _maxStoppingDistance);
      _defaultStoppingDistance = _agent.stoppingDistance;
      _agent.speed = MovementSpeed;
      _agent.stoppingDistance = _defaultStoppingDistance;

      StartChasingPlayer();

      _changeStoppingDistanceRoutine = StartCoroutine(DoChangeStoppingDistance());
    }

    private void Update()
    {
      if (_agent.destination != _playerController.transform.position)
      {
        StartChasingPlayer();
      }

      LookAtPlayer();
    }

    private void OnDestroy()
    {
      _enemyController.EnemyHandler.Dead -= StopChasingRoutine;
    }

    private void StartChasingPlayer()
    {
      _agent.SetDestination(_playerController.transform.position);
    }

    private IEnumerator DoChangeStoppingDistance()
    {
      while (true)
      {
        yield return new WaitForSeconds(_stoppingDistanceTimeRate);

        float newStoppingDistance = Random.Range(_minStoppingDistance, _maxStoppingDistance);

        _agent.stoppingDistance = newStoppingDistance;
      }
    }

    private void LookAtPlayer()
    {
      transform.DOLookAt(_playerController.transform.position, _lookDuration, AxisConstraint.Y);
    }

    private void StopChasingRoutine()
    {
      StopCoroutine(_changeStoppingDistanceRoutine);
    }
  }
}