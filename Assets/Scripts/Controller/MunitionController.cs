using System.Collections;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Controller
{
    [RequireComponent(typeof(BoxCollider))]
    public class MunitionController : MonoBehaviour
    {
        public UnityAction OnStoppingUse;
        public HexagoneGenerator HexCurrent;
        [SerializeField]
        private bool _IsInUse;

        public bool IsInUse => _IsInUse;

        public float GetHeight()
        {
            return GetComponent<BoxCollider>().size.y;
        }

        /// <summary>
        /// Set inuse bool and call action
        /// </summary>
        /// <param name="state"></param>
        public Task SetUseState(bool state, bool invokeOnStoppingUse = true)
        {
            _IsInUse = state;
            gameObject.SetActive(state);

            if (state) return null;

            if (invokeOnStoppingUse)
            {
                OnStoppingUse?.Invoke();
            }
            
            // put after for the case it spawn in the same hexagon
            if (HexCurrent != null)
            {
                HexCurrent.HasElement = false;
                HexCurrent = null;
            }

            return null;
        }

    }
}