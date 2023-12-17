using UnityEngine;

namespace Test_Game
{
  public class PlayerMovementComp
  {
    private Vector3 _currentRotation;
    private float _verticalSpeed;

    public void Move(float movementSpeed, Vector2 axis, CharacterController characterController, Camera camera)
    {
      float horizontal = axis.x;
      float vertical = axis.y;

      Vector3 cameraForward = camera.transform.forward;
      Vector3 cameraRight = camera.transform.right;

      cameraForward.y = 0.0f;
      cameraRight.y = 0.0f;

      Vector3 moveDirection = (cameraForward.normalized * vertical + cameraRight.normalized * horizontal).normalized * movementSpeed * Time.deltaTime;

      characterController.Move(moveDirection);
    }

    public void Rotate(float rotationSpeed, float axisX, float rotationSmoothing, CharacterController characterController)
    {
      Vector3 rotation = new Vector3(0, axisX, 0) * rotationSpeed * Time.deltaTime;

      _currentRotation = Vector3.Lerp(_currentRotation, rotation, rotationSmoothing);
      characterController.transform.Rotate(_currentRotation);
    }

    public void Gravity(float gravityMult, CharacterController characterController)
    {
      Vector3 gravityVector = Physics.gravity * gravityMult * Time.deltaTime;

      characterController.Move(gravityVector);
    }

    public void Jump(float jumpForce, CharacterController characterController)
    {
      if (characterController.isGrounded)
      {
        _verticalSpeed = jumpForce;
      }

      _verticalSpeed += Physics.gravity.y * Time.deltaTime;

      Vector3 jumpVector = new Vector3(0, _verticalSpeed, 0) * Time.deltaTime;
      characterController.Move(jumpVector);
    }
  }
}