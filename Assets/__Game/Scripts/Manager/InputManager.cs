using System;
using UnityEngine;

namespace Test_Game
{
  public class InputManager : MonoBehaviour
  {
    public event Action JumpPressed;
    public event Action ShootPressed;
    public event Action UltaPressed;
    public event Action PausePressed;

    public PlayerInputActions PlayerInputActions { get; private set; }

    private void Awake()
    {
      PlayerInputActions = new();
      PlayerInputActions.Enable();
      PlayerInputActions.OnFeet.Jump.performed += OnJump;
      PlayerInputActions.OnFeet.Shoot.performed += OnShoot;
      PlayerInputActions.OnFeet.Ulta.performed += OnUlta;
      PlayerInputActions.General.Pause.performed += OnPause;
    }

    private void OnDestroy()
    {
      PlayerInputActions.OnFeet.Jump.performed -= OnJump;
      PlayerInputActions.OnFeet.Shoot.performed -= OnShoot;
      PlayerInputActions.OnFeet.Ulta.performed -= OnUlta;
      PlayerInputActions.General.Pause.performed -= OnPause;
    }

    public void EnableOnFeetControls()
    {
      PlayerInputActions.OnFeet.Enable();
    }

    public void DisableOnFeetControls()
    {
      PlayerInputActions.OnFeet.Disable();
    }


    public Vector2 GetMovementAxis()
    {
      return PlayerInputActions.OnFeet.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetLookAxis()
    {
      return PlayerInputActions.OnFeet.Look.ReadValue<Vector2>();
    }

    public float GetLookAxisY()
    {
      return PlayerInputActions.OnFeet.Look.ReadValue<Vector2>().y;
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

    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      PausePressed?.Invoke();
    }
  }
}