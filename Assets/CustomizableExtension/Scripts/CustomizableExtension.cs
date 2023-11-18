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

#nullable enable

using System;
using System.Collections.Generic;

using Yuzu;

namespace Graph3DVisualizer.Customizable
{
    public static class CacheForCustomizableObjects
    {
        private static readonly Dictionary<Type, SortedDictionary<Guid, object>> _cache = new();

        public static void Add (AbstractCustomizableParameter parameter, object customizableObject)
        {
            if (!_cache.TryGetValue(parameter.GetType(), out var dictionary))
            {
                dictionary = new();
                _cache.Add(parameter.GetType(), dictionary);
            }
            dictionary.Add(parameter.Id, customizableObject);
        }

        public static void Clear (Type type, bool useDispose = default)
        {
            if (useDispose)
            {
                foreach (var value in _cache[type].Values)
                {
                    if (value is IDisposable disposable)
                        disposable.Dispose();
                }
            }

            _cache[type].Clear();
        }

        public static void ClearAll (bool useDispose = default)
        {
            if (useDispose)
            {
                foreach (var type in _cache.Keys)
                {
                    Clear(type, true);
                }
            }

            _cache.Clear();
        }

        public static bool ContainsKey (AbstractCustomizableParameter parameter) =>
            _cache[parameter.GetType()].ContainsKey(parameter.Id);

        public static bool ContainsValue<T> (object customizableObject) =>
            _cache.TryGetValue(typeof(T), out var dictionary) && dictionary.ContainsValue(customizableObject);

        public static void Remove (AbstractCustomizableParameter parameter, bool useDispose = default)
        {
            if (useDispose && _cache[parameter.GetType()][parameter.Id] is IDisposable disposable)
                disposable.Dispose();

            _cache[parameter.GetType()].Remove(parameter.Id);
        }

        public static bool TryGetValue (AbstractCustomizableParameter parameter, out object? customizableObject)
        {
            customizableObject = null;
            return _cache.TryGetValue(parameter.GetType(), out var dictionary) &&
                dictionary.TryGetValue(parameter.Id, out customizableObject);
        }
    }

    /// <summary>
    ///  A class containing functions for dynamically calling methods <see cref="ICustomizable{TParams}.DownloadParams"/>,
    ///  <see cref="ICustomizable{TParams}.SetupParams(TParams)"/>.
    /// </summary>
    public static class CustomizableExtension
    {
        public static AbstractCustomizableParameter CallDownloadParams (object customizable, Dictionary<Guid, object> writeCache)
        {
            const string methodName = nameof(ICustomizable<AbstractCustomizableParameter>.DownloadParams);
            var attribute = (CustomizableGrandTypeAttribute) Attribute.GetCustomAttribute(customizable.GetType(),
                                                                                          typeof(CustomizableGrandTypeAttribute),
                                                                                          true);

            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>)
                    && interfaceType.GetGenericArguments()[0] == attribute.Type)
                {
                    return (AbstractCustomizableParameter) interfaceType.GetMethod(methodName).Invoke(customizable, new[] { writeCache });
                }
            }

            throw new MissingMethodException(customizable.GetType().Name, methodName);
        }

        public static void CallSetUpParams (object customizable, object parameter)
        {
            const string methodName = nameof(ICustomizable<AbstractCustomizableParameter>.SetupParams);
            var attribute = (CustomizableGrandTypeAttribute) Attribute.GetCustomAttribute(customizable.GetType(),
                                                                                          typeof(CustomizableGrandTypeAttribute),
                                                                                          true);

            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>)
                    && interfaceType.GetGenericArguments()[0] == attribute.Type)
                {
                    interfaceType.GetMethod(methodName).Invoke(customizable, new[] { parameter });
                    return;
                }
            }

            throw new MissingMethodException($"Customizable methods with parameter type {attribute.Type} not found");
        }
    }

    /// <summary>
    /// Abstract class for setup and download object parameters.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public abstract class AbstractCustomizableParameter : IComparable<AbstractCustomizableParameter>
    {
        public Guid Id { get; protected set; }

        protected AbstractCustomizableParameter (Guid? id) => Id = id ?? Guid.NewGuid();

        protected AbstractCustomizableParameter () => Id = Guid.NewGuid();

        public int CompareTo (AbstractCustomizableParameter other) => Id.CompareTo(other.Id);
    }

    /// <summary>
    /// An attribute that specifies which type of parameters to use for <see cref="CustomizableExtension.CallDownloadParams(object)"/>,
    /// <see cref="CustomizableExtension.CallSetUpParams(object, object)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CustomizableGrandTypeAttribute : Attribute
    {
        private Type _type;

        public Type Type
        {
            get => _type;
            set => _type = value.IsSubclassOf(typeof(AbstractCustomizableParameter))
                           ? value
                           : throw new WrongTypeInCustomizableParameterException();
        }

        public CustomizableGrandTypeAttribute (Type type) => Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public class WrongTypeInCustomizableParameterException : Exception
    {
        public WrongTypeInCustomizableParameterException ()
        {
        }

        public WrongTypeInCustomizableParameterException (Type expectedType, Type receivedType) :
            base($"The type inherited from {expectedType.Name} was expected, and {receivedType.Name} was obtained.")
        {
        }
    }

    public interface ICustomizable
    {
        AbstractCustomizableParameter DownloadParams (Dictionary<Guid, object> writeCache);

        void SetupParams (AbstractCustomizableParameter parameters);
    }

    /// <summary>
    /// Interface for setup and download object parameters.
    /// </summary>
    public interface ICustomizable<TParams> : ICustomizable
           where TParams : AbstractCustomizableParameter
    {
        TParams DownloadParams (Dictionary<Guid, object> writeCache);

        void SetupParams (TParams parameters);
    }
}