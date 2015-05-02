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

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Text"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Text property.</value>
		public BindableProperty<TextArea, String> TextProperty { get; private set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public String Text
		{
			get { return TextProperty.Value; }
			set { TextProperty.Value = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextArea"/> class.
		/// </summary>
		public TextArea()
			: base()
		{
			Self = this;
			Style = GUI.skin.textArea;
			TextProperty = new BindableProperty<TextArea, String>(this);
			MaxLength = Int32.MaxValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextArea"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public TextArea(String label)
			: this()
		{
			Label = label;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
			Text = GUI.TextArea(Position, Text, MaxLength, Style);
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
			Text = GUILayout.TextArea(Text, MaxLength, Style, LayoutOptions);
		}
	}
}
