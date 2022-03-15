using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controller.Player
{
    public class PlayerDetection : MonoBehaviour
    {
        public UnityAction<MunitionController> OnTriggerMunition;
        public UnityAction<ProjectileBehaviour> OnCollisionProjectile;
        private PlayerController _player;
        
        private void Start()
        {
            _player = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            MunitionController munition = other.GetComponent<MunitionController>();
            if (munition)
            {
                OnTriggerMunition?.Invoke(munition);
                return;
            }

            ProjectileBehaviour projectile = other.GetComponentInParent<ProjectileBehaviour>();
            if (projectile && projectile.Player !=_player)
            {
                OnCollisionProjectile?.Invoke(projectile);
                projectile.UnActivate();
            }
        }
    }
}