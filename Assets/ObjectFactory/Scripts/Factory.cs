using System;
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;

using UnityEngine;

namespace Graph3DVisualizer.ObjectFactory
{
    public sealed class Factory
    {
        private readonly Dictionary<Type, ICreater> _creatersList = new();

        public void AddCreater<T> (ICreater creater) where T : MonoBehaviour, ICustomizable =>
            _creatersList.Add(typeof(T), creater);

        public GameObject Create<TObject, TParameter> (TParameter parameter)
            where TObject : MonoBehaviour, ICustomizable<TParameter>
            where TParameter : AbstractCustomizableParameter =>
            _creatersList[typeof(TObject)].Create(parameter);
    }

    public interface ICreater
    {
        public GameObject Create (AbstractCustomizableParameter parameter);
    }
}