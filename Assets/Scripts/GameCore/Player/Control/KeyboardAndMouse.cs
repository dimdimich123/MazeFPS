using UnityEngine;

namespace GameCore.Players.Control
{
    public sealed class KeyboardAndMouse : MovementController
    {
        private const string _moveHorizontal = "Horizontal";
        private const string _moveVertical = "Vertical";
        private float _moveX;
        private float _moveZ;
        private Vector3 _move;

        private const string _mouseHorizontal = "Mouse X";
        private const string _mouseVertical = "Mouse Y";
        private float _mouseX;
        private float _mouseY;
        private float _xRotation = 0f;

        protected override void MovePlayer()
        {
            _moveX = Input.GetAxis(_moveHorizontal);
            _moveZ = Input.GetAxis(_moveVertical);

            _move = _transform.right * _moveX + _transform.forward * _moveZ;

            _characterController.Move(_move * _speed * Time.deltaTime);
        }

        protected override void RotateCamera()
        {
            _mouseX = Input.GetAxis(_mouseHorizontal) * _sensitivity * Time.deltaTime;
            _mouseY = Input.GetAxis(_mouseVertical) * _sensitivity * Time.deltaTime;

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);

            _transform.Rotate(Vector3.up * _mouseX);
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}

