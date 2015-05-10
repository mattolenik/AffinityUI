using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// An inline text field.
	/// </summary>
	public class TextField : ContentControl<TextField>
	{
		/// <summary>
		/// Gets or sets the maximum length of the text. Defaults to <see cref="Int32.MaxValue"/>.
		/// </summary>
		/// <value>The maximum text length.</value>
		public int MaxLength { get; set; }

        BindableProperty<TextField, string> _text;

        public BindableProperty<TextField, string> Text()
        {
            return _text;
        }

        public TextField Text(string text)
        {
            _text.Value = text;
            return this;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="TextField"/> class.
		/// </summary>
		public TextField()
			: base()
		{
            Style(() => GUI.skin.textField);
			_text = new BindableProperty<TextField, string>(this);
			MaxLength = Int32.MaxValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextField"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public TextField(string label) : this()
		{
            Label(label);
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
            Text(GUI.TextField(Position(), Text(), MaxLength, Style()));
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            Text(GUILayout.TextField(Text(), MaxLength, Style(), LayoutOptions()));
		}
	}
}
