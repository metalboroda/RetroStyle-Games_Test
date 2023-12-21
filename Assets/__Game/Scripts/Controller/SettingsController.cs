using UnityEngine;

public class SettingsController : MonoBehaviour
{
  private void Awake()
  {
    QualitySettings.vSyncCount = 1;
    Application.targetFrameRate = 120;
  }
}