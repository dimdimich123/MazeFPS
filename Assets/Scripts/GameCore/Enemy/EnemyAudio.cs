using UnityEngine;

namespace GameCore.Enemies
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _steps;
        [SerializeField] private AudioClip _takeDamage;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayStep()
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.clip = _steps[Random.Range(0, _steps.Length)];
                _audioSource.Play();
            }

        }

        public void PlayTakeDamage()
        {
            _audioSource.PlayOneShot(_takeDamage);
        }
    }
}