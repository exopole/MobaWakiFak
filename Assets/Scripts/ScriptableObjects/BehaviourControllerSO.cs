using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BehaviourSo", menuName = "Settings/Behaviour", order = 0)]
    public class BehaviourControllerSO : ScriptableObject
    {
        [SerializeField] private float _Speed = 100, _Frictions = 1, _BasicExplosionForce = 10;
        [SerializeField] private AnimationCurve _Curve;

        public AnimationCurve Curve => _Curve;

        public float Frictions => _Frictions;

        public float BasicExplosionForce => _BasicExplosionForce;

        public float Speed => _Speed;

    }
}