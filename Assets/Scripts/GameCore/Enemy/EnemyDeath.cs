using GameCore.ObjectPool;
using System;
using UnityEngine;

namespace GameCore.Enemies
{
    public sealed class EnemyDeath : MonoBehaviour
    {
        public event Action OnDie;

        private EnemyHealth _health;
        private bool _isDead;

        public void Init(EnemyHealth health)
        {
            _health = health;
            _isDead = false;

            health.OnChanged += HealthChanged;
        }

        public void Reinit()
        {
            _isDead = false;
        }

        private void HealthChanged(int value)
        {
            if (!_isDead && value <= 0)
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

