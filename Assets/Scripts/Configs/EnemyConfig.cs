using UnityEngine;
using GameCore.CommonLogic;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "EnemyConfig", order = 5)]
    public class EnemyConfig : ScriptableObject
    {
        [Range(1, 200)]
        [SerializeField] private int _health = 100;

        [Range(1, 200)]
        [SerializeField] private int _attack = 25;

        [Range(0.01f, 10f)]
        [SerializeField] private float _speed = 4;
        [Range(0.01f, 20f)]
        [SerializeField] private float _speedMoveAway = 6;

        [SerializeField] private Color _colorDefault = Color.white;

        [SerializeField] private Color _colorScared = new Color(255, 255, 255, 127);

        [Range(1f, 100f)]
        [SerializeField] private float _distanceToFind = 10;

        [Range(0.0001f, 2f)]
        [SerializeField] private float _stoppingDistance = 0.1f;

        [SerializeField] private EnemyTypeId _enemyTypeId = EnemyTypeId.Simple;

        public int Health => _health;
        public int Attack => _attack;
        public float Speed => _speed;
        public float SpeedMoveAway => _speedMoveAway;
        public Color ColorDefault => _colorDefault;
        public Color ColorScared => _colorScared;
        public float DistanceToFind => _distanceToFind;
        public float StoppingDistance => _stoppingDistance;
        public EnemyTypeId EnemyTypeId => _enemyTypeId;
    }
}
