using System;
using UnityEngine;

namespace Builder.Level
{
    public sealed class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private int _height;
        [SerializeField] private int _width;

        [SerializeField] GameObject _verticalWall;
        [SerializeField] GameObject _horizontalWall;

        private Maze _maze;
        private float _wallLength;
        private int _mazeCenterHeight;
        private int _mazeCenterWidth;

        private void Awake()
        {
            MazeGenerator generator = new MazeGenerator(_height, _width); ;
            _maze = generator.Maze;
            _mazeCenterHeight = _height / 2;
            _mazeCenterWidth = _width / 2;
            _wallLength = _verticalWall.transform.localScale.z;
            BuildMaze();
        }

        private void BuildMaze()
        {
            BuildVerticalWalls();
            BuildHorizontalWalls();
        }

        private void BuildVerticalWalls()
        {
            Vector3 position = new Vector3(0, _verticalWall.transform.localScale.y / 2, 0);
            for (int i = 0; i < _height; i++)
            {
                position.z = -(i * _wallLength + _wallLength / 2);
                Instantiate(_verticalWall, position, Quaternion.identity, transform);
            }
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_maze.RightWalls[i, j])
                    {
                        position.x = (j + 1) * _wallLength;
                        position.z = -(i * _wallLength + _wallLength / 2);
                        Instantiate(_verticalWall, position, Quaternion.identity, transform);
                    }
                }
            }
        }

        private void BuildHorizontalWalls()
        {
            Vector3 position = new Vector3(0, _verticalWall.transform.localScale.y / 2, 0);
            for (int i = 0; i < _width; i++)
            {
                position.x = i * _wallLength + _wallLength / 2;
                Instantiate(_horizontalWall, position, Quaternion.identity, transform);
            }
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_maze.BottomWalls[i, j])
                    {
                        position.x = j * _wallLength + _wallLength / 2;
                        position.z = -(i * _wallLength + _wallLength);
                        Instantiate(_horizontalWall, position, Quaternion.identity, transform);
                    }
                }
            }
        }









        //private void Awake()
        //{
        //    MazeGenerator gen = new MazeGenerator(10, 10);
        //    Maze maze = gen.Maze;
        //    int rows = maze.BottomWalls.GetUpperBound(0) + 1;
        //    int columns = maze.BottomWalls.Length / rows;

        //    string str = string.Empty;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            if(maze.BottomWalls[i, j])
        //            {
        //                str += 1 + "\t";
        //            }
        //            else
        //            {
        //                str += 0 + "\t";
        //            }
        //        }
        //        str += "\n";
        //    }
        //    Debug.Log(str);

        //    str = string.Empty;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            if (maze.RightWalls[i, j])
        //            {
        //                str += 1 + "\t";
        //            }
        //            else
        //            {
        //                str += 0 + "\t";
        //            }
        //        }
        //        str += "\n";
        //    }
        //    Debug.Log(str);
        //}

    }
}

