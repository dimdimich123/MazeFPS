using UnityEngine;
using GameCore.CommonLogic;

namespace GameCore.Player.Control
{
    public sealed class Joysticks : MovementController, IUIMovementHandler
    {
        private IUIMovementController _movement;
        private IUIMovementController _camera;

        public void Init(IUIMovementController movement, IUIMovementController camera)
        {
            _movement = movement;
            _camera = camera;
        }

        private float _moveX;
        private float _moveZ;
        private Vector3 _move;

        private float _mouseX;
        private float _mouseY;
        private float _xRotation = 0f;

        protected override void MovePlayer()
        {
            _moveX = _movement.AxisX;
            _moveZ = _movement.AxisY;

            _move = _transform.right * _moveX + _transform.forward * _moveZ;

            _characterController.Move(_move * _speed * Time.deltaTime);
        }

        protected override void RotateCamera()
        {
            _mouseX = _camera.AxisX * _sensitivity / 3 * Time.deltaTime;
            _mouseY = _camera.AxisY * _sensitivity / 3 * Time.deltaTime;

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);

            _transform.Rotate(Vector3.up * _mouseX);
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}