using UnityEngine;

namespace Configs
{ 
    [CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "PlayerConfig", order = 1)]
    public sealed class PlayerConfig: ScriptableObject
    {
        [Range(1, 200)]
        [SerializeField] private int _health = 100;

        [Range(1, 200)]
        [SerializeField] private int _attack = 25;

        [Range(0.01f, 10f)]
        [SerializeField] private float _speed = 4;

        [SerializeField] private Color _colorDefault = Color.white;

        [SerializeField] private Color _colorHunterMode = Color.black;

        public int Health => _health;
        public int Attack => _attack;
        public float Speed => _speed;
        public Color ColorDefault => _colorDefault;
        public Color ColorHunterMode => _colorHunterMode;
    }
}
