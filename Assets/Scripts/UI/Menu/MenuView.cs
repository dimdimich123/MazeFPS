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

        [SerializeField] private UnityEngine.Audio.AudioMixer _mixer;

        public event Action OnExit;
        public event Action OnStart;
        public event Action<bool> OnMusic;
        public event Action<bool> OnSound;

        private void Awake()
        {
            _mixer.GetFloat("MusicVolume", out float musicVolume);
            if (musicVolume <= -79f)
            {
                _toggleMusic.isOn = false;
            }

            _mixer.GetFloat("SoundVolume", out float soundVolume);
            if (soundVolume <= -79f)
            {
                _toggleSound.isOn = false;
            }
        }

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