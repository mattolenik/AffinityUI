using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A checkbox toggle.
	/// </summary>
	public class Toggle : ContentControl<Toggle>
    {
        BindableProperty<Toggle, bool> _isChecked;

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle"/> class.
		/// </summary>
		public Toggle()
			: base()
		{
			Self = this;
            Style(GUI.skin.toggle);
            _isChecked = new BindableProperty<Toggle, bool>(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public Toggle(String label)
			: this()
		{
            Label(label);
		}

		/// <summary>
		/// Called when the control is toggled on or off.
		/// </summary>
		/// <param name="handler">The event handler method.</param>
		/// <returns>this instance</returns>
		public Toggle OnToggled(PropertyChangedEventHandler<Toggle, bool> handler)
		{
			_isChecked.PropertyChanged += handler;
			return this;
		}

        public BindableProperty<Toggle, bool> IsChecked()
        {
            return _isChecked;
        }

		/// <summary>
		/// Sets the initial toggle value.
		/// </summary>
		/// <param name="value">The initial value.</param>
		/// <returns>this instance</returns>
		public Toggle IsChecked(bool value)
		{
			_isChecked.Value = value;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            _isChecked.Value = GUILayout.Toggle(_isChecked, Content(), Style(), LayoutOptions());
		}
	}
}