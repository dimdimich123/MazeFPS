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
    }
}

