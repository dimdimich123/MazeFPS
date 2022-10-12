namespace Builder.Level
{
    public sealed class Maze
    {
        public bool[,] RightWalls;
        public bool[,] BottomWalls;

        public Maze(int height, int width)
        {
            RightWalls = new bool[height, width];
            BottomWalls = new bool[height, width];

            for (int i = 0; i < height; i++)
            {
                RightWalls[i, width - 1] = true;
            }

            for (int i = 0; i < width; i++)
            {
                BottomWalls[height - 1, i] = true;
            }
        }
    }
}


