using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentScoreLabel;
        [SerializeField]
        private TextMeshProUGUI _bestScoreLabel;

        [SerializeField]
        private float _newBestScoreAnimationDuration;

        [SerializeField]
        private AudioSource _bestScoreChangedSound;

        private void Awake()
        {
            var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
            var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                ShowNewBestScoreAnimation();
                SaveNewBestScore(bestScore);
            }

            _currentScoreLabel.text = currentScore.ToString();
            _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
        }

        // Вызывается при нажатии на кнопку рестарта игры
        [UsedImplicitly]
        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }
        
        // Вызывается при нажатии на кнопку выхода из игры
        [UsedImplicitly]
        public void ExitGame()
        {
            // Данная конструкция выполняется только в редакторе Unity
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void SaveNewBestScore(int bestScore)
        {
            PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, bestScore);
            PlayerPrefs.Save();
        }
        
        private void ShowNewBestScoreAnimation()
        {
            _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAnimationDuration, 0);
            _bestScoreChangedSound.Play();
        }
    }
}
