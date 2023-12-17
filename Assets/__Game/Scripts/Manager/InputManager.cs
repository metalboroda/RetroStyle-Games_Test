using System;
using UnityEngine;

namespace Test_Game
{
  public class InputManager : MonoBehaviour
  {
    public event Action JumpPressed;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
      _playerInputActions = new();
      _playerInputActions.Enable();
      _playerInputActions.OnFeet.Jump.performed += OnJump;
    }

    private void OnDestroy()
    {
      _playerInputActions.OnFeet.Jump.performed -= OnJump;
    }

    public Vector2 GetMovementAxis()
    {
      return _playerInputActions.OnFeet.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetLookAxis()
    {
      return _playerInputActions.OnFeet.Look.ReadValue<Vector2>();
    }

    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      JumpPressed?.Invoke();
    }
  }
}