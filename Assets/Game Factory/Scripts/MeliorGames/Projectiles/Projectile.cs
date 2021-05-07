using System;
using Game_Factory.Scripts.MeliorGames.Units;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public Team Team;
        public bool Thrown;
        public GameObject View;

        private void Update()
        {
            /*if (Thrown)
            {
                View.transform.eulerAngles += (Vector3.up + Vector3.right) * 2;
            }*/
        }
        
    }
}
