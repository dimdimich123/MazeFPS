using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public sealed class MenuModel
    {
        private const string _musicVariable = "MusicVolume";
        private const string _soundVariable = "SoundVolume";
        private readonly AudioMixer _mixer;

        public MenuModel(AudioMixer mixer)
        {
            _mixer = mixer;
        }

        public void StartLevel()
        {
            SceneManager.LoadScene(Infrastructure.SceneName.Level.ToString());
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void ChangeMusic(bool state)
        {
            float volume = 0;
            if(!state)
            {
                volume = -80f;
            }
            _mixer.SetFloat(_musicVariable, volume);
        }

        public void ChangeSound(bool state)
        {
            float volume = 0;
            if (!state)
            {
                volume = -80f;
            }
            _mixer.SetFloat(_soundVariable, volume);
        }
    }
}