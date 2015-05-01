using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A checkbox toggle.
	/// </summary>
	public class Toggle : ContentControl<Toggle>
	{
		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="IsChecked"/> property.
		/// </summary>
		/// <value>The BindableProperty for the IsChecked property.</value>
		public BindableProperty<Toggle, bool> IsCheckedProperty { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle"/> class.
		/// </summary>
		public Toggle()
			: base()
		{
			Self = this;
			Style = GUI.skin.toggle;
			IsCheckedProperty = new BindableProperty<Toggle, bool>(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public Toggle(String label)
			: this()
		{
			Label = label;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is checked.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
		/// </value>
		public bool IsChecked
		{
			get { return IsCheckedProperty.Value; }
			set { IsCheckedProperty.Value = value; }
		}

		/// <summary>
		/// Called when the control is toggled on or off.
		/// </summary>
		/// <param name="handler">The event handler method.</param>
		/// <returns>this instance</returns>
		public Toggle OnToggled(PropertyChangedEventHandler<Toggle, bool> handler)
		{
			IsCheckedProperty.PropertyChanged += handler;
			return this;
		}

		/// <summary>
		/// Sets the initial toggle value.
		/// </summary>
		/// <param name="value">The initial value.</param>
		/// <returns>this instance</returns>
		public Toggle SetIsChecked(bool value)
		{
			IsChecked = value;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
			IsChecked = GUILayout.Toggle(IsChecked, Content, Style, LayoutOptions);
		}
	}
}