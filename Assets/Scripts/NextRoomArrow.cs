using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomArrow : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      GameManager.instance.LoadNewRoom();
    }
  }
}
