using Terrain;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 _spawPoint;
        
        public void Initialization(Vector3 spawnPoint)
        {
            _spawPoint = spawnPoint;
            Spawn();
        }

        private void Spawn()
        {
            transform.position = _spawPoint;
        }
    }
}