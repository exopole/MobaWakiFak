using System;
using Controller.Player;
using UnityEngine;

namespace Controller
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]private float _Speed, _Puissance = 1;
        private PlayerController _player;
        private void FixedUpdate()
        {
            transform.position += transform.forward * _Speed * Time.deltaTime;
        }

        public void Activate(PlayerController player)
        {
            _player = player;
            var position = player.ExitCannon.position;
            Activate(position, position + player.PhysicsTransform.forward, player.CurrentMunition); 
        }
        
        
        public void Activate(Vector3 startPos, Vector3 targetPos, int puissance)
        {
            transform.position = startPos;
            transform.LookAt(targetPos);
            enabled = true;
            _Puissance = puissance;
        }
    }
}