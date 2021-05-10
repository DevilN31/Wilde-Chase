using UnityEngine;

// ReSharper disable once IdentifierTypo
namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class TrajectoryRenderer : MonoBehaviour
  {
    private LineRenderer lineRenderer;

    private float initialLineWidth;

    private void Awake()
    {
      lineRenderer = GetComponent<LineRenderer>();
      initialLineWidth = lineRenderer.startWidth;
    
      HideTrajectory();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
      lineRenderer.startWidth = initialLineWidth;
    
      Vector3[] points = new Vector3[500];
      lineRenderer.positionCount = points.Length;

      for (int i = 0; i < points.Length; i++)
      {
        float time = i * 0.01f;

        points[i] = origin + speed * time + Physics.gravity * (time * time) / 2f;

        if (points[i].y < 0)
        {
          lineRenderer.positionCount = i + 1;
          break;
        }
      }

      lineRenderer.SetPositions(points);
    }

    public void HideTrajectory()
    {
      lineRenderer.startWidth = 0;
    }
  }
}