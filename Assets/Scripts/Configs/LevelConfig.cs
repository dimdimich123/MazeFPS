using UnityEngine;
using Infrastructure;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "LevelConfig", order = 3)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private SceneName _sceneName = SceneName.Level;
        [SerializeField] private MazeConfig _mazeConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public SceneName SceneName => _sceneName;
        public MazeConfig MazeConfig => _mazeConfig;
        public PlayerConfig PlayerConfig => _playerConfig;
    }
}
