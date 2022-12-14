using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameCore.CommonLogic;
using Configs;

namespace Infrastructure.Configs
{
    public sealed class ConfigsProvider
    {
        private readonly LoadLevelOption _loadLevelOption;
        private readonly MovementControllerConfig _controllerConfig;
        private readonly Dictionary<EnemyTypeId, EnemyConfig> _enemiesConfigs = new Dictionary<EnemyTypeId, EnemyConfig>();
        private readonly Dictionary<LevelNumber, LevelConfig> _levelsConfigs = new Dictionary<LevelNumber, LevelConfig>();

        public ConfigsProvider()
        {
            _loadLevelOption = Resources.Load<LoadLevelOption>("Configs/LoadLevelOption");
            _controllerConfig = Resources.Load<MovementControllerConfig>("Configs/MovementControllerConfig");
            _enemiesConfigs = Resources.LoadAll<EnemyConfig>("Configs/Enemies").ToDictionary(x => x.EnemyTypeId, x => x);
            _levelsConfigs = Resources.LoadAll<LevelConfig>("Configs/Levels").ToDictionary(x => x.LevelNumber, x => x);
        }

        public LoadLevelOption LoadLevel =>_loadLevelOption;
        public MovementControllerConfig ControllerConfig => _controllerConfig;
        public EnemyConfig GetEnemyConfig(EnemyTypeId id) => _enemiesConfigs.TryGetValue(id, out EnemyConfig config) ? config : null;
        public LevelConfig GetLevelConfig(LevelNumber number) => _levelsConfigs.TryGetValue(number, out LevelConfig config) ? config : null;
    }
}
