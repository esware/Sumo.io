using Managers;

namespace Dev.Scripts
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class CountdownTimer: MonoBehaviour
    {
        public Text countdownText;
        
        private float _totalTime = 10f;
        private LevelManager _levelManager;
        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _totalTime = _levelManager.LevelData.levelCompleteTime;
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            float timeLeft = _totalTime;

            while (timeLeft > 0)
            {
                countdownText.text = timeLeft.ToString("F1"); 
                yield return new WaitForSeconds(0.1f);
                timeLeft -= 0.1f;
            }

            countdownText.text = "0.0";
            GameEvents.loseEvent?.Invoke();
        }
    }

}