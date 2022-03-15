using System;
using Controller.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class Gauge : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.fillAmount = 0;
        }

        public void AddMunition(float percentageFill)
        {
            _image.fillAmount = percentageFill;
        }

        public void ResetMunition()
        {
            _image.fillAmount = 0;
        }
    }
}