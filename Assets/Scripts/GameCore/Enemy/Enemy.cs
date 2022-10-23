using UnityEngine;
using UnityEngine.AI;
using Configs;
using GameCore.DynamicMaze;
using GameCore.Players;
using GameCore.CommonLogic;

namespace GameCore.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private EnemyTypeId _enemyTypeId;
        public EnemyTypeId EnemyType => _enemyTypeId;

        [SerializeField] protected Transform _transform;
        [SerializeField] protected EnemyHealth _health;
        [SerializeField] protected EnemyDeath _death;
        [SerializeField] protected MeshRenderer[] _renderers;
        [SerializeField] protected EnemyMovement _movementController;
        protected Material[] _materials;

        protected Color _colorDefaut;
        protected Color _colorScared;
        protected Player _player;
        protected Transform _playerTransform;

        public int Attack { get; private set; }

        public void Init(EnemyConfig config, Maze maze, Transform playerTransform, Player player)
        {
            _colorDefaut = config.ColorDefault;
            _colorScared = config.ColorScared;

            _materials = new Material[_renderers.Length];
            for (int i = 0; i < _renderers.Length; ++i)
            {
                _materials[i] = _renderers[i].materials[0];
                _materials[i].color = _colorDefaut;
            }
            
            Attack = config.Attack;
            _player = player;
            _playerTransform = playerTransform;
            _movementController.Init(maze, config.Speed, config.SpeedMoveAway, config.DistanceToFind, config.StoppingDistance, playerTransform);
            _health.Init(config.Health);
            _death.Init(_health);

            _player.OnBonusStarted += RunAway;
            _player.OnBonusComplete += StopRunAway;

            _movementController.StartPatrol();
        }

        private void OnDestroy()
        {
            _player.OnBonusStarted -= RunAway;
            _player.OnBonusComplete -= StopRunAway;
        }

        public virtual void RunAway()
        {
            for (int i = 0; i < _materials.Length; ++i)
            {
                _materials[i].color = _colorScared;
            }
            _movementController.StopAllCoroutines();
        }

        public virtual void StopRunAway()
        {
            for (int i = 0; i < _materials.Length; ++i)
            {
                _materials[i].color = _colorDefaut;
            }
            _movementController.StartPatrol();
        }

        protected float DistanceOnNavMesh(Vector3 start, Vector3 end)
        {
            NavMeshPath _path = new NavMeshPath();
            float distance = 0;
            NavMesh.CalculatePath(start, end, NavMesh.AllAreas, _path);
            distance += Vector3.Distance(start, _path.corners[0]);
            for (int k = 1; k < _path.corners.Length; ++k)
            {
                distance += Vector3.Distance(_path.corners[k], _path.corners[k - 1]);
            }
            return distance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _playerMask) != 0)
            {
                other.gameObject.GetComponent<IHealth>().TakeDamage(Attack);
                _movementController.StartMoveAway();
            }
        }
    }
}