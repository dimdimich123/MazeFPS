using UnityEngine;

namespace GameCore.Configs.MazeConfig
{
    public sealed class MazeConfig : ScriptableObject
    {
        [SerializeField] private GameObject _verticalWall;
        [SerializeField] private GameObject _horizontalWall;
        [SerializeField] private GameObject _floor;

        [SerializeField] private int _height;
        [SerializeField] private int _width;

        public GameObject VerticalWall => _verticalWall;
        public GameObject HorizontalWall => _horizontalWall;
        public GameObject Floor => _floor;

        public int Height => _height;
        public int Width => _width;
    }
}


