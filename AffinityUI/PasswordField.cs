using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A masked password field.
	/// </summary>
	public class PasswordField : ContentControl<PasswordField>
	{
		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Password"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Password property.</value>
		public BindableProperty<PasswordField, String> PasswordProperty { get; private set; }

		/// <summary>
		/// Gets or sets the mask character, used only by GUI and GUILayout contexts.
		/// </summary>
		/// <value>The mask.</value>
		public char Mask { get; set; }

		/// <summary>
		/// Gets or sets the maximum password length, used only by GUI and GUILayout contexts.
		/// </summary>
		/// <value>The length of the max.</value>
		public int MaxLength { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public String Password
		{
			get { return PasswordProperty.Value; }
			set { PasswordProperty.Value = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordField"/> class.
		/// </summary>
		public PasswordField()
			: base()
		{
			Self = this;
			Mask = '*';
			MaxLength = Int32.MaxValue;
			Style = GUI.skin.textField;
			PasswordProperty = new BindableProperty<PasswordField, String>(this);
			Password = "";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordField"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public PasswordField(String label)
			: this()
		{
			Label = label;
		}

		/// <summary>
		/// Adds an event handler for password change.
		/// </summary>
		/// <param name="handler">The handler.</param>
		/// <returns>this instance</returns>
		public PasswordField OnPasswordChanged(PropertyChangedEventHandler<PasswordField, String> handler)
		{
			PasswordProperty.PropertyChanged += handler;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
			Password = GUI.PasswordField(Position, Password, Mask, MaxLength, Style);
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
			Password = GUILayout.PasswordField(Password, Mask, MaxLength, Style, LayoutOptions);
		}
	}
}