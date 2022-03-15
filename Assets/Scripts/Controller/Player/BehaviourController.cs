using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ScriptableObjects;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    #region variables

    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private BehaviourControllerSO _settings;
    private Rigidbody _physicsRb;

    private Transform _physicsTransform;
    private bool _areUnderExplosionEffect;


    #endregion

    #region Getter/setter

    public CharacterController CharacterController => _CharacterController;

    public Rigidbody PhysicsRb => _physicsRb;

    public Transform PhysicsTransform => _physicsTransform;
    public bool AreUnderExplosionEffect => _areUnderExplosionEffect;

    #endregion

    private void Awake()
    {
        _physicsRb = GetComponentInChildren<Rigidbody>();
        _physicsTransform = _physicsRb.transform;
    }

    public void AddForce(Vector2 direction)
    {
        if (_areUnderExplosionEffect || !_CharacterController.enabled || (_physicsRb && _physicsRb.velocity.y < -0.01f))
        {
            return;
        }
        Vector3 newDirection = new Vector3(direction.x, 0, direction.y);
        _CharacterController.Move(newDirection * _settings.Speed * Time.deltaTime);
        _physicsTransform.LookAt(_physicsTransform.position + newDirection);
    }

    public void Explosion(Vector3 direction, float force)
    {
        StartCoroutine( ExplosionEffectCoroutine(direction, force));
    }

    private IEnumerator ExplosionEffectCoroutine(Vector3 direction, float force)
    {
        Debug.Log("Explosion");
        _areUnderExplosionEffect = true;
        float forceCurrent =  _settings.Curve.Evaluate(force/10); // 10 is the max 
        while (forceCurrent >=0.1f && (_physicsRb && _physicsRb.velocity.y > -0.01f))
        {
            var forceVector = new Vector3(direction.x, 0, direction.z) * _settings.Speed * _settings.BasicExplosionForce * force * Time.deltaTime;
            _CharacterController.Move(forceVector);
            forceCurrent -= _settings.Frictions;
            yield return null;
        }
        
        _areUnderExplosionEffect = false;
    }
}