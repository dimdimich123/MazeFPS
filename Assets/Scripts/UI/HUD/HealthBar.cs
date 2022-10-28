using UnityEngine;
using UnityEngine.UI;
using GameCore.Players;

namespace UI.HUD
{

    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _filledImage;
        private PlayerHealth _health;

        public void Init(PlayerHealth health)
        {
            _health = health;

            _health.OnChanged += ChangeHealth;
        }

        private void ChangeHealth(float value) => _filledImage.fillAmount = value;

        private void OnDestroy()
        {
            _health.OnChanged -= ChangeHealth;
        }
    }
}
