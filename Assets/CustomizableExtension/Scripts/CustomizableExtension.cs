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
using System.Collections.Generic;
using System.Runtime.Serialization;

using Yuzu;

namespace Graph3DVisualizer.Customizable
{
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
    public abstract class AbstractCustomizableParameter { };

    /// <summary>
    /// An attribute that specifies which type of parameters to use for <see cref="CustomizableExtension.CallDownloadParams(object)"/>, <see cref="CustomizableExtension.CallSetUpParams(object, object)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CustomizableGrandTypeAttribute : Attribute
    {
        private Type _type;
        public Type Type { get => _type; set => _type = value.IsSubclassOf(typeof(AbstractCustomizableParameter)) ? value : throw new WrongTypeInCustomizableParameterException(); }
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