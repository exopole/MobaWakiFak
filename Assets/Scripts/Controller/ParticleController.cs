using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class ParticleController : MonoBehaviour
    {
        public static ParticleController Instance;
        
        [SerializeField] private ParticleSystem _FireParticle;
        private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Fire(Transform trans)
        {
            ParticleSystem particleSystem = null;
            foreach (var item in _particleSystems)
            {
                if (item.isStopped)
                {
                    particleSystem = item;
                    break;
                }
            }

            if (particleSystem == null)
            {
                particleSystem = Instantiate(_FireParticle);
                _particleSystems.Add(particleSystem);
            }

            particleSystem.transform.position = trans.position;
            particleSystem.transform.rotation = trans.rotation;
            
            particleSystem.Play();
        }
    }
}