using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.CameraControl;
using Game_Factory.Scripts.MeliorGames.Infrastructure.AssetManagement;
using Game_Factory.Scripts.MeliorGames.LevelManagement;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using MalbersAnimations.Controller;
using UnityEditor;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure
{
    public class GameFactory : MonoBehaviour
    {
        public PlayerContainer PlayerContainer;
        public List<EnemyContainer> Enemies = new List<EnemyContainer>();

        private AssetProvider _assetProvider;

        private void Awake()
        {
            _assetProvider = new AssetProvider();
        }
        
        public void CreatePlayer(GameObject at)
        {
            PlayerContainer = 
                _assetProvider.Instantiate(AssetPath.PlayerPath, at.transform.position, at.transform.rotation).
                GetComponent<PlayerContainer>();
        }

        public EnemyContainer CreateEnemy(GameObject at)
        {
            EnemyContainer enemy = 
                _assetProvider.Instantiate(
                    AssetPath.EnemyPath, at.transform.position, at.transform.rotation).GetComponent<EnemyContainer>();
            
            enemy.Init(PlayerContainer.PlayerTransform, PlayerContainer.ShootTarget);

            Enemies.Add(enemy);

            return enemy;
        }

        public void SetCameraFollow()
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            cameraFollow.SetTarget(PlayerContainer.PlayerTransform);
            //cameraFollow.CalculateOffset();
        }
    }
}
