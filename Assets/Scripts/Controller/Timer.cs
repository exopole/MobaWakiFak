using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Controller
{
    [RequireComponent(typeof(TextMeshProUGUI))]    
    public class Timer : MonoBehaviour
    {
        public UnityAction OnEndTimer;
        [Tooltip("Time in seconds")]
        [SerializeField] private float _Time;

        private string _format = "{0}:{1}";
        private TextMeshProUGUI _text;

        public void StartTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private void SetTimerText(TimeSpan time)
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            _text.text = string.Format(_format,time.Minutes, time.Seconds);

        }

        private IEnumerator TimerCoroutine()
        {
            var timeStart = Time.time;
            TimeSpan time;
            do
            {
                time = TimeSpan.FromSeconds(_Time - (Time.time - timeStart));
                SetTimerText(time);
                yield return new  WaitForSeconds(1f);
            } while (time.TotalSeconds > 0);
            
            OnEndTimer?.Invoke();
        }
    }
}