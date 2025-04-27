using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmBuilder
{
    public class ChangeObserver
    {
        public readonly string PropertyName;
        private readonly List<Action<object?>> _subscriptions;

        public ChangeObserver(string propertyName)
        {
            PropertyName = propertyName;
            _subscriptions = new List<Action<object?>>();
        }
        
        /// <summary>
        /// Attach action to property that will be called when property changed.
        /// </summary>
        /// <param name="action">Action that will be called</param>
        public void Attach(Action<object?> action)
        {
            _subscriptions.Add(action);
        }

        /// <summary>
        /// Detach action from property that will be called when property changed.
        /// </summary>
        /// <param name="action">Action that will be called</param>
        public void Detach(Action<object?> action)
        {
            var index = _subscriptions.FindIndex(s => s.Target == action.Target);
            if (index < 0) return;
            _subscriptions.RemoveAt(index);
        }
        
        /// <summary>
        /// Clear all subscriptions.
        /// </summary>
        public void Clear()
        {
            _subscriptions.Clear();
        }
        
        /// <summary>
        /// Notify all subscriptions.
        /// </summary>
        /// <param name="value">Value</param>
        public void Notify(object? value)
        {
            if (!_subscriptions.Any())
                return;
            
            foreach (var action in _subscriptions)
                action.Invoke(value);
        }
        
        /// <summary>
        /// Count subscriptions.
        /// </summary>
        public int Count => _subscriptions.Count;
    }
}