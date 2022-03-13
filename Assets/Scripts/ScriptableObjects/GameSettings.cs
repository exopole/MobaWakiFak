using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private int _NumberOfHole, _NumberOfMunition, _GaugeMax;

        public int NumberOfHole => _NumberOfHole;

        public int NumberOfMunition => _NumberOfMunition;

        public int GaugeMax => _GaugeMax;
    }
}