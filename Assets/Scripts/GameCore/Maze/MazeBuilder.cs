using UnityEngine;
using Configs;

namespace GameCore.DynamicMaze
{
    public sealed class MazeBuilder
    {
        public Maze CreatedMaze { get; private set; }

        private int _height;
        private int _width;

        private Transform _parent;
        private GameObject _verticalWall;
        private GameObject _horizontalWall;
        private GameObject _floor;

        private float _verticalWallSize;
        private float _horizontalWallSize;
        private float _verticalWallHeight;
        private float _horizontalWallHeight;

        public MazeBuilder(MazeConfig config, Transform parent)
        {
            _parent = parent;
            _height = config.Height;
            _width = config.Width;
            _verticalWall = config.VerticalWall;
            _horizontalWall = config.HorizontalWall;
            _floor = config.Floor;
            _verticalWallSize = _verticalWall.transform.localScale.z;
            _verticalWallHeight = _verticalWall.transform.localScale.y;
            _horizontalWallSize = _horizontalWall.transform.localScale.x;
            _horizontalWallHeight = _horizontalWall.transform.localScale.y;
            MazeGenerator generator = new MazeGenerator(_height, _width, _verticalWallSize, _horizontalWallSize);
            CreatedMaze = generator.Maze;
        }

        public void BuildMaze()
        {
            Vector3 position;

            for(int i = 0; i < _height; ++i)
            {
                position = CreatedMaze[i, 0].Position;
                position.x -= _horizontalWallSize / 2;
                position.y += _horizontalWallHeight / 2;
                Object.Instantiate(_verticalWall, position, Quaternion.identity, _parent);
            }

            for (int i = 0; i < _width; ++i)
            {
                position = CreatedMaze[0, i].Position;
                position.z += _verticalWallSize / 2;
                position.y += _verticalWallHeight / 2;
                Object.Instantiate(_horizontalWall, position, Quaternion.identity, _parent);
            }

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if(CreatedMaze[i, j].IsRightWall)
                    {
                        position = CreatedMaze[i, j].Position;
                        position.x += _horizontalWallSize / 2;
                        position.y += _horizontalWallHeight / 2;
                        Object.Instantiate(_verticalWall, position, Quaternion.identity, _parent);
                    }
                    if(CreatedMaze[i, j].IsBottomWall)
                    {
                        position = CreatedMaze[i, j].Position;
                        position.z -= _verticalWallSize / 2;
                        position.y += _verticalWallHeight / 2;
                        Object.Instantiate(_horizontalWall, position, Quaternion.identity, _parent);
                    }

                    position = CreatedMaze[i, j].Position;
                    Object.Instantiate(_floor, position, Quaternion.identity, _parent);
                }
            }
        }
    }
}

