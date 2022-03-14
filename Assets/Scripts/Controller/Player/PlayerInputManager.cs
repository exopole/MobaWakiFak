using System;
using Controller.Behaviour;
using UnityEngine;

namespace Controller.Player
{
    [RequireComponent(typeof(BehaviourController))]
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerController _playerController;
        private ActionsMobile _actionsMobile;
        private BehaviourController _behaviourController;
        private void Awake()
        {
            _actionsMobile = new ActionsMobile();
            _behaviourController = GetComponent<BehaviourController>();
        }

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            _actionsMobile.Enable();
        }

        private void OnDisable()
        {
            _actionsMobile.Disable();
        }

        private void Update()
        {
            //Read the movement value
            Vector2 movementInput = _actionsMobile.Player.Move.ReadValue<Vector2>() * -1;
            _behaviourController.AddForce(movementInput.normalized);
            
            
            if (_actionsMobile.Player.Fire.triggered)
            {
                _playerController.Fire();
            }
            
        }
    }
}