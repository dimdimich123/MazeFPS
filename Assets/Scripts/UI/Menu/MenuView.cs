using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Toggle _toggleMusic;
        [SerializeField] private Toggle _toggleSound;

        public event Action OnExit;
        public event Action OnStart;
        public event Action<bool> OnMusic;
        public event Action<bool> OnSound;

        private void OnEnable()
        {
            _buttonExit.onClick.AddListener(OnButtonExit);
            _buttonStart.onClick.AddListener(OnButtonStart);
            _toggleMusic.onValueChanged.AddListener(OnToggleMusic);
            _toggleSound.onValueChanged.AddListener(OnToggleSound);
        }

        private void OnButtonExit()
        {
            OnExit?.Invoke();
        }

        private void OnButtonStart()
        {
            OnStart?.Invoke();
        }

        private void OnToggleMusic(bool state)
        {
            OnMusic?.Invoke(state);
        }

        private void OnToggleSound(bool state)
        {
            OnSound?.Invoke(state);
        }

        private void OnDisable()
        {
            _buttonExit.onClick.RemoveAllListeners();
            _buttonStart.onClick.RemoveAllListeners();
            _toggleMusic.onValueChanged.RemoveAllListeners();
            _toggleSound.onValueChanged.RemoveAllListeners();
        }
    }
}