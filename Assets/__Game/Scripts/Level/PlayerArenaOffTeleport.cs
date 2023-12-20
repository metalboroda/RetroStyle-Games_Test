using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerArenaOffTeleport : MonoBehaviour
  {
    public event Action PlayerTeleported;

    public static PlayerArenaOffTeleport Instance { get; private set; }

    [Inject] private NavMeshController _navMeshController;

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
      Vector3 randomTeleportPoint = _navMeshController.GetRandomPointOnNavMesh();

      playerTransform.DOMove(randomTeleportPoint, 0);

      PlayerTeleported?.Invoke();
    }
  }
}