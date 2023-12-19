using System;
using UnityEngine;

namespace Test_Game
{
  public class InputManager : MonoBehaviour
  {
    public event Action JumpPressed;
    public event Action ShootPressed;
    public event Action UltaPressed;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
      _playerInputActions = new();
      _playerInputActions.Enable();
      _playerInputActions.OnFeet.Jump.performed += OnJump;
      _playerInputActions.OnFeet.Shoot.performed += OnShoot;
      _playerInputActions.OnFeet.Ulta.performed += OnUlta;
    }

    private void OnDestroy()
    {
      _playerInputActions.OnFeet.Jump.performed -= OnJump;
      _playerInputActions.OnFeet.Shoot.performed -= OnShoot;
      _playerInputActions.OnFeet.Ulta.performed -= OnUlta;
    }

    public void DisableControls()
    {
      _playerInputActions.Disable();
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

    private void OnUlta(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      UltaPressed?.Invoke();
    }
  }
}