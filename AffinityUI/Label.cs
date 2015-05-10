using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A text label.
	/// </summary>
	public class Label : ContentControl<Label>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Label"/> class.
		/// </summary>
		public Label()
			: base()
		{
            Style(() => GUI.skin.label);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Label"/>.
		/// </summary>
		/// <param name="text">The label text.</param>
		public Label(string text)
			: this()
		{
            Content(new GUIContent(text));
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
            GUI.Label(Position(), Content(), Style());
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
            GUILayout.Label(Content(), Style(), LayoutOptions());
		}
	}
}