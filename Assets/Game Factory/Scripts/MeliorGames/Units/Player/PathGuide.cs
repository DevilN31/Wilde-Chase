using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Path;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PathGuide : MonoBehaviour
  {
    [HideInInspector] public TrackWaypoints TrackWaypoints;

    public MAnimalAIControl AnimalAI;

    public Waypoint CurrentWaypoint;
    public List<Waypoint> Nodes = new List<Waypoint>();

    [Range(0, 10)] public int DistanceOffset;

    private void Awake()
    {
      TrackWaypoints = GameObject.FindWithTag("Path").GetComponent<TrackWaypoints>();
      Nodes = TrackWaypoints.Nodes;
    }

    private void OnDrawGizmos()
    {
      if (Application.isPlaying)
      {
        CalculateDistanceToWaypoint();
        Gizmos.DrawWireSphere(CurrentWaypoint.transform.position, 3);
      }
    }

    private void Update()
    {
      CalculateDistanceToWaypoint();
    }

    private void CalculateDistanceToWaypoint()
    {
      Vector3 position = transform.position;
      float distance = Mathf.Infinity;

      for (int i = 0; i < Nodes.Count; i++)
      {
        Vector3 difference = Nodes[i].transform.position - position;
        float currentDistance = difference.magnitude;
        
        if (currentDistance < distance)
        {
          if (i + DistanceOffset > Nodes.Count - 1)
          {
            CurrentWaypoint = Nodes[Nodes.Count - 1];
            AnimalAI.SetTarget(CurrentWaypoint.gameObject);
            if (currentDistance <= 2 && CurrentWaypoint.Type == WaypointType.Finish)
            {
              Debug.Log("Finish");
              AnimalAI.Stop();
            }
          }
          else
          {
            CurrentWaypoint = Nodes[i + DistanceOffset];
            AnimalAI.SetTarget(CurrentWaypoint.gameObject);
            distance = currentDistance;
          } 
        }
      }
    }
  }
}