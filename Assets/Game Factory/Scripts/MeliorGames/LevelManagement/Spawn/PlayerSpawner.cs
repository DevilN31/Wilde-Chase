using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class PlayerSpawner : MonoBehaviour
  {
    private GameFactory gameFactory;
    private LevelContainer levelContainer;

    public void Init(GameFactory _gameFactory, GameObject spawn, MWayPoint wayPoint, LevelContainer _levelContainer)
    {
      gameFactory = _gameFactory;
      levelContainer = _levelContainer;
      SpawnPlayer(spawn);
      gameFactory.PlayerContainer.Init(wayPoint, Camera.main, levelContainer);
    }

    private void SpawnPlayer(GameObject spawn)
    {
      gameFactory.CreatePlayer(spawn);
      gameFactory.SetCameraFollow();
    }
  }
}