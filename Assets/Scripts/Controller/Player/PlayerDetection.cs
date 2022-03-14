using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controller.Player
{
    public class PlayerDetection : MonoBehaviour
    {
        public UnityAction<MunitionController> OnTriggerMunition;
        private void OnTriggerEnter(Collider other)
        {
            MunitionController munition = other.GetComponent<MunitionController>();
            if (munition)
            {
                OnTriggerMunition?.Invoke(munition);
            }
        }
    }
}