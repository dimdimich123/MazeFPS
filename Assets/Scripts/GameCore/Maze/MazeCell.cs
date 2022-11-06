namespace GameCore.DynamicMaze
{
    public sealed class MazeCell
    {
        public bool IsRightWall = false;
        public bool IsBottomWall = false;
        public UnityEngine.Vector3 Position;
        public UnityEngine.Vector3 PositionForMovement;
        public bool IsFree = true;
    }
}