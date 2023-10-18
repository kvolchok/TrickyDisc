using DG.Tweening;
using Game;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreLabel;
        [SerializeField]
        private int _rewardPerEnemy;

        [SerializeField]
        private float _animationDuration;
        [SerializeField]
        private float _scaleFactor;

        [SerializeField]
        private AudioSource _scoreChangeAudioClip;

        private int _score;

        private void Awake()
        {
            _scoreLabel.text = "0";
        }

        // Вызывается по ивенту, когда игрок уничтожил врага
        [UsedImplicitly]
        public void AddScore()
        {
            _score += _rewardPerEnemy;
            _scoreChangeAudioClip.Play();
            _scoreLabel.text = _score.ToString();
            _scoreLabel.transform.DOPunchScale(Vector3.one * _scaleFactor, _animationDuration, 0)
                .OnComplete(() => _scoreLabel.transform.localScale = Vector3.one);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(GlobalConstants.SCORE_PREFS_KEY, _score);
            PlayerPrefs.Save();
        }
    }
}
