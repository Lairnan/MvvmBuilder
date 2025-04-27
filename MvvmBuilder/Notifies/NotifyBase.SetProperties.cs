using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MvvmBuilder.Notifies
{
    public abstract partial class NotifyBase
    {
        /// <summary>
        /// Save property in reference field and call <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="field">Reference field name.</param>
        /// <param name="value">Value changed field.</param>
        /// <param name="property">Property name field.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string property = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnRaiseChanged(value, property);
        }
    
        /// <summary>
        /// Save property in dictionary tracker and call <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="value">Value changed field.</param>
        /// <param name="property">Property name field.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        protected void SetProperty<T>(T value, [CallerMemberName] string property = "")
        {
            if (!_trackerProperties.TryAdd(property, value)) _trackerProperties[property] = value;
            OnRaiseChanged(value, property);
        }

        /// <summary>
        /// Save property in dictionary tracker and call <see cref="PropertyChanged"/> event.
        /// Execute action after save property.
        /// </summary>
        /// <param name="value">Value changed field.</param>
        /// <param name="action">Action with parameter of type property.</param>
        /// <param name="property">Property name field.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        protected void SetProperty<T>(T value, Action<T>? action, [CallerMemberName] string property = "")
        {
            SetProperty(value, property);
            action?.Invoke(value);
        }

        /// <summary>
        /// Set start value on property if is first start application.
        /// Save property in dictionary tracker and call <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="value">Value changed field.</param>
        /// <param name="startValue">Start value property by first start program.</param>
        /// <param name="property">Property name field.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        protected void SetProperty<T>(T value, T startValue, [CallerMemberName] string property = "")
        {
            if (!_trackerProperties.ContainsKey(property)) value = startValue;
            SetProperty(value, property);
        }
    
        /// <summary>
        /// Set start value on property if is first start application.
        /// Save property in dictionary tracker and call <see cref="PropertyChanged"/> event.
        /// Execute action after save property.
        /// </summary>
        /// <param name="value">Value changed field.</param>
        /// <param name="action">Action with parameter of type property.</param>
        /// <param name="startValue">Start value property by first start program.</param>
        /// <param name="property">Property name field.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        protected void SetProperty<T>(T value, Action<T>? action, T startValue, [CallerMemberName] string property = "")
        {
            SetProperty(value, startValue, property);
            action?.Invoke(value);
        }
    }
}