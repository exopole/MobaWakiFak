using System;
using UnityEngine;

namespace Utils
{
    public class Rotator : MonoBehaviour
    {
        public float Angle;
        public Vector3 AngleRotation = Vector3.up;
        private void FixedUpdate()
        {
            transform.Rotate(AngleRotation, Angle);
        }
    }
}