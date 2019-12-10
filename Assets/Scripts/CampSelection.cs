using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CampSelection : MonoBehaviour
{
  CharacterCombat charCombat;
  [HideInInspector]
  public NavMeshAgent agent;
  public bool allowMovement;

  Ray ray;
  RaycastHit hit;

  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    charCombat = GetComponent<CharacterCombat>();
  }

  private void Movement()
  {
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit))
    {
      if (Input.GetMouseButtonUp(0))
      {
        if (hit.collider.CompareTag("Camp"))
        {
          agent.destination = hit.point;
        }
        else if (hit.collider.CompareTag("NextRoomArrow"))
        {
          agent.destination = hit.point;
        }
        else
        {
          agent.destination = hit.point;
        }
      }
    }
  }

  void Update()
  {
    if (allowMovement)
    {
      Movement();
    }
  }
}
