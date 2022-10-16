namespace GameCore.Maze
{
    public sealed class MazeCell
    {
        public bool IsRightWall = false;
        public bool IsBottomWall = false;
        public UnityEngine.Vector3 Position = new UnityEngine.Vector3(0, 0, 0);
    }
}