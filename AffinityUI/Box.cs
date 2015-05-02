using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A box with GUIContent.
	/// </summary>
	public class Box : ContentControl<Box>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Box"/> class.
		/// </summary>
		public Box()
			: base()
		{
			Self = this;
			Style = GUI.skin.box;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Box"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public Box(String label)
			: this()
		{
			Label = label;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
			GUI.Box(Position, Content, Style);
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
			GUILayout.Box(Content, Style, LayoutOptions);
		}
	}
}
