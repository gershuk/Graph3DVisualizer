using System;

using UnityEngine;

namespace PlayerInputControls
{
    public enum InputType
    {
        Off = 0,
        MenuOnly = 1,
        ToolsOnly = 2,
        All = 3,
    }

    public class PlayerParams
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        public float MovingSpeed { get; }
        public float RotationSpeed { get; }
        public ToolConfig[] ToolConfigs { get; }

        public PlayerParams (Vector3 position, Vector3 eulerAngles, float movingSpeed, float rotationSpeed, ToolConfig[] toolConfigs)
        {
            Position = position;
            EulerAngles = eulerAngles;
            RotationSpeed = rotationSpeed;
            MovingSpeed = movingSpeed;
            ToolConfigs = toolConfigs ?? throw new ArgumentNullException(nameof(toolConfigs));
        }
    }

    public abstract class AbstractPLayer : MonoBehaviour
    {
        protected InputType _inputType;
        public abstract InputType InputType { get; set; } 
    }
}
