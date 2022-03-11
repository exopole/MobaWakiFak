using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    [SerializeField] private Rigidbody _Rigidbody;
    [SerializeField] private CharacterController _CharacterController;

    [SerializeField] private float _Speed = 100;
    [SerializeField] private float _Gravity = 100;

    public void AddForce(Vector2 direction)
    {
        Vector3 newDirection = new Vector3(direction.x, -_Gravity *Time.deltaTime, direction.y);
        // _Rigidbody.MovePosition(_Rigidbody.transform.position + newDirection * _Speed * Time.deltaTime);   
        _CharacterController.Move(newDirection * _Speed * Time.deltaTime);   
    }
}
