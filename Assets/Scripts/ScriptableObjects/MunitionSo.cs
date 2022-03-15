using System.Collections.Generic;
using System.Linq;
using Controller;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Munitions", menuName = "Settings/Munitions", order = 0)]
    public class MunitionSo : ScriptableObject
    {
        [SerializeField] private List<MunitionController> _Munitions;
        private Dictionary<MunitionController, List<MunitionController>> _munitionPool = new Dictionary<MunitionController, List<MunitionController>>();

        public List<MunitionController> Munitions => _Munitions;

        public MunitionController GetRandomMunitions(Vector2 position, UnityAction OnUse,Transform parent = null)
        {
            var randomMunition = _Munitions[Random.Range(0, _Munitions.Count)];
            var pos3D = new Vector3(position.x, randomMunition.GetHeight() / 2, position.y);
            MunitionController result;
            if (IsNewType(randomMunition))
            {
                 result = AddNewMunitionType(pos3D, randomMunition, parent, OnUse);
            }
            else
            {
                var munitions = _munitionPool[randomMunition];

                result = FindUnusedMunition(munitions);
                if (result)
                {
                    var transform = result.transform;
                    transform.position = pos3D;
                    transform.parent = parent;
                }
                else
                {
                    result = AddNewMunition(pos3D, randomMunition, parent, OnUse);
                }
                
            }
            
            result.SetUseState(true);
            return result;
        }

        public void Clear()
        {
            foreach (var pool in _munitionPool)
            {
                foreach (var munition in pool.Value)
                {
                    munition.SetUseState(false, false);
                }
            }
        }

        private MunitionController AddNewMunitionType(Vector3 position, MunitionController prefab, Transform parent, UnityAction onUse = null)
        {
            var newMunition = NewMunition(position, prefab, parent);
            newMunition.OnStoppingUse += onUse;
            _munitionPool[prefab] = new List<MunitionController> {newMunition};
            return newMunition;
        }
        private MunitionController AddNewMunition(Vector3 position, MunitionController prefab, Transform parent, UnityAction onUse = null)
        {
            var newMunition = NewMunition(position, prefab, parent);
            newMunition.OnStoppingUse += onUse;
            _munitionPool[prefab].Add(newMunition);
            return newMunition;
        }
        
        private MunitionController NewMunition(Vector3 position, MunitionController prefab, Transform parent)
        {
            return Instantiate(prefab, position, Quaternion.identity, parent);
        }

        private bool IsNewType(MunitionController type)
        {
            if (!_munitionPool.ContainsKey(type))
            {
                return true;
            }

            var munitions = _munitionPool[type];
            return munitions == null || munitions.Count == 0;
        }

        private MunitionController FindUnusedMunition(List<MunitionController> munitions)
        {
            foreach (var munition in munitions)
            {
                if (!munition.IsInUse)
                {
                    return munition;
                }
            }

            return null;
        } 
    }
}