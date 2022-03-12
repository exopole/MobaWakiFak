using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private float _Speed = 100;
    private Rigidbody _physicsRb;
    private Transform _physicsTransform;


    private void Awake()
    {
        _physicsRb = GetComponentInChildren<Rigidbody>();
        _physicsTransform = _physicsRb.transform;
    }

    public CharacterController CharacterController => _CharacterController;


    public void AddForce(Vector2 direction)
    {
        if (!_CharacterController.enabled || (_physicsRb && _physicsRb.velocity.y > 0.01f))
        {
            return;
        }
        Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
        _CharacterController.Move(newDirection * _Speed * Time.deltaTime);
        _physicsTransform.LookAt(_physicsTransform.position + newDirection);
    }

}