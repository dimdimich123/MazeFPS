namespace GameCore.Maze
{
    public sealed class Maze
    {
        public MazeCell[,] MazeCells;

        public Maze(int height, int width, float verticalWallSize, float horizontalWallSize)
        {
            MazeCells = new MazeCell[height, width];

            for(int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; j++)
                {
                    MazeCells[i, j] = new MazeCell();
                    MazeCells[i, j].Position = new UnityEngine.Vector3(j * horizontalWallSize, 0, -(i * verticalWallSize));
                }
            }

            for (int i = 0; i < height; i++)
            {
                MazeCells[i, width - 1].IsRightWall = true;
            }

            for (int i = 0; i < width; i++)
            {
                MazeCells[height - 1, i].IsBottomWall = true;
            }
        }
    }
}


