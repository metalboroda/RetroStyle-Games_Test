using UnityEngine;

namespace Test_Game
{
  public class MouseSetup : MonoBehaviour
  {
    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }
}