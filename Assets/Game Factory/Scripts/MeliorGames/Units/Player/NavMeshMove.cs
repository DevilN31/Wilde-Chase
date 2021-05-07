using System;
using MalbersAnimations.Controller;
using UnityEngine;
using UnityEngine.AI;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class NavMeshMove : MonoBehaviour
  {
    public Camera Camera;
    public NavMeshAgent Agent;
    public MAnimalAIControl AnimalAIControl;

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
          //Agent.SetDestination(hit.point);
          AnimalAIControl.SetDestination(hit.point);
        }
      }
    }
  }
}