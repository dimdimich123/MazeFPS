using UnityEngine;
using Infrastructure.Configs;
using Infrastructure.Factory;
using GameCore.DynamicMaze;
using Configs;
using GameCore.Players.Control;
using GameCore.CommonLogic;
using UI.HUD;
using System.Linq;
using GameCore.Players;
using UI;

namespace Infrastructure
{
    public sealed class LevelBuilder : MonoBehaviour
    {
        private ConfigsProvider _configsProvider;
        private GameFactory _gameFactory;
        private LevelConfig _levelConfig;

        private void Awake()
        {
            _configsProvider = new ConfigsProvider();
            SceneName sceneName = _configsProvider.LoadLevel.SceneName;
            _levelConfig = _configsProvider.GetLevelConfig(sceneName);
            _gameFactory = new GameFactory(_levelConfig);
            
            Build();
        }

        private void Build()
        {
            Maze maze = _gameFactory.CreateMaze();

            MovementControllerConfig movementConfig = _configsProvider.ControllerConfig;
            GameObject player = _gameFactory.CreatePlayer(maze, movementConfig);

            GameObject levelUI = _gameFactory.CreateUI(movementConfig);
            InitUI(levelUI, player);

            InitPlayer(player, levelUI);

            Player playerBehaviour = player.GetComponent<Player>();
            LevelUIManager levelUIManager = levelUI.GetComponent<LevelUIManager>();
            LevelManager levelManager = gameObject.AddComponent<LevelManager>();
            levelManager.Init(playerBehaviour, levelUIManager, _levelConfig.EnemiesData, maze, _gameFactory,
                _configsProvider, _levelConfig.EnemiesAtSameTime, _levelConfig.TimebetweenSpawnBonus);
        }

        private void InitPlayer(GameObject player, GameObject levelUI)
        {
            MovementController _movementController = player.GetComponent<MovementController>();
            if (_movementController is IUIMovementHandler _movementHandler)
            {
                IUIMovementController[] controllers = levelUI.GetComponentsInChildren<IUIMovementController>();
                IUIMovementController movemetController = controllers.First(x => x.UIMovementControlleId == UIMovementControllerTypeId.Movement);
                IUIMovementController cameraController = controllers.First(x => x.UIMovementControlleId == UIMovementControllerTypeId.Camera);
                _movementHandler.Init(movemetController, cameraController);
                if (movemetController is JoystickUI joystickMove && cameraController is JoystickUI joystickCamera)
                {
                    float screenWidth = player.GetComponentInChildren<Camera>().pixelWidth;
                    joystickMove.Init(screenWidth);
                    joystickCamera.Init(screenWidth);
                }
            }
        }

        private void InitUI(GameObject levelUI, GameObject player)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            levelUI.GetComponentInChildren<HealthBar>().Init(health);
        }
    }
}
