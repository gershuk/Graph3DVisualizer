// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualizer.SupportComponents
{
    public interface IMoveable
    {
        float MovingSpeed { get; set; }
        float RotationSpeed { get; set; }
        event Action<Vector3, UnityEngine.Object> ObjectMoved;
        Vector3 GlobalCoordinates { get; set; }
        Vector3 LocalCoordinates { get; set; }
        void Translate (Vector3 moveVector, float deltaTime);
        void Rotate (Vector2 rotationChange, float deltaTime);
        IEnumerator MoveAlongTrajectory (IReadOnlyList<Vector3> trajectory);
    }

    public interface IDestructible
    {
        event Action<UnityEngine.Object> Destroyed;
    }

    public interface IVisibile
    {
        bool Visibility { get; set; }
        event Action<bool, UnityEngine.Object> VisibleChanged;
    }

    public interface ISelectable
    {
        bool IsSelected { get; set; }
        bool IsHighlighted { get; set; }
        event Action<UnityEngine.Object, bool> SelectedChanged;
        event Action<UnityEngine.Object, bool> HighlightedChanged;
        Color SelectFrameColor { get; set; }
    }
}
