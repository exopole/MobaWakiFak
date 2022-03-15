using System;
using Controller.Player;
using UnityEngine;

namespace Controller
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]private float _Speed, _Puissance = 1;
        private PlayerController _player;

        public float Speed => _Speed;

        public float Puissance => _Puissance;

        public PlayerController Player => _player;

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

        public void UnActivate()
        {
            gameObject.SetActive(false);
            enabled = false;
        }
        
        
        private void Activate(Vector3 startPos, Vector3 targetPos, int puissance)
        {
            gameObject.SetActive(true);
            transform.position = startPos;
            transform.LookAt(targetPos);

            var scale = (float)puissance / 10;
            transform.localScale = new Vector3(scale, scale, scale);
            
            enabled = true;
            _Puissance = puissance;
        }
        
        
    }
}