using System;
using System.Collections;
using Terrain;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Controller.Player
{
    [RequireComponent(typeof(BehaviourController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _PhysicsTransform;
        private BehaviourController _behaviour;
        private Vector3 _spawPoint;
        
        public Transform PhysicsTransform => _PhysicsTransform;

        private void Update()
        {
            if (_PhysicsTransform.position.y < -5)
            {
                Spawn();
            }
        }

        public void Initialization(Vector3 spawnPoint)
        {
            _behaviour = GetComponent<BehaviourController>();
            _spawPoint = spawnPoint;
        }

        private void Spawn()
        {
            _behaviour.CharacterController.enabled = false;
            Vector3 newPos = _spawPoint;
            newPos.y = _behaviour.CharacterController.height;
            transform.position = newPos;
            _PhysicsTransform.localPosition = Vector3.up;
            _PhysicsTransform.localEulerAngles = Vector3.zero;
            StartCoroutine(SpawCoroutine());
        }

        private IEnumerator SpawCoroutine()
        {
            yield return null;
            _behaviour.CharacterController.enabled = true;

        }
    }
}