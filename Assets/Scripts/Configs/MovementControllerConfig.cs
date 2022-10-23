using System;
using UnityEngine;
using GameCore.Players.Control;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewMovementControllerConfig", menuName = "MovementControllerConfig", order = 4)]
    public class MovementControllerConfig : ScriptableObject
    {
        public Type ControllerType = typeof(KeyboardAndMouse);
        public float MouseSensitivity = 350f;
    }
}
