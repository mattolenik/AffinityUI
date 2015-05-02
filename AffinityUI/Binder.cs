using System;

namespace AffinityUI
{
	/// <summary>
	/// Data binding direction, indicates how data should be bound between source and destination.
	/// </summary>
	public enum BindingDirection
	{
		/// <summary>
		/// Binding is performed from the source to the control and the control to the source.
		/// </summary>
		TwoWay,
		/// <summary>
		/// Binding is performed from the source to the control.
		/// </summary>
		OneWay,
		/// <summary>
		/// Binding is performed from the control to the source.
		/// </summary>
		OneWayToSource
	}

	/// <summary>
	/// A data binding helper for fields and properties.
	/// </summary>
	/// <typeparam name="T">The type of the field or property being bound</typeparam>
	public class Binder<T>
	{
		private Action<T> setter;

		private Func<T> getter;

		/// <summary>
		/// Gets the <see cref="BindingDirection"/>.
		/// </summary>
		/// <value>The binding direction.</value>
		public BindingDirection Direction { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Binder&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="direction">The binding direction.</param>
		private Binder(BindingDirection direction)
		{
			Direction = direction;
		}

		/// <summary>
		/// Creates a binder with two way binding.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="setter">The setter.</param>
		/// <param name="getter">The getter.</param>
		/// <returns>a new Binder for type TValue</returns>
		public static Binder<TValue> BindTwoWay<TValue>(Func<TValue> getter, Action<TValue> setter)
		{
			var binder = new Binder<TValue>(BindingDirection.TwoWay);
			binder.setter = setter;
			binder.getter = getter;
			return binder;
		}

		/// <summary>
		/// Creates a binder with one way binding.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="getter">The getter.</param>
		/// <returns>a new Binder for type TValue</returns>
		public static Binder<TValue> BindOneWay<TValue>(Func<TValue> getter)
		{
			var binder = new Binder<TValue>(BindingDirection.OneWay);
			binder.getter = getter;
			return binder;
		}

		/// <summary>
		/// Creates a binder with one way to source binding.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="setter">The setter.</param>
		/// <returns>a new Binder for type TValue</returns>
		public static Binder<TValue> BindOneWayToSource<TValue>(Action<TValue> setter)
		{
			var binder = new Binder<TValue>(BindingDirection.OneWayToSource);
			binder.setter = setter;
			return binder;
		}

		/// <summary>
		/// Gets or sets the bound value.
		/// </summary>
		/// <value>The bound value.</value>
		public T Value
		{
			get
			{
				if (getter == null)
				{
					throw new InvalidOperationException("Cannot get the value because the binding mode is one way to source (write only).");
				}
				return getter();
			}
			set
			{
				if (setter == null)
				{
					throw new InvalidOperationException("Cannot set the value because the binding mode is one way (read only).");
				}
				setter(value);
			}
		}
	}
}