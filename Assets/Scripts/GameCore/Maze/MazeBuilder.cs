using UnityEngine;
using GameCore.Configs.MazeConfig;

namespace GameCore.Maze
{
    public sealed class MazeBuilder
    {
        private int _height;
        private int _width;

        private Transform _parent;
        private GameObject _verticalWall;
        private GameObject _horizontalWall;
        private GameObject _floor;

        private MazeCell[,] _maze;
        private float _verticalWallSize;
        private float _horizontalWallSize;
        private float _wallHeight;

        public MazeBuilder(MazeConfig config, Transform parent)
        {
            _parent = parent;
            _height = config.Height;
            _width = config.Width;
            _verticalWall = config.VerticalWall;
            _horizontalWall = config.HorizontalWall;
            _floor = config.Floor;
            _verticalWallSize = _verticalWall.transform.localScale.z;
            _horizontalWallSize = _horizontalWall.transform.localScale.x;
            _wallHeight = _verticalWall.transform.localScale.y;
            MazeGenerator generator = new MazeGenerator(_height, _width, _verticalWallSize, _horizontalWallSize);
            _maze = generator.Maze.MazeCells;
        }

        public void BuildMaze()
        {
            Vector3 position;

            for(int i = 0; i < _height; ++i)
            {
                position = _maze[i, 0].Position;
                position.x -= _horizontalWallSize / 2;
                Object.Instantiate(_verticalWall, position, Quaternion.identity, _parent);
            }

            for (int i = 0; i < _width; ++i)
            {
                position = _maze[0, i].Position;
                position.z += _verticalWallSize / 2;
                Object.Instantiate(_horizontalWall, position, Quaternion.identity, _parent);
            }

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if(_maze[i, j].IsRightWall)
                    {
                        position = _maze[i, j].Position;
                        position.x += _horizontalWallSize / 2;
                        Object.Instantiate(_verticalWall, position, Quaternion.identity, _parent);
                    }
                    if(_maze[i, j].IsBottomWall)
                    {
                        position = _maze[i, j].Position;
                        position.z -= _verticalWallSize / 2;
                        Object.Instantiate(_horizontalWall, position, Quaternion.identity, _parent);
                    }

                    position = _maze[i, j].Position;
                    position.y -= _wallHeight / 2;
                    Object.Instantiate(_floor, position, Quaternion.identity, _parent);
                }
            }
        }
    }
}

