using UnityEngine;
using UnityEngine.Audio;
using Configs;

namespace UI.Menu
{
    [RequireComponent(typeof(MenuView))]
    public sealed class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private MovementControllerConfig _movementConfig;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _buttonClickSound;

        private MenuView _view;
        private MenuModel _model;

        private void Awake()
        {
            _view = GetComponent<MenuView>();
            _model = new MenuModel(_mixer, _movementConfig);
            _view.Init(_mixer, _movementConfig);
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnEnable()
        {
            _view.OnStart += OnStart;
            _view.OnExit += OnExit;
            _view.OnMusic += OnMusic;
            _view.OnSound += OnSound;
            _view.OnSensitivity += OnSensitivityChange;
            _view.OnChangeMovementControl += OnChangeMovementControl;
        }

        private void OnStart()
        {
            _buttonClickSound.Play();
            _model.StartLevel();
        }

        private void OnExit()
        {
            _buttonClickSound.Play();
            _model.Exit();
        }

        private void OnMusic(bool state)
        {
            _buttonClickSound.Play();
            _model.ChangeMusic(state);
        }

        private void OnSound(bool state)
        {
            _buttonClickSound.Play();
            _model.ChangeSound(state);
        }

        private void OnSensitivityChange(float value)
        {
            _model.ChangeSensitivity(value);
        }

        private void OnChangeMovementControl(System.Type type)
        {
            _buttonClickSound.Play();
            _model.ChangeMovementControlType(type);
        }

        private void OnDisable()
        {
            _view.OnStart -= OnStart;
            _view.OnExit -= OnExit;
            _view.OnMusic -= OnMusic;
            _view.OnSound -= OnSound;
            _view.OnSensitivity -= OnSensitivityChange;
            _view.OnChangeMovementControl -= OnChangeMovementControl;
        }
    }
}