using System.Collections;
using UnityEngine;

namespace GameCore.Bonuses
{
    public sealed class BonusMotion : MonoBehaviour
    {
        [Header("Levitation")]
        [SerializeField] private float _liftingDistance;
        [SerializeField] private float _liftingDuration;

        private Vector3 _startPosition;
        private Vector3 _finalPosition;
        private float _timeLevitation = 0;

        [Header("Rotation")]
        [Range(0, 360)]
        [SerializeField] private float _tiltAngle;
        [SerializeField] private float _rotationDuration;

        private Vector3 _startRotation;
        private Vector3 _finalRotation;
        private float _timeRotation = 0;

        private Transform _transform;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            _startPosition = _transform.position;
            _finalPosition = _startPosition;
            _finalPosition.y += _liftingDistance;


            _startRotation = new Vector3(0, 0, _tiltAngle);
            _finalRotation = new Vector3(0, 360, _tiltAngle);
            StartCoroutine(Levitation());
            StartCoroutine(Rotation());
        }

        private IEnumerator Levitation()
        {
            while(true)
            {
                while(_timeLevitation < _liftingDuration)
                {
                    _timeLevitation += Time.deltaTime;
                    _transform.position = Vector3.Lerp(_startPosition, _finalPosition, _timeLevitation / _liftingDuration);
                    yield return null;
                }

                while (_timeLevitation > 0)
                {
                    _timeLevitation -= Time.deltaTime;
                    _transform.position = Vector3.Lerp(_startPosition, _finalPosition, _timeLevitation / _liftingDuration);
                    yield return null;
                }
            }
        }

        private IEnumerator Rotation()
        {
            _transform.Rotate(0, 0, _tiltAngle);
            while (true)
            {
                if(_timeRotation > _rotationDuration)
                {
                    _timeRotation = 0;
                }
                _timeRotation += Time.deltaTime;
                _transform.eulerAngles = Vector3.Lerp(_startRotation, _finalRotation, _timeRotation / _rotationDuration);
                yield return null;
            }
        }
    }
}
