using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClockView: MonoBehaviour
    {
        public TMP_Text clockText;
        private bool _counting;
        private DateTime _startTime;
        private TimeSpan _duration;
        private Action _countdownFinishedCallback;

        private void Start()
        {
            clockText.text = string.Empty;
        }

        private void SetClock(TimeSpan time)
        {
            clockText.text = $"{time.Minutes:D}:{time.Seconds:D2}";
        }

        public void StartClock(TimeSpan matchConfigMatchDuration, Action action)
        {
            _counting = true;
            _startTime = DateTime.Now;
            _duration = matchConfigMatchDuration;
            _countdownFinishedCallback = action;
        }

        private void Update()
        {
            if (!_counting)
            {
                _startTime += TimeSpan.FromSeconds(Time.unscaledDeltaTime);
                return;
            }
            var elapsedTime = DateTime.Now - _startTime;
            SetClock(_duration == TimeSpan.MaxValue ? elapsedTime : _duration - elapsedTime);
            if (elapsedTime >= _duration)
            {
                _countdownFinishedCallback?.Invoke();
            }
        }

        public void EnableClock(bool enabled)
        {
            _counting = enabled;
        }
    }
}