using System;
using System.Runtime.CompilerServices;

namespace MvvmBuilder.Notifies
{
    public abstract partial class NotifyBase
    {
        /// <summary>
        /// Get property from dictionary tracker by property name.
        /// </summary>
        /// <param name="property">Property name.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <returns>Value from dictionary or default value if is null</returns>
        protected T GetProperty<T>([CallerMemberName] string property = "")
        {
            if (!_trackerProperties.TryGetValue(property, out var value) || value == null) return default!;
            return (T)value;
        }
    
        /// <summary>
        /// Get property from dictionary tracker by property name.
        /// If value equals null, set value = defValue.
        /// </summary>
        /// <param name="defValue">Default value if own value is null.</param>
        /// <param name="property">Property name.</param>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <returns>Value from dictionary or <see cref="defValue"/> if is null</returns>
        protected T GetProperty<T>(Func<T> defValue, [CallerMemberName] string property = "")
        {
            if (!_trackerProperties.TryGetValue(property, out var value)) return defValue.Invoke();
            if (value != null || value != default) return (T)value;
            
            var def = defValue.Invoke();
            if (!_trackerProperties.TryAdd(property, def))
                _trackerProperties[property] = def;
            
            return def;
        }
    }
}