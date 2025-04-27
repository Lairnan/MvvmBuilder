using System;
using System.ComponentModel;
using System.Reflection;

namespace MvvmBuilder
{
    public class PropertyObserverNode
    {
        private readonly Action _action;
        private INotifyPropertyChanged? _notifyObject;

        public PropertyInfo PropertyInfo { get; }
        public PropertyObserverNode? Next { get; set; }

        public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
        {
            this.PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            _action = action;
        }

        public void SubscribeListenerFor(INotifyPropertyChanged notifyObject)
        {
            _notifyObject = notifyObject;
            _notifyObject.PropertyChanged += OnPropertyChanged;
        }

        private void UnsubscribeListener()
        {
            if (_notifyObject != null)
                _notifyObject.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == this.PropertyInfo.Name || string.IsNullOrEmpty(e?.PropertyName))
            {
                _action?.Invoke();
            }
        }
    }
}