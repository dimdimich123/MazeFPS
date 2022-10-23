using UnityEngine;

namespace GameCore.Enemies
{
    public sealed class SimpleEnemy : Enemy
    {
        public override void RunAway()
        {
            base.RunAway();
            _movementController.Agent.destination = _movementController.Maze.FindPositionToHide(_playerTransform.position, _transform.position);
        }

        public override void StopRunAway()
        {
            base.StopRunAway();
        }
    }
}


