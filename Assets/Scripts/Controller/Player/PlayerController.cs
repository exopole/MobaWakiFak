using Terrain;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _PhysicsTransform;

        public Transform PhysicsTransform => _PhysicsTransform;

        private Vector3 _spawPoint;
        
        public void Initialization(Vector3 spawnPoint)
        {
            _spawPoint = spawnPoint;
            Spawn();
        }

        private void Spawn()
        {
            _PhysicsTransform.position = _spawPoint;
        }
    }
}