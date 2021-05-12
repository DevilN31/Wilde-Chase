using Game_Factory.Scripts.MeliorGames.Infrastructure;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class PlayerSpawner : MonoBehaviour
  {
    public GameFactory GameFactory;

    public void Init(GameObject spawn, MWayPoint wayPoint)
    {
      SpawnPlayer(spawn);
      GameFactory.PlayerContainer.Init(wayPoint, Camera.main);
    }

    private void SpawnPlayer(GameObject spawn)
    {
      GameFactory.CreatePlayer(spawn);
      GameFactory.SetCameraFollow();
    }
  }
}