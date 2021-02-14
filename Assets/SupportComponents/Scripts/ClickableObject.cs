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

using UnityEngine;

namespace Grpah3DVisualizer.SupportComponents
{
    public abstract class ClickableObject : MonoBehaviour
    {
        protected Transform _transform;
        protected GameObject _gameObject;

        protected abstract void ClickAction (GameObject gameObject);

        public event Action<GameObject> Clicked;
        public void Click (GameObject gameObject)
        {
            ClickAction(gameObject);
            Clicked?.Invoke(gameObject);
        }
        public abstract void SetDisabled ();
    }
}
