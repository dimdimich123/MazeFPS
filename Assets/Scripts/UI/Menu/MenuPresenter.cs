using UnityEngine;
using UnityEngine.Audio;

namespace UI.Menu
{
    [RequireComponent(typeof(MenuView))]
    public sealed class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _buttonClickSound;

        private MenuView _view;
        private MenuModel _model;

        private void Awake()
        {
            _view = GetComponent<MenuView>();
            _model = new MenuModel(_mixer);
        }

        private void OnEnable()
        {
            _view.OnStart += OnStart;
            _view.OnExit += OnExit;
            _view.OnMusic += OnMusic;
            _view.OnSound += OnSound;
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

        private void OnDisable()
        {
            _view.OnStart -= OnStart;
            _view.OnExit -= OnExit;
            _view.OnMusic -= OnMusic;
            _view.OnSound -= OnSound;
        }
    }
}