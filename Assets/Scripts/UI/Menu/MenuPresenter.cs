using UnityEngine;
using UnityEngine.Audio;

namespace UI.Menu
{
    [RequireComponent(typeof(MenuView))]
    public sealed class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;

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
            _model.StartLevel();
        }

        private void OnExit()
        {
            _model.Exit();
        }

        private void OnMusic(bool state)
        {
            _model.ChangeMusic(state);
        }

        private void OnSound(bool state)
        {
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