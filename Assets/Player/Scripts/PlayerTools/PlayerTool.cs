using System;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Physics;

namespace PlayerInputControls
{
    public readonly struct ToolConfig
    {
        public Type ToolType { get; }
        public object ToolParams { get; }

        public ToolConfig (Type toolType, object toolParams)
        {
            if (!toolType.IsSubclassOf(typeof(PlayerTool)))
                throw new Exception($"{toolType} is not subclass of PlayerTool");
            ToolType = toolType ?? throw new ArgumentNullException(nameof(toolType));
            ToolParams = toolParams;
        }
    }

    public abstract class ToolParams
    { }

    public abstract class PlayerTool : MonoBehaviour
    {
        public abstract void RegisterEvents (IInputActionCollection inputActions);

        protected RaycastHit RayCast (float range)
        {
            Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, range);
            return hit;
        }
    }
}
