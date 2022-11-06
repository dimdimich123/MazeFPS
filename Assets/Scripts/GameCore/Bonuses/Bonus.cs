using UnityEngine;
using GameCore.Players;

namespace GameCore.Bonuses
{
    public sealed class Bonus : MonoBehaviour
    {
        [Range(0.01f, 300f)]
        [SerializeField] private float _duration;
        [SerializeField] private LayerMask _playerMask;

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _playerMask) != 0)
            {
                other.gameObject.GetComponent<Player>().TakeBonus(_duration);
                gameObject.SetActive(false);
            }
        }

    }
}