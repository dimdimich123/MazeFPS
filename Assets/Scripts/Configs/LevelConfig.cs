using UnityEngine;
using Infrastructure;
using Infrastructure.Data;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "LevelConfig", order = 3)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private LevelNumber _levelNumber;
        [SerializeField] private MazeConfig _mazeConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private int _enemiesAtSameTime;
        [SerializeField] private EnemyCreationData[] _enemiesData;
        [SerializeField] private float _timebetweenSpawnBonus;

        public LevelNumber LevelNumber => _levelNumber;
        public MazeConfig MazeConfig => _mazeConfig;
        public PlayerConfig PlayerConfig => _playerConfig;
        public int EnemiesAtSameTime => _enemiesAtSameTime;
        public EnemyCreationData[] EnemiesData => _enemiesData;
        public float TimebetweenSpawnBonus => _timebetweenSpawnBonus;
    }
}
