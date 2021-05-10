using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Level
{
    [RequireComponent(typeof(Collider))]
    public class InitialPointTriggerObserver : MonoBehaviour
    {
        public BoxCollider Collider;
        
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider other) =>
            TriggerEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) =>
            TriggerExit?.Invoke(other);
        
        private void OnDrawGizmos()
        {
            if(!Collider)
                return;
      
            Gizmos.color = new Color32(200, 30, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}
