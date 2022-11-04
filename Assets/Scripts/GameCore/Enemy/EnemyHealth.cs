using System;
using UnityEngine;
using GameCore.CommonLogic;

namespace GameCore.Enemies
{
    public sealed class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action<int> OnChanged;
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (_currentHealth != value)
                {
                    _currentHealth = value;
                    OnChanged?.Invoke(_currentHealth);
                }
            }
        }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

        private int _currentHealth;
        private int _maxHealth;

        public void Init(int health)
        {
            CurrentHealth = health;
            MaxHealth = health;
        }

        public void Reinit()
        {
            CurrentHealth = MaxHealth;
        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth <= 0)
            {
                return;
            }
            CurrentHealth -= damage;
        }
    }
}


