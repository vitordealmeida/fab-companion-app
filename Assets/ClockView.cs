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
        private SwitchView _switch;
        private bool _initialized;

        private void Awake()
        {
            _switch = GetComponentInChildren<SwitchView>();
        }

        private void Start()
        {
            if (!_initialized)
            {
                clockText.text = "0:00";
            }
        }

        private void SetClock(TimeSpan time)
        {
            clockText.text = $"{time.Minutes:D}:{time.Seconds:D2}";
        }

        public void StartClock(TimeSpan matchConfigMatchDuration, Action action, bool paused = false)
        {
            _initialized = true;
            _startTime = DateTime.Now;
            _duration = matchConfigMatchDuration;
            _countdownFinishedCallback = action;
            SetClock(_duration == TimeSpan.MaxValue ? TimeSpan.Zero : _duration);
            if (paused)
            {
                _counting = false;
                _switch.SetState(false);
            }
            else
            {
                _counting = true;
                _switch.SetState(true);
            }
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

        public void EnableClock(bool isEnabled)
        {
            _counting = isEnabled;
        }
    }
}