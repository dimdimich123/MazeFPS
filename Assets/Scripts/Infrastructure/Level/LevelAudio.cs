using System.Collections;
using UnityEngine;

namespace Infrastructure.Level
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class LevelAudio : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] private AudioClip _levelStarted;
        [SerializeField] private AudioClip _bonusMusic;
        [SerializeField] private AudioClip _loseMusic;
        [SerializeField] private AudioClip _winMusic;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.PlayOneShot(_levelStarted);
        }

        public void PlayLose()
        {
            _audioSource.clip = _loseMusic;
            _audioSource.Play();
        }

        public void PlayWin()
        {
            _audioSource.clip = _winMusic;
            _audioSource.Play();
        }

        public void StartBonusMusic()
        {
            _audioSource.clip = _bonusMusic;
            _audioSource.Play();
        }

        public void StopBonusMusic()
        {
            StartCoroutine(BonusMusicFade());
        }

        private IEnumerator BonusMusicFade()
        {
            float defaultVolume = _audioSource.volume;
            float time = 0;

            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(defaultVolume, 0, time / _fadeDuration);
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.volume = defaultVolume;
        }
    }
}