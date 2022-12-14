using UnityEngine;
using UnityEngine.AI;

namespace GameCore.DynamicMaze
{
    public sealed class Maze
    {
        private MazeCell[,] _mazeCells;

        public int Height { get; private set; }
        public int Width { get; private set; }

        public Maze(int height, int width, float verticalWallSize, float horizontalWallSize)
        {
            Height = height;
            Width = width;
            _mazeCells = new MazeCell[height, width];

            for(int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; j++)
                {
                    _mazeCells[i, j] = new MazeCell();
                    _mazeCells[i, j].Position = new Vector3(j * horizontalWallSize, 0, -(i * verticalWallSize));
                    _mazeCells[i, j].PositionForMovement = _mazeCells[i, j].Position;
                    _mazeCells[i, j].PositionForMovement.y = 0.5f;
                }
            }

            for (int i = 0; i < height; i++)
            {
                _mazeCells[i, width - 1].IsRightWall = true;
            }

            for (int i = 0; i < width; i++)
            {
                _mazeCells[height - 1, i].IsBottomWall = true;
            }
        }


        public Vector3 FindPositionToHide(Vector3 playerTransform, Vector3 enemyTransform)
        {
            float distanceForPlayer = 0;
            float distanceForEnemy = 0;
            float distanceDifference = float.MinValue;
            float maxDistanceForPlayer = 0;
            float diff = 0;
            MazeCell result = null;
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_mazeCells[i, j].IsFree == false) continue;
                    distanceForPlayer = DistanceOnNavMesh(playerTransform, _mazeCells[i, j].PositionForMovement);
                    distanceForEnemy = DistanceOnNavMesh(enemyTransform, _mazeCells[i, j].PositionForMovement);
                    diff = distanceForPlayer - distanceForEnemy;
                    if (Mathf.Abs(diff) - Mathf.Abs(distanceDifference) < 0.5f)
                    {
                        if (distanceForPlayer > maxDistanceForPlayer)
                        {
                            maxDistanceForPlayer = distanceForPlayer;
                            distanceDifference = diff;
                            result = _mazeCells[i, j];
                        }
                    }
                    else if (diff > distanceDifference)
                    {
                        distanceDifference = diff;
                        maxDistanceForPlayer = distanceForPlayer;
                        result = _mazeCells[i, j];
                    }
                }
            }
            result.IsFree = false;
            return result.PositionForMovement;
        }

        public Vector3 FindPositionToHide(Vector3 position)
        {
            float distance = 0;
            float maxDistance = 0;
            MazeCell result = null;
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_mazeCells[i, j].IsFree == false) continue;
                    distance = DistanceOnNavMesh(position, _mazeCells[i, j].PositionForMovement);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        result = _mazeCells[i, j];
                    }
                }
            }
            result.IsFree = false;
            return result.PositionForMovement;
        }

        public static float DistanceOnNavMesh(Vector3 start, Vector3 end)
        {
            NavMeshPath _path = new NavMeshPath();
            float distance = 0;
            NavMesh.CalculatePath(start, end, NavMesh.AllAreas, _path);
            distance += Vector3.Distance(start, _path.corners[0]);
            for (int k = 1; k < _path.corners.Length; ++k)
            {
                distance += Vector3.Distance(_path.corners[k], _path.corners[k - 1]);
            }
            return distance;
        }

        public Vector3 GetRandomPosition()
        {
            int mazeCellHeightIndex = Random.Range(0, Height);
            int mazeCellWidthIndex = Random.Range(0, Width);
            return _mazeCells[mazeCellHeightIndex, mazeCellWidthIndex].Position;
        }

        public Vector3 GetRandomPositionForMovement()
        {
            int mazeCellHeightIndex = Random.Range(0, Height);
            int mazeCellWidthIndex = Random.Range(0, Width);
            return _mazeCells[mazeCellHeightIndex, mazeCellWidthIndex].PositionForMovement;
        }

        public void FreeAllCells()
        {
            foreach (MazeCell cell in _mazeCells)
            {
                cell.IsFree = true;
            }
        }

        public MazeCell this[int index1, int index2]
        {
            get 
            { 
                if((index1 >= Height || index1 < 0) || ( index2 >= Width || index2 < 0))
                {
                    throw new System.IndexOutOfRangeException();
                }
                return _mazeCells[index1, index2]; 
            }
        }
    }
}


