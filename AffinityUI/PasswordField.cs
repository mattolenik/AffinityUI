using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A masked password field.
	/// </summary>
	public class PasswordField : ContentControl<PasswordField>
	{
        BindableProperty<PasswordField, string> _password;

        char _mask;

        int _maxLength;

        public BindableProperty<PasswordField, string> Password()
        {
            return _password;
        }

        public PasswordField Password(string text)
        {
            _password.Value = text;
            return this;
        }

        public PasswordField Mask(char mask)
        {
            _mask = mask;
            return this;
        }

        public char Mask()
        {
            return _mask;
        }

        public PasswordField MaxLength(int maxLength)
        {
            _maxLength = maxLength;
            return this;
        }

        public int MaxLength()
        {
            return _maxLength;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordField"/> class.
		/// </summary>
		public PasswordField()
			: base()
		{
			_mask = '*';
			_maxLength = Int32.MaxValue;
            Style(() => GUI.skin.textField);
            _password = new BindableProperty<PasswordField, string>(this, string.Empty);
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
			_password.PropertyChanged += handler;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
            Password(GUI.PasswordField(Position(), Password(), _mask, _maxLength, Style()));
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            Password(GUILayout.PasswordField(Password(), _mask, _maxLength, Style(), LayoutOptions()));
		}
	}
}