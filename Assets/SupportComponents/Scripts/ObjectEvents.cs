// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Graph3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Graph3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Graph3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Graph3DVisualizer.SupportComponents
{
    /// <summary>
    /// Interface with notification of object destruction.
    /// </summary>
    public interface IDestructible
    {
        event Action<UnityEngine.Object> Destroyed;
    }

    /// <summary>
    /// Interface that controls the changing of <see cref="Transform"/> object. It can report changes coordinates of the object.
    /// </summary>
    public interface IMoveable
    {
        event Action<Vector3, UnityEngine.Object> ObjectMoved;

        Vector3 GlobalCoordinates { get; set; }
        Vector3 LocalCoordinates { get; set; }
        float MovingSpeed { get; set; }
        float RotationSpeed { get; set; }

        IEnumerator MoveAlongTrajectory (IReadOnlyList<Vector3> trajectory);

        void Rotate (Vector2 rotationChange, float deltaTime);

        void Translate (Vector3 moveVector, float deltaTime);
    }

    /// <summary>
    /// Interface that allows you to select and highlight objects. It can report changes selected/highlighted state of the object.
    /// </summary>
    public interface ISelectable
    {
        event Action<UnityEngine.Object, bool> HighlightedChanged;

        event Action<UnityEngine.Object, bool> SelectedChanged;

        bool IsHighlighted { get; set; }
        bool IsSelected { get; set; }
        Color SelectFrameColor { get; set; }
    }

    /// <summary>
    /// Interface that controls the visibility of an object. It can report changes visibility of the object.
    /// </summary>
    public interface IVisibile
    {
        event Action<bool, UnityEngine.Object> VisibleChanged;

        bool Visibility { get; set; }
    }
}