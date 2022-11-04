using UnityEngine;
using UnityEngine.AI;
using Configs;
using GameCore.DynamicMaze;
using GameCore.Players;
using GameCore.CommonLogic;
using GameCore.ObjectPool;

namespace GameCore.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public static int NumberAlive = 0;

        [SerializeField] protected LayerMask _playerMask;
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

        private EnemiesObjectPool _pool;

        public int Attack { get; private set; }

        protected bool _isScared = false;

        public void Init(EnemyConfig config, Maze maze, Transform playerTransform, Player player, EnemiesObjectPool pool)
        {
            _colorDefaut = config.ColorDefault;
            _colorScared = config.ColorScared;

            _materials = new Material[_renderers.Length];
            for (int i = 0; i < _renderers.Length; ++i)
            {
                _materials[i] = _renderers[i].materials[0];
                _materials[i].color = _colorDefaut;
            }

            _pool = pool;
            Attack = config.Attack;
            _player = player;
            _playerTransform = playerTransform;
            _movementController.Init(maze, config.Speed, config.SpeedMoveAway, config.DistanceToFind, config.StoppingDistance, playerTransform);
            _health.Init(config.Health);
            _death.Init(_health);
        }

        public virtual void Reinit()
        {
            _health.Reinit();
            _death.Reinit();
        }

        public void Active()
        {
            NumberAlive++;
        }

        private void ReturnToPool()
        {
            NumberAlive--;
            _pool.ReturnObject(this);
        }

        private void OnEnable()
        {
            if (_player != null && _death != null)
            {
                _player.OnBonusStarted += RunAway;
                _player.OnBonusComplete += StopRunAway;
                _death.OnDie += ReturnToPool;
            }
        }

        private void OnDisable()
        {
            _player.OnBonusStarted -= RunAway;
            _player.OnBonusComplete -= StopRunAway;
            _death.OnDie -= ReturnToPool;
        }

        public virtual void RunAway()
        {
            _isScared = true;
            for (int i = 0; i < _materials.Length; ++i)
            {
                _materials[i].color = _colorScared;
            }
            _movementController.StopAllCoroutines();
        }

        public virtual void StopRunAway()
        {
            _isScared = false;
            for (int i = 0; i < _materials.Length; ++i)
            {
                _materials[i].color = _colorDefaut;
            }
            _movementController.StartPatrol();
        }

        protected abstract void OnTriggerEnter(Collider other);
    }
}