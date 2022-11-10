using UnityEngine;
using UI.LevelCompleted;
using UI.HUD;
using UI.Pause;

namespace UI
{
    public sealed class LevelUIManager : MonoBehaviour
    {
        [SerializeField] private LevelCompletedView _completeView;
        [SerializeField] private HUDView _hudView;
        [SerializeField] private PauseView _pauseView;

        [SerializeField] private AudioSource _buttonClickSound;

        private IPanel _currentView;
        private bool isJoysticks = false;

        private void Awake()
        {
            _pauseView.Init(_buttonClickSound);

            _pauseView.OnContinue += ContinueLevel;

            _currentView = _hudView;
            _hudView.Open(); 
        }

        private void Start()
        {
            if (GetComponentInChildren<JoystickUI>() != null)
            {
                isJoysticks = true;
            }

            if (SystemInfo.deviceType == DeviceType.Desktop && !isJoysticks)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void ContinueLevel()
        {
            _hudView.Open();
            _currentView = _hudView;
            if (SystemInfo.deviceType == DeviceType.Desktop && !isJoysticks)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void LevelComplete(string result)
        {
            _currentView?.Close();
            _completeView.SetResultText(result);
            _completeView.Open();
            _currentView = _completeView;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (_buttonClickSound.isPlaying == false)
                {
                    _buttonClickSound.Play();
                }
                OnApplicationFocus(false);
            }
        }

        private void OnApplicationFocus(bool pause)
        {
            if(!pause && (Object)_currentView != _completeView)
            {
                _currentView?.Close();
                _pauseView.Open();
                _currentView = _pauseView;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void OnDestroy()
        {
            _pauseView.OnContinue -= ContinueLevel;
        }
    }
}
