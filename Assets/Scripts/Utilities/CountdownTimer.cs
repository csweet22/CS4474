using System;
using TMPro;
using UnityEngine;

namespace Scripts.Utilities
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private TextMeshProUGUI timeText;

        public Action OnTimeOut;
        private bool isTimedOut = false;

        void Update()
        {
            if (!isTimedOut)
            {
                time -= Time.deltaTime;
                timeText.text = FormatTime(time);
                if (time <= 0)
                {
                    OnTimeOut.Invoke();
                    isTimedOut = true;
                }
            }
        }

        private string FormatTime(float time)
        {
            int minutes = Mathf.Clamp(Mathf.FloorToInt(time / 60), 0, int.MaxValue);
            int seconds = Mathf.Clamp(Mathf.FloorToInt(time % 60), 0, int.MaxValue);
            return $"{minutes:D2}:{seconds:D2}";
        }
    }
}
