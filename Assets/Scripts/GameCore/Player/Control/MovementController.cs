using UnityEngine;

namespace GameCore.Player.Control
{
    public abstract class MovementController : MonoBehaviour
    {
        protected float _sensitivity;
        protected float _speed;
        protected Transform _cameraTransform;
        protected CharacterController _characterController;
        protected Transform _transform;

        private void Awake()
        {
            _cameraTransform = GetComponentInChildren<Camera>().transform;
            _characterController = GetComponent<CharacterController>();
            _transform = GetComponent<Transform>();
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


