using UnityEngine;

namespace UI.Pause
{
    [RequireComponent(typeof(CanvasGroup), typeof(PauseView))]
    public sealed class PausePresenter : MonoBehaviour
    {
        private PauseView _view;
        private PauseModel _model;

        private void Awake()
        {
            _view = GetComponent<PauseView>();
            CanvasGroup canvas = GetComponent<CanvasGroup>();
            _model = new PauseModel(canvas);
        }

        private void OnEnable()
        {
            _view.OnContinue += Continue;
            _view.OnExit += Exit;
        }

        private void Continue()
        {
            _model.Continue();
        }

        private void Exit()
        {
            _model.Exit();
        }

        private void OnDisable()
        {
            _view.OnContinue -= Continue;
            _view.OnExit -= Exit;
        }
    }
}
