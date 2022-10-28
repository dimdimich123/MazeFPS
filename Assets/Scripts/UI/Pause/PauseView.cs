using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class PauseView : MonoBehaviour, IPanel
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _buttonExit;

        public event Action OnContinue;
        public event Action OnExit;

        private void OnEnable()
        {
            _buttonContinue.onClick.AddListener(Continue);
            _buttonExit.onClick.AddListener(Exit);
        }

        private void Continue()
        {
            OnContinue?.Invoke();
        }

        private void Exit()
        {
            OnExit?.Invoke();
        }

        private void OnDisable()
        {
            _buttonContinue.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }

        public void Close()
        {
            Time.timeScale = 1f;
            _canvas.Close();
        }

        public void Open()
        {
            Time.timeScale = 0f;
            _canvas.Open();
        }
    }
}
