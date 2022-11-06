using System.Collections;
using System.Collections.Generic;
using GameCore.Players;
using GameCore.ObjectPool;
using Infrastructure.Factory;
using Infrastructure.Data;
using GameCore.DynamicMaze;
using Infrastructure.Configs;
using GameCore.CommonLogic;
using GameCore.Enemies;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public sealed class LevelManager : MonoBehaviour
    {
        private const string _defeatHeader = "You Lose!";
        private const string _victoryHeader = "Congratulations! \nYou won!";

        private Player _player;
        private PlayerDeath _playerDeath;
        private LevelUIManager _levelUI;
        private EnemiesObjectPool _enemiesPool;
        private Maze _maze;
        private GameObject _bonus;
        private Transform _bonusTransform;
        private float _timebetweenSpawnBonus;

        private readonly List<EnemyTypeId> _remainingEnemies = new List<EnemyTypeId>();

        public void Init(Player player, LevelUIManager levelUI, EnemyCreationData[] enemiesData, 
            Maze maze, GameFactory gameFactory, ConfigsProvider configProvider, int enemiesAtSameTime, float timebetweenSpawnBonus)
        {
            _timebetweenSpawnBonus = timebetweenSpawnBonus;
            _maze = maze;
            _player = player;
            _playerDeath = player.GetComponent<PlayerDeath>();
            _levelUI = levelUI;
            _enemiesPool = new EnemiesObjectPool(enemiesData, gameFactory, maze, configProvider.GetEnemyConfig, player);

            for (int i = 0; i < enemiesData.Length; i++)
            {
                for (int j = 0; j < enemiesData[i].Count; j++)
                {
                    _remainingEnemies.Add(enemiesData[i].Type);
                }
            }

            for (int i = 0; i < enemiesAtSameTime; i++)
            {
                SpawnEnemy();
            }

            _bonus = gameFactory.CreateBonus(maze);
            _bonusTransform = _bonus.GetComponent<Transform>();

            _player.OnBonusComplete += OnBonusEnd;
            _playerDeath.OnDie += LevelLose;
            _enemiesPool.OnEnemyReturned += OnEnemyDie;
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_timebetweenSpawnBonus);
            _bonusTransform.position = _maze.GetRandomPositionForMovement();
            _bonus.gameObject.SetActive(true);
        }

        private void SpawnEnemy()
        {
            int enemyIndex = Random.Range(0, _remainingEnemies.Count);
            Enemy enemy = _enemiesPool.GetEnemy(_remainingEnemies[enemyIndex]);
            enemy.gameObject.SetActive(true);
            enemy.Active();
            _remainingEnemies.RemoveAt(enemyIndex);
        }

        private void OnBonusEnd()
        {
            _maze.FreeAllCells();
            StartCoroutine(Timer());
        }

        private void OnEnemyDie()
        {
            if(_remainingEnemies.Count > 0)
            {
                SpawnEnemy();
            }
            else
            if(Enemy.NumberAlive == 0)
            {
                LevelWon();
            }
        }

        private void LevelLose()
        {
            _levelUI.LevelComplete(_defeatHeader);
        }

        private void LevelWon()
        {
            _levelUI.LevelComplete(_victoryHeader);
        }

        public void OnDestroy()
        {
            _player.OnBonusComplete -= OnBonusEnd;
            _playerDeath.OnDie -= LevelLose;
            _enemiesPool.OnEnemyReturned -= OnEnemyDie;
        }
    }
}