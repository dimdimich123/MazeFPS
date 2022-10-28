using UnityEngine;
using UnityEngine.SceneManagement;
using Infrastructure;

namespace UI.Pause
{
    public sealed class PauseModel
    {
        private readonly CanvasGroup _canvas;

        public PauseModel(CanvasGroup canvas)
        {
            _canvas = canvas;
        }

        public void Continue()
        {
            Time.timeScale = 1f;
            _canvas.Close();
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneName.Menu.ToString());
        }
    }
}
