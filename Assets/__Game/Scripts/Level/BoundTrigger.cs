using UnityEngine;

public class BoundTrigger : MonoBehaviour
{
  public LayerMask layerToIgnore;

  private void OnTriggerEnter(Collider other)
  {
    if ((layerToIgnore.value & 1 << other.gameObject.layer) == 0)
    {
      Destroy(other.gameObject);
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if ((layerToIgnore.value & 1 << collision.gameObject.layer) == 0)
    {
      Destroy(collision.gameObject);
    }
  }
}