using System;
using System.Collections;
using UnityEngine;
using GameCore.Player.Control;
using Configs;

namespace GameCore.Player
{
    public sealed class Player : MonoBehaviour
    { 
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerDeath _death;
        [SerializeField] private MeshRenderer _renderer;
        private MovementController _movementController;
        private Material _material;

        private Color _colorDefaut;
        private Color _colorHunterMode;

        public event Action<bool> OnTakeBonus;

        public int Attack { get; private set; }

        public void Init(PlayerConfig config, MovementController controller, float mouseSensitivity)
        {
            _colorDefaut = config.ColorDefault;
            _colorHunterMode = config.ColorHunterMode;
            _material = _renderer.materials[0];
            _material.color = _colorDefaut;
            Attack = config.Attack;
            _movementController = controller;
            _movementController.Init(config.Speed, mouseSensitivity);
            _health.Init(config.Health);
            _death.Init(_health);
        }

        public void TakeBonus()
        {
            StopAllCoroutines();
            StartCoroutine(EnterHunterMode());
        }

        private IEnumerator EnterHunterMode()
        {
            _material.color = _colorHunterMode;
            OnTakeBonus?.Invoke(true);
            yield return new WaitForSeconds(15f);
            OnTakeBonus?.Invoke(false);
            _material.color = _colorDefaut;
        }
    }
}

