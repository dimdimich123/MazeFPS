using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GameCore.DynamicMaze;

namespace GameCore.Enemies
{
    public sealed class EnemyMovement : MonoBehaviour
    {
        private const float _timeToUpdate = 0.1f;

        [SerializeField] private Transform _transform;
        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;

        private Maze _maze;
        public Maze Maze => _maze;

        private float _speed;
        private float _speedMoveAway;
        private float _distanceToFind;
        private float _stoppingDistanceSquare;
        private Transform _player;
        private GameObject _playerGameObject;


        RaycastHit _hitInfo;
        bool _isSeen;
        private int _cellHeight;
        private int _cellWidth;
        private readonly WaitForSeconds _waitForSeconsd = new WaitForSeconds(_timeToUpdate);

        public void Init(Maze maze, float speed, float speedMoveAway, float distanceToFind, float stoppingDistance, Transform player)
        {
            _maze = maze;
            _speed = speed;
            _speedMoveAway = speedMoveAway;
            _distanceToFind = distanceToFind;
            _stoppingDistanceSquare = stoppingDistance * stoppingDistance;
            _player = player;
            _playerGameObject = _player.gameObject;
            _agent.speed = speed;
            _agent.stoppingDistance = stoppingDistance;
        }

        public void StartPatrol()
        {
            StopAllCoroutines();
            StartCoroutine(Patrol());
        }

        private IEnumerator Patrol()
        {
            while (true)
            {
                while (DestinationNotReached())
                {
                    if (PlayerNearby())
                    {
                        StartFollow();
                    }
                    yield return _waitForSeconsd;
                }
                _cellHeight = Random.Range(0, _maze.Height);
                _cellWidth = Random.Range(0, _maze.Width);
                _agent.destination = _maze[_cellHeight, _cellWidth].PositionForMovement;
            }
        }

        private void StartFollow()
        {
            StopAllCoroutines();
            StartCoroutine(Follow());
        }

        private IEnumerator Follow()
        {
            while (PlayerNearby())
            {
                _agent.destination = _player.position;
                yield return _waitForSeconsd;
            }

            StartPatrol();
        }

        public void StartMoveAway()
        {
            StopAllCoroutines();
            StartCoroutine(MoveAway());
        }

        private IEnumerator MoveAway()
        {
            _cellHeight = Random.Range(0, _maze.Height);
            _cellWidth = Random.Range(0, _maze.Width);
            _agent.destination = _maze[_cellHeight, _cellWidth].PositionForMovement;
            _agent.speed = _speedMoveAway;
            while (DestinationNotReached() && PlayerNearby())
            {
                yield return _waitForSeconsd;
            }
            _agent.speed = _speed;
            StartPatrol();
        }

        private bool DestinationNotReached() =>
            Vector3.SqrMagnitude(_transform.position - _agent.destination) >= _stoppingDistanceSquare;

        private bool PlayerNearby()
        {
            Physics.Raycast(_transform.position, _player.position - _transform.position, out _hitInfo, _distanceToFind);
            _isSeen = false;
            if (_hitInfo.collider != null)
            {
                _isSeen = _hitInfo.collider.gameObject == _playerGameObject;
            }
            return _isSeen;
        }
    }
}


