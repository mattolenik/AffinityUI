using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A multi-line text area.
	/// </summary>
	public class TextArea : ContentControl<TextArea>
	{
        int _maxLength;

        BindableProperty<TextArea, String> _text;

        public BindableProperty<TextArea, String> Text()
        {
            return _text;
        }

        public TextArea Text(string text)
        {
            _text.Value = text;
            return this;
        }

        public int MaxLength()
        {
            return _maxLength;
        }

        public TextArea MaxLength(int length)
        {
            _maxLength = length;
            return this;
        }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TextArea"/> class.
		/// </summary>
		public TextArea()
			: base()
		{
            Style(GUI.skin.textArea);
            _text = new BindableProperty<TextArea, String>(this, String.Empty);
            _maxLength = int.MaxValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextArea"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public TextArea(String label)
			: this()
		{
            Label(label);
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
            Text(GUI.TextArea(Position(), Text(), _maxLength, Style()));
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            Text(GUILayout.TextArea(Text(), _maxLength, Style(), LayoutOptions()));
		}
	}
}
