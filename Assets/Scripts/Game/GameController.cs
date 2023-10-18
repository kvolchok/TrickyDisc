using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private float _sceneChangeDelay;

        private void Awake()
        {
            // Максимальное значение кадров в секунду
            Application.targetFrameRate = 60;
        }

        // Вызывается по ивенту смерти игрока
        [UsedImplicitly]
        public void OnPlayerDied()
        {
            StartCoroutine(ShowGameOver());
        }

        private IEnumerator ShowGameOver()
        {
            // Задержка, чтобы успели проиграться анимация и звук смерти игрока
            yield return new WaitForSeconds(_sceneChangeDelay);
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE);
        }
    }
}
