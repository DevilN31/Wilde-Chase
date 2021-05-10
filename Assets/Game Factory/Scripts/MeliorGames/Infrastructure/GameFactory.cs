using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.CameraControl;
using Game_Factory.Scripts.MeliorGames.Infrastructure.AssetManagement;
using Game_Factory.Scripts.MeliorGames.Level;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using MalbersAnimations.Controller;
using UnityEditor;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure
{
    public class GameFactory : MonoBehaviour
    {
        public PlayerInitialPoint PlayerInitialPoint;
        public List<EnemyInitialPoint> EnemyInitialPoints;

        public MWayPoint InitialWayPoint;
        
        private PlayerContainer playerContainer;
        private List<EnemyContainer> enemies = new List<EnemyContainer>();

        private AssetProvider _assetProvider;

        private void Awake()
        {
            _assetProvider = new AssetProvider();
        }

        private void Start()
        {
            CreatePlayer(PlayerInitialPoint.gameObject);
            SetCameraFollow();

            foreach (EnemyInitialPoint point in EnemyInitialPoints)
            {
                var enemy = CreateEnemy(point.gameObject);
                point.enemies.Add(enemy);
            }
        }

        public void CreatePlayer(GameObject at)
        {
            playerContainer = 
                _assetProvider.Instantiate(AssetPath.PlayerPath, at.transform.position, at.transform.rotation).
                GetComponent<PlayerContainer>();
            
            playerContainer.Init(InitialWayPoint, Camera.main);
        }

        public EnemyContainer CreateEnemy(GameObject at)
        {
            EnemyContainer enemy = 
                _assetProvider.Instantiate(
                    AssetPath.EnemyPath, at.transform.position, at.transform.rotation).GetComponent<EnemyContainer>();
            
            enemy.Init(playerContainer.PlayerTransform, playerContainer.ShootTarget);

            enemies.Add(enemy);

            return enemy;
        }

        public void SetCameraFollow()
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            cameraFollow.SetTarget(playerContainer.PlayerTransform);
        }
    }
}
