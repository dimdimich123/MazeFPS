using GameCore.CommonLogic;
using GameCore.Players;
using UnityEngine;

namespace GameCore.Enemies
{
    public sealed class SimpleEnemy : Enemy
    {
        public override void RunAway()
        {
            base.RunAway();
            _movementController.Agent.speed = _movementController.SpeedMoveAway;
            _movementController.Agent.destination = _movementController.Maze.FindPositionToHide(_playerTransform.position, _transform.position);
        }

        public override void StopRunAway()
        {
            _movementController.Agent.speed = _movementController.Speed;
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
                    _health.TakeDamage(damage);
                }
                else
                {
                    other.gameObject.GetComponent<IHealth>().TakeDamage(Attack);
                    _movementController.StartMoveAway();
                }

            }
        }
    }
}


