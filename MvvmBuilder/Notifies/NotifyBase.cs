using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MvvmBuilder.Notifies
{
    /// <summary>
    /// Starter notify builder object.
    /// </summary>
    public abstract partial class NotifyBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Saved all properties that tracked by notify property.
        /// </summary>
        private readonly Dictionary<string, object?> _trackerProperties;
        private readonly Type _mainClass;
        
        /// <summary>
        /// Saved all subscriptions.
        /// </summary>
        private static readonly Dictionary<object, List<ChangeObserver>> _subscriptions = new Dictionary<object, List<ChangeObserver>>();

        protected NotifyBase()
        {
            _trackerProperties = new Dictionary<string, object?>();
            _mainClass = GetType();

            var changeObservers = new List<ChangeObserver>();
            _subscriptions.TryAdd(this, changeObservers);

            foreach (var property in _mainClass.GetProperties().Where(s => s.CanWrite))
                _trackerProperties.Add(property.Name, null);
            
        }
    
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Called <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="property">Name of property.</param>
        protected void OnRaiseChanged(object? value = null, [CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (!_subscriptions.TryGetValue(this, out var changeObservers)) return;
            foreach (var changeObserver in changeObservers)
                changeObserver.Notify(value);
        }

        /// <summary>
        /// Subscribe on changes.
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="className">Class name of property</param>
        /// <param name="action">Action that will be called when property changed</param>
        public static void SubscribeOnChanges(string propertyName, string className, Action<object?> action)
        {
            var obj = _subscriptions.Keys.FirstOrDefault(s => s.GetType().Name == className);
            if (obj == null) return;
            SubscribeOnChanges(propertyName, obj, action);
        }

        /// <summary>
        /// Subscribe on changes.
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="obj">Class of property</param>
        /// <param name="action">Action that will be called when property changed</param>
        public static void SubscribeOnChanges(string propertyName, object obj, Action<object?> action)
        {
            if (!_subscriptions.TryGetValue(obj, out var changeObservers)) return;
            var changeObserver = changeObservers.FirstOrDefault(s => s.PropertyName == propertyName);
            if (changeObserver == null)
            {
                changeObserver = new ChangeObserver(propertyName);
                _subscriptions[obj].Add(changeObserver);
            }
            changeObserver.Attach(action);
        }
        
        /// <summary>
        /// Unsubscribe on changes. 
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="className">Class name of property</param>
        /// <param name="action">Action that will be called when property changed</param>
        public static void UnsubscribeOnChanges(string propertyName, string className, Action<object?> action)
        {
            var obj = _subscriptions.Keys.FirstOrDefault(s => s.GetType().Name == className);
            if (obj == null) return;
            UnsubscribeOnChanges(propertyName, obj, action);
        }
        
        /// <summary>
        /// Unsubscribe on changes. 
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="obj">Class of property</param>
        /// <param name="action">Action that will be called when property changed</param>
        public static void UnsubscribeOnChanges(string propertyName, object obj, Action<object?> action)
        {
            if (!_subscriptions.TryGetValue(obj, out var changeObservers)) return;
            var changeObserver = changeObservers.FirstOrDefault(s => s.PropertyName == propertyName);
            changeObserver?.Detach(action);
        }

        /// <summary>
        /// Clear all subscriptions. 
        /// </summary>
        /// <param name="propertyName">Name of property.</param>
        /// <param name="className">Class name of property.</param>
        public static void ClearSubscriptions(string propertyName, string className)
        {
            var obj = _subscriptions.Keys.FirstOrDefault(s => s.GetType().Name == className);
            if (obj == null) return;
            ClearSubscriptions(propertyName, obj);
        }

        /// <summary>
        /// Clear all subscriptions. 
        /// </summary>
        /// <param name="propertyName">Name of property.</param>
        /// <param name="obj">Class of property</param>
        public static void ClearSubscriptions(string propertyName, object obj)
        {
            if (!_subscriptions.TryGetValue(obj, out var changeObservers)) return;
            var changeObserver = changeObservers.FirstOrDefault(s => s.PropertyName == propertyName);
            changeObserver?.Clear();
        }
    }
}