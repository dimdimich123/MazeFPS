using UnityEngine;

namespace GameCore.Players.Control
{
    public abstract class MovementController : MonoBehaviour
    {
        protected float _sensitivity;
        protected float _speed;
        protected Transform _cameraTransform;
        protected CharacterController _characterController;
        protected Transform _transform;
        protected PlayerAudio _audio;

        private void Awake()
        {
            _cameraTransform = GetComponentInChildren<Camera>().transform;
            _characterController = GetComponent<CharacterController>();
            _transform = GetComponent<Transform>();
            _audio = GetComponent<PlayerAudio>();
        }

        public void Init(float speed, float sensitivity)
        {
            _speed = speed;
            _sensitivity = sensitivity;
        }

        private void OnEnable()
        {
            StartCoroutine(PlayerUpdate());
        }

        private System.Collections.IEnumerator PlayerUpdate()
        {
            yield return null;

            while(true)
            {
                MovePlayer();
                if (_characterController.velocity.sqrMagnitude > 8f)
                {
                    _audio.PlayStep();
                }

                RotateCamera();
                yield return null;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        protected abstract void RotateCamera();
        protected abstract void MovePlayer();
    }
}


