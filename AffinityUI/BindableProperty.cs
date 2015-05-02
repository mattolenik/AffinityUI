using System;

namespace AffinityUI
{
	/// <summary>
	/// Change notification function signature.
	/// </summary>
	public delegate void PropertyChangedEventHandler<TSource, TValue>(TSource source, TValue oldValue, TValue newValue);

	/// <summary>
	/// Encapsulates databinding and update notification functionality for control properties.
	/// </summary>
	/// <typeparam name="TOwner">The type of the owner control.</typeparam>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	public class BindableProperty<TOwner, TProperty>
	{
		private TProperty _value;

		private TOwner _owner;

		private Binder<TProperty> _binder;

		/// <summary>
		/// Occurs when the property's value changes.
		/// </summary>
		public event PropertyChangedEventHandler<TOwner, TProperty> PropertyChanged;

		/// <summary>
		/// Gets a value indicating whether this property is databound.
		/// </summary>
		/// <value><c>true</c> if this property is bound; otherwise, <c>false</c>.</value>
		public bool IsBound
		{
			get { return _binder != null; }
		}

		/// <summary>
		/// Gets the binding direction. Throws an <see cref="InvalidOperationException"/> if the property is not bound.
		/// </summary>
		/// <value>The direction.</value>
		public BindingDirection Direction
		{
			get
			{
				if (_binder != null)
				{
					return _binder.Direction;
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
			this._owner = owner;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> class.
		/// </summary>
		/// <param name="owner">The owner control.</param>
		/// <param name="defaultValue">The default value.</param>
		public BindableProperty(TOwner owner, TProperty defaultValue) : this(owner)
		{
			_value = defaultValue;
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
				return _value;
			}
			set
			{
				if (AreDifferent(value, _value))
				{
					DispatchPropertyChanged(_value, value);
					if (_binder != null &&
						(_binder.Direction == BindingDirection.TwoWay || _binder.Direction == BindingDirection.OneWayToSource))
					{
						_binder.Value = value;
					}
				}
				_value = value;
			}
		}

        /// <summary>
        /// Sets the value but ignores databinding.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the owner control</returns>
        public TOwner SetIgnoreBinding(TProperty value)
        {
            _value = value;
            return _owner;
        }

		/// <summary>
		/// Binds this property using two way binding.
		/// </summary>
		/// <param name="getter">The getter.</param>
		/// <param name="setter">The setter.</param>
		/// <returns>the owner control</returns>
		public TOwner BindTwoWay(Func<TProperty> getter, Action<TProperty> setter)
		{
			_binder = Binder<TProperty>.BindTwoWay(getter, setter);
			return _owner;
		}

		/// <summary>
		/// Binds this property using one way binding.
		/// </summary>
		/// <param name="getter">The getter.</param>
		/// <returns>the owner control</returns>
		public TOwner BindOneWay(Func<TProperty> getter)
		{
			_binder = Binder<TProperty>.BindOneWay(getter);
			return _owner;
		}

		/// <summary>
		/// Binds this property using one way to source binding.
		/// </summary>
		/// <param name="setter">The setter.</param>
		/// <returns>the owner control</returns>
		public TOwner BindOneWayToSource(Action<TProperty> setter)
		{
			_binder = Binder<TProperty>.BindOneWayToSource(setter);
			return _owner;
		}

		/// <summary>
		/// Convenience fluent method for adding a handler to the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="handler">The handler.</param>
		/// <returns>the owner control</returns>
		public TOwner OnPropertyChanged(PropertyChangedEventHandler<TOwner, TProperty> handler)
		{
			PropertyChanged += handler;
			return _owner;
		}

		/// <summary>
		/// Updates the binding. Call this when the binding should be updated without getting or setting the <see cref="Value"/> property.
		/// </summary>
		public void UpdateBinding()
		{
			if (_binder != null &&
					(_binder.Direction == BindingDirection.TwoWay || _binder.Direction == BindingDirection.OneWay))
			{
				if (AreDifferent(_value, _binder.Value))
				{
					DispatchPropertyChanged(_value, _binder.Value);
					_value = _binder.Value;
				}
			}
		}

		/// <summary>
		/// Dispatches property changed events.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void DispatchPropertyChanged(TProperty oldValue, TProperty newValue)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(_owner, oldValue, newValue);
			}
		}

		/// <summary>
		/// Determines if the arguments are different.
		/// </summary>
		/// <param name="a">First object.</param>
		/// <param name="b">Second object.</param>
		/// <returns><c>true</c> if the arguments are different; otherwise, <c>false</c>.</returns>
		private bool AreDifferent(Object a, Object b)
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