using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewMazeConfig", menuName = "MazeConfig", order = 2)]
    public sealed class MazeConfig : ScriptableObject
    {
        [SerializeField] private GameObject _verticalWall;
        [SerializeField] private GameObject _horizontalWall;
        [SerializeField] private GameObject _floor;

        [Range(3, 100)]
        [SerializeField] private int _height = 10;

        [Range(3, 100)]
        [SerializeField] private int _width = 10;

        public GameObject VerticalWall => _verticalWall;
        public GameObject HorizontalWall => _horizontalWall;
        public GameObject Floor => _floor;

        public int Height => _height;
        public int Width => _width;
    }
}


