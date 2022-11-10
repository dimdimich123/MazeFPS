using System;
using UnityEngine;
using UnityEngine.UI;
using GameCore.Players.Control;

namespace UI.Menu
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Toggle _toggleMusic;
        [SerializeField] private Toggle _toggleSound;

        [SerializeField] private Slider _sliderSensitivity;
        [SerializeField] private Toggle _toggleKeyboardAndMouse;
        [SerializeField] private Toggle _toggleJoysticks;

        [SerializeField] private UnityEngine.Audio.AudioMixer _mixer;

        public event Action OnExit;
        public event Action OnStart;
        public event Action<bool> OnMusic;
        public event Action<bool> OnSound;
        public event Action<float> OnSensitivity;
        public event Action<Type> OnChangeMovementControl;

        public void Init(UnityEngine.Audio.AudioMixer mixer, Configs.MovementControllerConfig movementConfig)
        {
            
            mixer.GetFloat(MenuModel.MusicVariable, out float musicVolume);
            if (musicVolume <= -79f)
            {
                _toggleMusic.isOn = false;
            }

            mixer.GetFloat(MenuModel.SoundVariable, out float soundVolume);
            if (soundVolume <= -79f)
            {
                _toggleSound.isOn = false;
            }

            _sliderSensitivity.value = movementConfig.MouseSensitivity;

            if(movementConfig.ControllerType == typeof(Joysticks))
            {
                _toggleJoysticks.isOn = true;
            }
            else
            {
                _toggleKeyboardAndMouse.isOn = true;
            }
        }

        private void OnEnable()
        {
            _buttonExit.onClick.AddListener(OnButtonExit);
            _buttonStart.onClick.AddListener(OnButtonStart);
            _toggleMusic.onValueChanged.AddListener(OnToggleMusic);
            _toggleSound.onValueChanged.AddListener(OnToggleSound);
            _sliderSensitivity.onValueChanged.AddListener(OnSliderSensitivity);
            _toggleKeyboardAndMouse.onValueChanged.AddListener(OnToggleKeyboardAndMouse);
            _toggleJoysticks.onValueChanged.AddListener(OnToggleJoysticks);
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

        private void OnSliderSensitivity(float value)
        {
            OnSensitivity?.Invoke(value);
        }

        private void OnToggleKeyboardAndMouse(bool state)
        {
            if(state)
            {
                OnChangeMovementControl?.Invoke(typeof(KeyboardAndMouse));
            }
        }

        private void OnToggleJoysticks(bool state)
        {
            if(state)
            {
                OnChangeMovementControl?.Invoke(typeof(Joysticks));
            }
        }

        private void OnDisable()
        {
            _buttonExit.onClick.RemoveAllListeners();
            _buttonStart.onClick.RemoveAllListeners();
            _toggleMusic.onValueChanged.RemoveAllListeners();
            _toggleSound.onValueChanged.RemoveAllListeners();
            _sliderSensitivity.onValueChanged.RemoveAllListeners();
            _toggleKeyboardAndMouse.onValueChanged.RemoveAllListeners();
            _toggleJoysticks.onValueChanged.RemoveAllListeners();
        }
    }
}