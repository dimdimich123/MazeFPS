using System;
using System.Collections;
using UnityEngine;
using GameCore.Players.Control;
using Configs;

namespace GameCore.Players
{
    public sealed class Player : MonoBehaviour
    { 
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerDeath _death;
        [SerializeField] private PlayerAudio _audio;
        [SerializeField] private MeshRenderer _renderer;
        private MovementController _movementController;
        private Material _material;

        private Color _colorDefaut;
        private Color _colorHunterMode;

        public event Action OnBonusStarted;
        public event Action OnBonusComplete;

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
            _health.Init(config.Health, _audio);
            _death.Init(_health);
        }

        public void TakeBonus(float duration)
        {
            StartCoroutine(EnterHunterMode(duration));
        }

        private IEnumerator EnterHunterMode(float duration)
        {
            _material.color = _colorHunterMode;
            OnBonusStarted?.Invoke();
            yield return new WaitForSeconds(duration);
            OnBonusComplete?.Invoke();
            _material.color = _colorDefaut;
        }
    }
}

