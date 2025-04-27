using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace MvvmBuilder
{
    public class PropertyObserver
    {
        private readonly Action _action;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserver"/> class.
        /// </summary>
        /// <param name="propertyExpression">The expression representing the property to observe.</param>
        /// <param name="action">The action to be executed when the property changes.</param>
        public PropertyObserver(Expression propertyExpression, Action action)
        {
            _action = action;
            Subscribe(propertyExpression);
        }

        /// <summary>
        /// Subscribes to changes in a property specified by a lambda expression.
        /// </summary>
        /// <param name="propertyExpression">The lambda expression specifying the property to subscribe to.</param>
        /// <exception cref="NotSupportedException">Thrown if the expression is not a constant member expression.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property owner object does not implement INotifyPropertyChanged.</exception>
        private void Subscribe(Expression propertyExpression)
        {
            var propNameStack = new Stack<PropertyInfo>();
            while (propertyExpression is MemberExpression memberExpression)
            {
                propertyExpression = memberExpression.Expression;
                propNameStack.Push((memberExpression.Member as PropertyInfo)!);
            }
            
            if (!(propertyExpression is ConstantExpression constantExpression))
                throw new NotSupportedException("Only constants are supported");

            var propObserverNodeRoot = new PropertyObserverNode(propNameStack.Pop(), _action);
            foreach (var propName in propNameStack)
            {
                var currentNode = new PropertyObserverNode(propName, _action);
                propObserverNodeRoot = currentNode;
            }

            var propOwnerObject = constantExpression.Value;
            
            if (!(propOwnerObject is INotifyPropertyChanged notifyObject))
                throw new InvalidOperationException("Only INotifyPropertyChanged objects are supported");
            
            propObserverNodeRoot.SubscribeListenerFor(notifyObject);
        }

        /// <summary>
        /// Creates a new instance of the PropertyObserver class that observes the specified property expression and invokes the provided action when the property changes.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression that represents the property to observe.</param>
        /// <param name="action">An action to be invoked when the property changes.</param>
        /// <returns>A new instance of the PropertyObserver class.</returns>
        public static PropertyObserver Observes<T>(Expression<Func<T>> propertyExpression, Action action) =>
            new PropertyObserver(propertyExpression.Body, action);
    }
}