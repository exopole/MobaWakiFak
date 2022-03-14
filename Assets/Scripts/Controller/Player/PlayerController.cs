using System;
using System.Collections;
using Terrain;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Controller.Player
{
    [RequireComponent(typeof(BehaviourController))]
    public class PlayerController : MonoBehaviour
    {
        #region variables
        [SerializeField] private Transform _PhysicsTransform, _ExitCannon;


        private BehaviourController _behaviour;
        private PlayerDetection _detection;
        private Vector3 _spawPoint;
        private int _currentMunition, _maxMunition;

        #endregion
        public Transform ExitCannon => _ExitCannon;

        public int CurrentMunition => _currentMunition;
        
        public Transform PhysicsTransform => _PhysicsTransform;

        #region UnityMethods

        private void Start()
        {
            _detection = GetComponentInChildren<PlayerDetection>();
            _detection.OnTriggerMunition += OnDetectMunition;
        }

        private void Update()
        {
            if (_PhysicsTransform.position.y < -5)
            {
                Spawn();
            }
        }
        #endregion

        public void Initialization(Vector3 spawnPoint, int gaugeMax)
        {
            _behaviour = GetComponent<BehaviourController>();
            _spawPoint = spawnPoint;
            _maxMunition = gaugeMax;
            Spawn();
        }

        public void Fire()
        {
            if (_currentMunition > 0)
            {
                ParticleController.Instance.Fire(_ExitCannon);
                GameController.Instance.Fire(this);
                _currentMunition = 0;
            }
        }

        private void OnDetectMunition(MunitionController munition)
        {
            if (_currentMunition >= _maxMunition) return;
            _currentMunition++;
            munition.SetUseState(false);
        }

        private void Spawn()
        {
            _currentMunition = 0;
            _behaviour.CharacterController.enabled = false;
            Vector3 newPos = _spawPoint;
            newPos.y = _behaviour.CharacterController.height;
            transform.position = newPos;
            _PhysicsTransform.LookAt(-_PhysicsTransform.position);;
            StartCoroutine(SpawCoroutine());
        }
        
        private IEnumerator SpawCoroutine()
        {
            yield return null;
            var physicsRb = _behaviour.PhysicsRb;
            physicsRb.velocity = Vector3.zero;
            physicsRb.freezeRotation = true;
            _PhysicsTransform.localPosition = Vector3.up;
            yield return null;
            _behaviour.CharacterController.enabled = true;
            yield return new WaitUntil(() => _behaviour.PhysicsRb.velocity.y < -0.01f);
            physicsRb.freezeRotation = false;
        }

    }
}