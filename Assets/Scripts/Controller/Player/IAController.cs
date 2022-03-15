using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terrain;
using UnityEngine;
using Utils.PathFinding;

namespace Controller.Player
{
    public class IAController : MonoBehaviour
    {
        [SerializeField]private PlayerController _PlayerController;
        
        private PathFinding _pathFinding = new PathFinding();
        private List<HexagoneGenerator> _map = new List<HexagoneGenerator>();
        private bool _isInitialize;

        private List<HexagoneGenerator> _path;
        private BehaviourController _behaviour;
        private PlayerController _botController;
        private int _goal;
        private bool _fireProcess;

        private void Awake()
        {
            _behaviour = GetComponent<BehaviourController>();
            _botController = GetComponent<PlayerController>();
            _botController.OnSpawn += InitializeNewPath;
        }
        
        private void Update()
        {
            if (!_isInitialize || _fireProcess) return;
            
            if (_path == null)
            {
                InitializeNewPath();
            }

            if (_path.Count == 0) 
            {
                if (_botController.CurrentMunition > 0 && _botController.CurrentMunition >= _goal)
                {
                    Fire();
                    return;
                }
                InitializeNewPath();
            }
            
            GoTo();
            
        }

        public void Initialize(List<HexagoneGenerator> map)
        {
            _map = map;
            _isInitialize = true;
            _goal = Random.Range(1, 10);
            InitializeNewPath();
        }

        public void Stop()
        {
            _isInitialize = false;
            _fireProcess = false;
        }

        public void Clean()
        {
            _map.Clear();
            _path.Clear();
        }

        private Vector2 GetPosition2D()
        {
            var position = transform.position;
            return new Vector2(position.x, position.z);
        }

        private void InitializeNewPath()
        {
            var hexagons = _map.OrderBy(h => Vector3.Distance(transform.position, h.transform.position)).ToList();

            if (hexagons.Count == 0)
            {
                Debug.Log("No Hexagons");
                return;
            }

            var withMunition = hexagons.FirstOrDefault(h => h.HasElement && h != hexagons[0]);
            if (withMunition == null)
            {
                return;
            }

            _path = _pathFinding.FindPath(hexagons[0], withMunition);
        }

        private void GoTo()
        {
            //safety
            if (_path.Count == 0)
            {
                return;
            }

            // if we are near the end
            if (Vector2.Distance(_path[0].GetPosition2D(), GetPosition2D()) < 0.1f)
            {
                _path.RemoveAt(0);
            }

            //If no more path
            if (_path.Count == 0)
            {
                return;
            }
            _behaviour.AddForce((_path[0].GetPosition2D() - GetPosition2D()).normalized);
        }

        private void Fire()
        {
            StartCoroutine(FireCoroutine());
        }

        private IEnumerator FireCoroutine()
        {
            _fireProcess = true;
            var timeStart = Time.time;
            while (Time.time - timeStart < 0.5f)
            {
                if (_behaviour.AreUnderExplosionEffect)
                {
                    timeStart = Time.time;
                }

                Vector3 target = _PlayerController.transform.position;
                target.y = _botController.PhysicsTransform.position.y;
                _botController.PhysicsTransform.LookAt(target);
                yield return null;
            }
            _botController.Fire();
            _goal = Random.Range(1, 10);
            _fireProcess = false;
        }
    }
}