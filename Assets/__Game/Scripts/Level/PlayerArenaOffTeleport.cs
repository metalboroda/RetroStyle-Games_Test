using DG.Tweening;
using Lean.Pool;
using System;
using UnityEngine;
using Zenject;

namespace Test_Game
{
  public class PlayerArenaOffTeleport : MonoBehaviour
  {
    public event Action PlayerTeleported;

    public static PlayerArenaOffTeleport Instance { get; private set; }

    [SerializeField] private GameObject _teleportVFXObj;

    [Inject] private NavMeshController _navMeshController;

    private void Awake()
    {
      Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out PlayerController playerController))
      {
        TeleportPlayer(playerController);
      }
    }

    private void TeleportPlayer(PlayerController player)
    {
      player.PlayerMovement.CharacterController.enabled = false;

      Vector3 randomTeleportPoint = _navMeshController.GetRandomPointOnNavMesh();

      player.transform.DOMove(randomTeleportPoint, 0).OnComplete(() =>
      {
        player.PlayerMovement.CharacterController.enabled = true;

        LeanPool.Spawn(_teleportVFXObj, randomTeleportPoint, Quaternion.identity);
      });

      PlayerTeleported?.Invoke();
    }
  }
}