using System;
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

namespace Infrastructure
{
    public sealed class LevelManager : IDisposable
    {
        private const string _defeatHeader = "You Lose!";
        private const string _victoryHeader = "Congratulations! \nYou won!";

        private readonly PlayerDeath _playerDeath;
        private readonly LevelUIManager _levelUI;
        private readonly EnemiesObjectPool _enemiesPool;

        private readonly List<EnemyTypeId> _remainingEnemies = new List<EnemyTypeId>();

        public LevelManager(Player player, LevelUIManager levelUI, EnemyCreationData[] enemiesData, 
            Maze maze, GameFactory gameFactory, ConfigsProvider configProvider, int enemiesAtSameTime)
        {
            
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

            _playerDeath.OnDie += LevelLose;
            _enemiesPool.OnEnemyReturned += OnEnemyDie;
        }

        private void SpawnEnemy()
        {
            int enemyIndex = UnityEngine.Random.Range(0, _remainingEnemies.Count);
            Enemy enemy = _enemiesPool.GetEnemy(_remainingEnemies[enemyIndex]);
            enemy.Active();
            _remainingEnemies.RemoveAt(enemyIndex);
        }

        private void OnEnemyDie()
        {
            if(_remainingEnemies.Count > 0)
            {
                SpawnEnemy();
            }
            else
            {
                if(Enemy.NumberAlive == 0)
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

        public void Dispose()
        {
            _playerDeath.OnDie -= LevelLose;
            _enemiesPool.OnEnemyReturned -= OnEnemyDie;
        }
    }
}