using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Game
{
  public class PlayerArenaOffTeleport : MonoBehaviour
  {
    public event Action PlayerTeleported;

    public static PlayerArenaOffTeleport Instance { get; private set; }

    [SerializeField] private List<Transform> _teleportPoints = new();

    private void Awake()
    {
      Instance = this;
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.TryGetComponent(out PlayerHandler playerHandler))
      {
        TeleportPlayer(playerHandler.transform);
      }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
      if (_teleportPoints.Count == 0)
      {
        return;
      }

      Transform randomTeleportPoint = _teleportPoints[Random.Range(0, _teleportPoints.Count)];

      playerTransform.DOMove(randomTeleportPoint.position, 0);

      PlayerTeleported?.Invoke();
    }
  }
}