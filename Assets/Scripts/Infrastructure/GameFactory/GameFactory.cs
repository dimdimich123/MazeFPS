using UnityEngine;
using Configs;
using UnityEngine.AI;
using GameCore.DynamicMaze;
using GameCore.CommonLogic;
using Infrastructure.Prefabs;
using GameCore.Players.Control;
using GameCore.Players;
using UI.HUD;
using GameCore.Enemies;

namespace Infrastructure.Factory
{
    public sealed class GameFactory
    {
        private readonly PrefabProvider _prefabProvider;
        private readonly LevelConfig _levelConfig;

        public GameFactory(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _prefabProvider = new PrefabProvider();
        }

        public Maze CreateMaze()
        {
            Transform mazeObject = new GameObject("Maze").transform;
            NavMeshSurface surface = mazeObject.gameObject.AddComponent<NavMeshSurface>();
            surface.collectObjects = CollectObjects.Children;

            MazeBuilder mazeBuilder = new MazeBuilder(_levelConfig.MazeConfig, mazeObject);
            mazeBuilder.BuildMaze();

            surface.BuildNavMesh();
            return mazeBuilder.CreatedMaze;
        }

        public GameObject CreatePlayer(Maze maze, MovementControllerConfig config)
        {
            GameObject player = _prefabProvider.Player;
            Vector3 position = maze.GetRandomPosition();
            position.y += player.transform.localScale.y;

            GameObject _player = Object.Instantiate(player, position, Quaternion.identity); ;
            MovementController _movementController = _player.AddComponent(config.ControllerType) as MovementController;

            Player _playerComponent = _player.GetComponent<Player>();
            _playerComponent.Init(_levelConfig.PlayerConfig, _movementController, config.MouseSensitivity);

            return _player;
        }

        public Enemy CreateEnemy(EnemyTypeId enemyTypeId, Maze maze)
        {
            GameObject enemy = _prefabProvider.GetEnemy(enemyTypeId);
            Vector3 position = maze.GetRandomPosition();
            position.y += enemy.transform.localScale.y;

            GameObject newEnemy = Object.Instantiate(enemy, position, Quaternion.identity);
            return newEnemy.GetComponent<Enemy>();
        }

        public GameObject CreateUI(MovementControllerConfig movementConfig)
        {
            GameObject levelUI = Object.Instantiate(_prefabProvider.UI);

            if(movementConfig.ControllerType == typeof(Joysticks))
            {
                Transform hud = levelUI.GetComponentInChildren<HUDView>().transform;
                Object.Instantiate(_prefabProvider.Joysticks, hud);
            }

            return levelUI;
        }

        public GameObject CreateBonus(Maze maze)
        {
            Vector3 position = maze.GetRandomPosition();
            position.y += 0.5f;
            return Object.Instantiate(_prefabProvider.Bonus, position, Quaternion.identity);
        }
    }
}
