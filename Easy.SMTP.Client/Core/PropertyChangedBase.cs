using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Easy.SMTP.Core
{
    /// <summary>
    /// Base class implementing <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies clients that all properties may have changed.
        /// </summary>
        /// <remarks>
        /// This method raises the <see cref="PropertyChanged"/> event with <see cref="String.Empty"/> as the property name.
        /// </remarks>
        protected void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
            NotifyOfPropertyChange(String.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected virtual void NotifyOfPropertyChange(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="property">The changed property.</param>
        /// <typeparam name="TProperty">The type of the changed property.</typeparam>
        protected void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange(ExpressionHelper.GetMemberName(property));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            this.NotifyOfPropertyChange(propertyName);
            return true;
        }
    }
}
