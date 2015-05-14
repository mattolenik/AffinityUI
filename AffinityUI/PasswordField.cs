using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A masked password field.
	/// </summary>
	public class PasswordField : ContentControl<PasswordField>
	{
        BindableProperty<PasswordField, string> password;
        char mask;
        int maxLength;

        public BindableProperty<PasswordField, string> Password()
        {
            return password;
        }

        public PasswordField Password(string text)
        {
            password.Value = text;
            return this;
        }

        public PasswordField Mask(char mask)
        {
            this.mask = mask;
            return this;
        }

        public char Mask()
        {
            return mask;
        }

        public PasswordField MaxLength(int maxLength)
        {
            this.maxLength = maxLength;
            return this;
        }

        public int MaxLength()
        {
            return maxLength;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordField"/> class.
		/// </summary>
		public PasswordField()
			: base()
		{
			mask = '*';
			maxLength = Int32.MaxValue;
            Style(() => GUI.skin.textField);
            password = new BindableProperty<PasswordField, string>(this, string.Empty);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordField"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public PasswordField(string label)
			: this()
		{
            Label(label);
		}

		/// <summary>
		/// Adds an event handler for password change.
		/// </summary>
		/// <param name="handler">The handler.</param>
		/// <returns>this instance</returns>
		public PasswordField OnPasswordChanged(PropertyChangedEventHandler<PasswordField, string> handler)
		{
			password.PropertyChanged += handler;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
            Password(GUI.PasswordField(Position(), Password(), mask, maxLength, Style()));
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            Password(GUILayout.PasswordField(Password(), mask, maxLength, Style(), LayoutOptions()));
		}
	}
}