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
		/// <summary>
		/// Gets or sets the maximum length of the text. Defaults to <see cref="Int32.MaxValue"/>.
		/// </summary>
		/// <value>The maximum text length.</value>
		public int MaxLength { get; set; }

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
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TextArea"/> class.
		/// </summary>
		public TextArea()
			: base()
		{
			Self = this;
            Style(GUI.skin.textArea);
			_text = new BindableProperty<TextArea, String>(this);
			MaxLength = 50;
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
            Text(GUI.TextArea(Position(), Text(), MaxLength, Style()));
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            Text(GUILayout.TextArea(Text(), MaxLength, Style(), LayoutOptions()));
		}
	}
}
