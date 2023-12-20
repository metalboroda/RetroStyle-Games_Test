using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Game
{
  public class PlayerStatsController : MonoBehaviour
  {
    public event Action<int> EnergyAdded;
    public event Action<int> HealthAdded;

    public void AddEnergy(int energy)
    {
      EnergyAdded?.Invoke(energy);
    }

    public void AddHealth(int health)
    {
      HealthAdded?.Invoke(health);
    }

    public void AddRandomEnergyHealth()
    {
      float randomValue = Random.value;

      if (randomValue <= 0.7f)
      {
        AddEnergy(10);
      }
      else
      {
        AddHealth(50);
      }
    }
  }
}