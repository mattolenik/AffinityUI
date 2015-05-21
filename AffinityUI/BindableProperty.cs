using System;
using System.Text.RegularExpressions;

namespace AffinityUI
{
    /// <summary>
    /// Change notification function signature.
    /// </summary>
	public delegate void PropertyChangedEventHandler<TSource, TValue>(TSource src, TValue old, TValue nw);

    /// <summary>
    /// Encapsulates databinding and update notification functionality for control properties.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner control.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class BindableProperty<TOwner, TProperty>
    {
        TProperty value;
        TOwner owner;
        Binder<TProperty> binder;

        /// <summary>
        /// Occurs when the property's value changes.
        /// </summary>
        public event PropertyChangedEventHandler<TOwner, TProperty> PropertyChanged;

        /// <summary>
        /// Gets the binding direction. Throws an <see cref="InvalidOperationException"/> if the property is not bound.
        /// </summary>
        /// <value>The direction.</value>
        public BindingDirection Direction
        {
            get
            {
                if (binder != null)
                {
                    return binder.Direction;
                }
                throw new InvalidOperationException("Property is not bound.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> class.
        /// </summary>
        /// <param name="owner">The owner control.</param>
        public BindableProperty(TOwner owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> class.
        /// </summary>
        /// <param name="owner">The owner control.</param>
        /// <param name="defaultValue">The default value.</param>
        public BindableProperty(TOwner owner, TProperty defaultValue)
            : this(owner)
        {
            value = defaultValue;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public TProperty Value
        {
            get
            {
                UpdateBinding();
                return value;
            }
            set
            {
                if (AreDifferent(this.value, value))
                {
                    DispatchPropertyChanged(this.value, value);
                    if (binder != null &&
                        (binder.Direction == BindingDirection.TwoWay || binder.Direction == BindingDirection.OneWayToSource))
                    {
                        binder.Value = value;
                    }
                }
                this.value = value;
            }
        }

        public static implicit operator TProperty(BindableProperty<TOwner, TProperty> instance)
        {
            return instance.Value;
        }

        /// <summary>
        /// Sets the value but ignores databinding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the owner control</returns>
        public TOwner SetIgnoreBinding(TProperty value)
        {
            this.value = value;
            return owner;
        }

        /// <summary>
        /// Binds this property using two way binding.
        /// </summary>
        /// <param name="getter">The getter.</param>
        /// <param name="setter">The setter.</param>
        /// <returns>the owner control</returns>
        public TOwner BindTwoWay(Func<TProperty> getter, Action<TProperty> setter)
        {
            binder = Binder<TProperty>.BindTwoWay(getter, setter);
            return owner;
        }

        /// <summary>
        /// Binds this property using one way binding.
        /// </summary>
        /// <param name="getter">The getter.</param>
        /// <returns>the owner control</returns>
        public TOwner BindOneWay(Func<TProperty> getter)
        {
            binder = Binder<TProperty>.BindOneWay(getter);
            return owner;
        }

        /// <summary>
        /// Binds this property using one way to source binding.
        /// </summary>
        /// <param name="setter">The setter.</param>
        /// <returns>the owner control</returns>
        public TOwner BindOneWayToSource(Action<TProperty> setter)
        {
            binder = Binder<TProperty>.BindOneWayToSource(setter);
            return owner;
        }

        /// <summary>
        /// Convenience fluent method for adding a handler to the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>the owner control</returns>
        public TOwner OnPropertyChanged(PropertyChangedEventHandler<TOwner, TProperty> handler)
        {
            PropertyChanged += handler;
            return owner;
        }

        /// <summary>
        /// Updates the binding. Call this when the binding should be updated without getting or setting the <see cref="Value"/> property.
        /// </summary>
        public void UpdateBinding()
        {
            if (binder != null &&
                (binder.Direction == BindingDirection.TwoWay || binder.Direction == BindingDirection.OneWay))
            {
                if (AreDifferent(value, binder.Value))
                {
                    DispatchPropertyChanged(value, binder.Value);
                    value = binder.Value;
                }
            }
        }

        /// <summary>
        /// Dispatches property changed events.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        void DispatchPropertyChanged(TProperty oldValue, TProperty newValue)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(owner, oldValue, newValue);
            }
        }

        /// <summary>
        /// Determines if the arguments are different.
        /// </summary>
        /// <param name="a">First object.</param>
        /// <param name="b">Second object.</param>
        /// <returns><c>true</c> if the arguments are different; otherwise, <c>false</c>.</returns>
        bool AreDifferent(object a, object b)
        {
            if (a == null && b == null)
            {
                return false;
            }
            if ((a == null && b != null) || (a != null && b == null))
            {
                return true;
            }
            return !a.Equals(b);
        }
    }
}