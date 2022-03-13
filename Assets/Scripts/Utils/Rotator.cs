using System;
using UnityEngine;

namespace Utils
{
    public class Rotator : MonoBehaviour
    {
        public float Angle;
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.up, Angle);
        }
    }
}