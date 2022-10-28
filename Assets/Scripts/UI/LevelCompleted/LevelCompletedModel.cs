using UnityEngine.SceneManagement;
using Infrastructure;

namespace UI.LevelCompleted
{
    public sealed class LevelCompletedModel
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(SceneName.Menu.ToString());
        }
    }
}
