using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelCompleted
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class LevelCompletedView : MonoBehaviour, IPanel
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private Text _textResult;
        [SerializeField] private Button _buttonGoToMenu;

        public event Action OnGoToMenu;

        private void OnEnable()
        {
            _buttonGoToMenu.onClick.AddListener(GoToMenu);
        }

        private void GoToMenu()
        {
            OnGoToMenu?.Invoke();
        }

        private void OnDisable()
        {
            _buttonGoToMenu.onClick.RemoveAllListeners();
        }

        public void SetResultText(string text) => _textResult.text = text;

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
