using GameCore.CommonLogic;
using GameCore.Players;
using UnityEngine;

namespace GameCore.Enemies
{

    public sealed class TeleporingEnemy : Enemy
    {
        public override void RunAway()
        {
            base.RunAway();
            _movementController.Agent.isStopped = true;
            _movementController.Agent.Warp(_movementController.Maze.FindPositionToHide(_playerTransform.position));
        }

        public override void StopRunAway()
        {
            _movementController.Agent.isStopped = false;
            base.StopRunAway();
        }

        public override void Reinit()
        {
            base.Reinit();
            StopRunAway();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _playerMask) != 0)
            {
                if (_isScared)
                {
                    RunAway();
                    int damage = other.gameObject.GetComponent<Player>().Attack;
                    _health.TakeDamage(damage);                }
                else
                {
                    other.gameObject.GetComponent<IHealth>().TakeDamage(Attack);
                    _movementController.StartMoveAway();
                }

            }
        }
    }
}

