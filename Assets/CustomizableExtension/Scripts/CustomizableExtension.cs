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
using System.Runtime.Serialization;

using Yuzu;

namespace Graph3DVisualizer.Customizable
{
    public static class CacheForCustomizableObjects
    {
        private static readonly Dictionary<Type, SortedDictionary<AbstractCustomizableParameter, object>> _cache = new Dictionary<Type, SortedDictionary<AbstractCustomizableParameter, object>>();

        public static void Add (AbstractCustomizableParameter parameter, object customizableObject)
        {
            if (!_cache.TryGetValue(parameter.GetType(), out var dictionary))
            {
                dictionary = new SortedDictionary<AbstractCustomizableParameter, object>();
                _cache.Add(parameter.GetType(), dictionary);
            }
            dictionary.Add(parameter, customizableObject);
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

        public static bool ContainsKey (AbstractCustomizableParameter parameter) => _cache[parameter.GetType()].ContainsKey(parameter);

        public static bool ContainsValue<T> (object customizableObject) => _cache.TryGetValue(typeof(T), out var dictionary) ? dictionary.ContainsValue(customizableObject) : false;

        public static void Remove (AbstractCustomizableParameter parameter, bool useDispose = default)
        {
            if (useDispose && _cache[parameter.GetType()][parameter] is IDisposable disposable)
                disposable.Dispose();

            _cache[parameter.GetType()].Remove(parameter);
        }

        public static bool TryGetParameter<T> (object customizableObject, out T? parameter) where T : AbstractCustomizableParameter
        {
            parameter = null;
            foreach (var pair in _cache[typeof(T)])
            {
                if (pair.Value.Equals(customizableObject) && pair.Key.GetType() == typeof(T))
                {
                    parameter = (T) pair.Key;
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetValue (AbstractCustomizableParameter parameter, out object? customizableObject)
        {
            customizableObject = null;
            return _cache.TryGetValue(parameter.GetType(), out var dictionary) && dictionary.TryGetValue(parameter, out customizableObject);
        }
    }

    /// <summary>
    ///  A class containing functions for dynamically calling methods <see cref="ICustomizable{TParams}.DownloadParams"/>, <see cref="ICustomizable{TParams}.SetupParams(TParams)"/>.
    /// </summary>
    public static class CustomizableExtension
    {
        public static AbstractCustomizableParameter CallDownloadParams (object customizable)
        {
            const string methodName = nameof(ICustomizable<AbstractCustomizableParameter>.DownloadParams);
            var attribute = (CustomizableGrandTypeAttribute) Attribute.GetCustomAttribute(customizable.GetType(), typeof(CustomizableGrandTypeAttribute), true);
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>) && interfaceType.GetGenericArguments()[0] == attribute.Type)
                {
                    var method = interfaceType.GetMethod(methodName);
                    return (AbstractCustomizableParameter) method.Invoke(customizable, null);
                }
            }

            throw new MissingMethodException(customizable.GetType().Name, methodName);
        }

        public static List<T> CallDownloadParams<T> (object customizable) where T : AbstractCustomizableParameter
        {
            var parameters = new List<T>();
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>)
                    && (interfaceType.GetGenericArguments()[0].IsSubclassOf(typeof(T)) || interfaceType.GetGenericArguments()[0] == typeof(T)))
                {
                    parameters.Add((T) interfaceType.GetMethod(nameof(ICustomizable<AbstractCustomizableParameter>.DownloadParams)).Invoke(customizable, null));
                }
            }
            return parameters;
        }

        public static void CallSetUpParams (object customizable, object parameter)
        {
            const string methodName = nameof(ICustomizable<AbstractCustomizableParameter>.SetupParams);
            var attribute = (CustomizableGrandTypeAttribute) Attribute.GetCustomAttribute(customizable.GetType(), typeof(CustomizableGrandTypeAttribute), true);
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>) && interfaceType.GetGenericArguments()[0] == attribute.Type)
                {
                    var method = interfaceType.GetMethod(methodName);
                    method.Invoke(customizable, new[] { parameter });
                    return;
                }
            }

            throw new MissingMethodException($"Customizable methods with parameter type {attribute.Type} not found");
        }

        public static void CallSetUpParams (object customizable, object[] parameters)
        {
            foreach (var param in parameters)
            {
                var isFinded = false;

                foreach (var interfaceType in customizable.GetType().GetInterfaces())
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>) && interfaceType.GetGenericArguments()[0] == param.GetType())
                    {
                        interfaceType.GetMethod(nameof(ICustomizable<AbstractCustomizableParameter>.SetupParams), interfaceType.GetGenericArguments()).Invoke(customizable, new[] { param });
                        isFinded = true;
                    }
                }

                if (!isFinded)
                    throw new MissingMethodException($"Customizable methods with parameter type {param.GetType()} not found");
            }
        }
    }

    /// <summary>
    /// Abstract class for setup and download object parameters.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public abstract class AbstractCustomizableParameter : IComparable<AbstractCustomizableParameter>
    {
        private string _id;

        public string Id { get => _id; protected set => _id = value ?? Guid.NewGuid().ToString(); }

        protected AbstractCustomizableParameter (string? id) => Id = id;

        public int CompareTo (AbstractCustomizableParameter other) => Id.CompareTo(other.Id);
    }

    /// <summary>
    /// An attribute that specifies which type of parameters to use for <see cref="CustomizableExtension.CallDownloadParams(object)"/>, <see cref="CustomizableExtension.CallSetUpParams(object, object)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CustomizableGrandTypeAttribute : Attribute
    {
        private Type _type;

        public Type Type { get => _type; set => _type = value.IsSubclassOf(typeof(AbstractCustomizableParameter)) ? value : throw new WrongTypeInCustomizableParameterException(); }

        public CustomizableGrandTypeAttribute (Type type) => Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public class WrongTypeInCustomizableParameterException : Exception
    {
        protected WrongTypeInCustomizableParameterException (SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WrongTypeInCustomizableParameterException ()
        {
        }

        public WrongTypeInCustomizableParameterException (string message) : base(message)
        {
        }

        public WrongTypeInCustomizableParameterException (string message, Exception innerException) : base(message, innerException)
        {
        }

        public WrongTypeInCustomizableParameterException (Type expectedType, Type receivedType) : base($"The type inherited from {expectedType.Name} was expected, and {receivedType.Name} was obtained.")
        {
        }
    }

    /// <summary>
    /// Interface for setup and download object parameters.
    /// </summary>
    public interface ICustomizable<TParams> where TParams : AbstractCustomizableParameter
    {
        TParams DownloadParams ();

        void SetupParams (TParams parameters);
    }
}