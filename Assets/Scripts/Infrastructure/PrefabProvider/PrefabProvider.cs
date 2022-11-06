using GameCore.CommonLogic;
using GameCore.Enemies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Prefabs
{
    public sealed class PrefabProvider
    {
        private readonly GameObject _player;
        private readonly GameObject _ui;
        private readonly GameObject _joysticks;
        private readonly GameObject _bonus;
        private readonly Dictionary<EnemyTypeId, GameObject> _enemies = new Dictionary<EnemyTypeId, GameObject>();

        public PrefabProvider()
        {
            _player = Resources.Load<GameObject>("Prefabs/Player/Player");
            _ui = Resources.Load<GameObject>("Prefabs/UI/LevelUI");
            _joysticks = Resources.Load<GameObject>("Prefabs/UI/Joysticks");
            _bonus = Resources.Load<GameObject>("Prefabs/Bonus/Bonus");
            _enemies = Resources.LoadAll<Enemy>("Prefabs/Enemies").ToDictionary(x => x.EnemyType, x => x.gameObject);
        }

        public GameObject Player => _player;
        public GameObject UI => _ui;
        public GameObject Joysticks => _joysticks;
        public GameObject Bonus => _bonus;
        public GameObject GetEnemy(EnemyTypeId id) => _enemies.TryGetValue(id, out GameObject enemy) ? enemy : null;
    }
}
