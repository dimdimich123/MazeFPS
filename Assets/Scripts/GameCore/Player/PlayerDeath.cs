using System;
using UnityEngine;

namespace GameCore.Player
{
    public sealed class PlayerDeath : MonoBehaviour
    {
        public event Action OnDie;

        private PlayerHealth _health;
        private bool _isDead = false;

        public void Init(PlayerHealth health)
        {
            _health = health;

            health.OnChanged += HealthChanged;
        }
        
        private void HealthChanged(float value)
        {
            if(!_isDead && value <= 0)
            {
                Die();
            }
        }

        private void OnDestroy()
        {
            _health.OnChanged -= HealthChanged;
        }

        private void Die()
        {
            _isDead = true;
            OnDie?.Invoke();
        }
    }
}

