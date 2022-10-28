using UnityEngine;

namespace UI.LevelCompleted
{
    [RequireComponent(typeof(CanvasGroup), typeof(LevelCompletedView))]
    public class LevelCompletedPresenter : MonoBehaviour
    {
        private LevelCompletedView _view;
        private LevelCompletedModel _model;

        private void Awake()
        {
            _view = GetComponent<LevelCompletedView>();
            _model = new LevelCompletedModel();
        }

        private void OnEnable()
        {
            _view.OnGoToMenu += GoToMenu;
        }

        private void GoToMenu()
        {
            _model.GoToMenu();
        }

        private void OnDisable()
        {
            _view.OnGoToMenu -= GoToMenu;
        }
    }
}
