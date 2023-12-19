using System;
using UnityEngine;

namespace Test_Game
{
  public class InputManager : MonoBehaviour
  {
    public event Action JumpPressed;
    public event Action ShootPressed;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
      _playerInputActions = new();
      _playerInputActions.Enable();
      _playerInputActions.OnFeet.Jump.performed += OnJump;
      _playerInputActions.OnFeet.Shoot.performed += OnShoot;
    }

    private void OnDestroy()
    {
      _playerInputActions.OnFeet.Jump.performed -= OnJump;
      _playerInputActions.OnFeet.Shoot.performed -= OnShoot;
    }

    public Vector2 GetMovementAxis()
    {
      return _playerInputActions.OnFeet.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetLookAxis()
    {
      return _playerInputActions.OnFeet.Look.ReadValue<Vector2>();
    }

    public float GetLookAxisY()
    {
      return _playerInputActions.OnFeet.Look.ReadValue<Vector2>().y;
    }

    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      JumpPressed?.Invoke();
    }

    private void OnShoot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      ShootPressed?.Invoke();
    }
  }
}