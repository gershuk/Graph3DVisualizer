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

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Base class for all Graph3D objects.
    /// </summary>
    public abstract class AbstractGraphObject : MonoBehaviour
    {
        //attribute for debug only
        [SerializeField]
        private string _id;

        public string Id { get => _id; protected set => _id = value ?? Guid.NewGuid().ToString(); }
    }

    /// <summary>
    /// Class that describes common <see cref="AbstractGraphObject"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public abstract class AbstractGraphObjectParameters : AbstractCustomizableParameter
    {
        public string Id { get; set; }

        public AbstractGraphObjectParameters (string id) => Id = id;
    }
}