using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Configs;

namespace UI.Menu
{
    public sealed class MenuModel
    {
        public static readonly string MusicVariable = "MusicVolume";
        public static readonly string SoundVariable = "SoundVolume";
        private readonly AudioMixer _mixer;
        private readonly MovementControllerConfig _movementConfig;

        public MenuModel(AudioMixer mixer, MovementControllerConfig movementConfig)
        {
            _mixer = mixer;
            _movementConfig = movementConfig;
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
            _mixer.SetFloat(MusicVariable, volume);
        }

        public void ChangeSound(bool state)
        {
            float volume = 0;
            if (!state)
            {
                volume = -80f;
            }
            _mixer.SetFloat(SoundVariable, volume);
        }

        public void ChangeSensitivity(float value)
        {
            _movementConfig.MouseSensitivity = value;
        }

        public void ChangeMovementControlType(System.Type type)
        {
            _movementConfig.ControllerType = type;
        }
    }
}