using Configs;
using Infrastructure.Data;
using System.Collections.Generic;
using Infrastructure.Factory;
using GameCore.DynamicMaze;
using GameCore.CommonLogic;
using GameCore.Enemies;
using System;
using GameCore.Players;

namespace GameCore.ObjectPool
{
    public sealed class EnemiesObjectPool
    {
        private readonly List<Enemy> _pool;
        private readonly Maze _maze;
        private readonly GameFactory _gameFactory;
        private readonly Func<EnemyTypeId, EnemyConfig> _enemyConfigs;
        private readonly Player _player;

        public event Action OnEnemyReturned;

        public EnemiesObjectPool(EnemyCreationData[] enemiesData, GameFactory gameFactory, Maze maze, 
            Func<EnemyTypeId, EnemyConfig> enemyConfigs, Player player)
        {
            _gameFactory = gameFactory;
            _maze = maze;
            _enemyConfigs = enemyConfigs;
            _player = player;
            _pool = new List<Enemy>();

            for (int i = 0; i < enemiesData.Length; i++)
            {
                Enemy enemy = _gameFactory.CreateEnemy(enemiesData[i].Type, _maze);
                enemy.Init(_enemyConfigs(enemiesData[i].Type), _maze, _player.transform, _player, this);
                _pool.Add(enemy);
                enemy.gameObject.SetActive(false);
            }
        }

        public Enemy GetEnemy(EnemyTypeId enemyId)
        {
            Enemy result = null;
            if (_pool.Count > 0)
            {
                int index = _pool.FindIndex(x => x.EnemyType == enemyId);
                if(index != -1)
                {
                    result = _pool[index];
                    _pool.RemoveAt(index);
                    int index1 = UnityEngine.Random.Range(0, _maze.Height);
                    int index2 = UnityEngine.Random.Range(0, _maze.Width);
                    result.transform.position = _maze[index1, index2].Position;
                    result.gameObject.SetActive(true);
                    result.Reinit();
                }
                else
                {
                    result = _gameFactory.CreateEnemy(enemyId, _maze);
                    result.Init(_enemyConfigs(enemyId), _maze, _player.transform, _player, this);
                }
            }
            else
            { 
                result = _gameFactory.CreateEnemy(enemyId, _maze);
                result.Init(_enemyConfigs(enemyId), _maze, _player.transform, _player, this);
            }
            
            return result;
        }

        public void ReturnObject(Enemy obj)
        {
            _pool.Add(obj);
            obj.gameObject.SetActive(false);
            OnEnemyReturned?.Invoke();
        }
    }
}
