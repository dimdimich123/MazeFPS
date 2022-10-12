using System;

namespace Builder.Level
{
    public sealed class MazeGenerator
    {
        public Maze Maze { get; private set; }

        public MazeGenerator(int height, int width)
        {
            CreateNewMaze(height, width);
        }

        public void CreateNewMaze(int height, int width)
        {
            Maze = new Maze(height, width);
            CreateMaze(height, width);
        }

        private void CreateMaze(int height, int width)
        {
            Random random = new Random();
            int heightNumber = 0;
            int[] row = new int[width];
            for (int i = 0; i < width; i++)
            {
                row[i] = i;
            }

            int maxValue = width;

            do
            {
                SetRightWalls(row, heightNumber, random);
                SetBottomWalls(row, heightNumber, random);

                for (int i = 0; i < row.Length; i++)
                {
                    if(Maze.BottomWalls[heightNumber, i])
                    {
                        row[i] = maxValue;
                        maxValue++;
                    }
                }

                heightNumber++;
            } while (heightNumber < height - 1);

            SetRightWalls(row, heightNumber, random);
            RemoveRightWallsInLastRow(row, heightNumber);
        }

        private void SetRightWalls(int[] row, int heightNumber, Random random)
        {
            for (int i = 0; i < row.Length - 1; i++)
            {
                if(row[i + 1] != row[i])
                {
                    bool isPutWall = random.Next(0, 2) > 0 ? true : false;
                    if (isPutWall)
                    {
                        Maze.RightWalls[heightNumber, i] = true;
                    }
                    else
                    {
                        row[i + 1] = row[i];
                    }
                }
                else
                {
                    Maze.RightWalls[heightNumber, i] = true;
                }
            }
        }

        private void SetBottomWalls(int[] row, int heightNumber, Random random)
        {
            bool isWayOut = false;
            for (int i = 0; i < row.Length - 1; i++)
            {
                if(row[i] == row[i + 1])
                {
                    bool isPutWall = random.Next(0, 2) > 0 ? true : false;
                    if (isPutWall)
                    {
                        Maze.BottomWalls[heightNumber, i] = true;
                    }
                    else
                    {
                        isWayOut = true;
                    }
                }
                else
                {
                    if (isWayOut)
                    {
                        bool isPutWall = random.Next(0, 2) > 0 ? true : false;
                        if (isPutWall)
                        {
                            Maze.BottomWalls[heightNumber, i] = true;
                        }
                    }
                    isWayOut = false;
                }
            }
            if (isWayOut)
            {
                bool isPutWall = random.Next(0, 2) > 0 ? true : false;
                if (isPutWall)
                {
                    Maze.BottomWalls[heightNumber, row.Length - 1] = true;
                }
            }
        }

        private void RemoveRightWallsInLastRow(int[] row, int heightNumber)
        {
            for (int i = 0; i < row.Length - 1; i++)
            {
                if(row[i] != row[i + 1])
                {
                    Maze.RightWalls[heightNumber, i] = false;
                }
            }
        }
    }
}

