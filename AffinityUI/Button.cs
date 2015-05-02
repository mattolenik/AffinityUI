using System;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// Handler type for button click events.
	/// </summary>
	public delegate void ButtonClickHandler<TSource>(TSource source);

	/// <summary>
	/// A clickable button.
	/// </summary>
	public class Button : ContentControl<Button>
	{
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
        public event ButtonClickHandler<Button> Clicked = delegate{};

		/// <summary>
		/// Called when the button is clicked.
		/// </summary>
		/// <param name="handler">The event handler method.</param>
		/// <returns>this instance</returns>
		public Button OnClicked(ButtonClickHandler<Button> handler)
		{
			Clicked += handler;
			return this;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Button"/> class.
		/// </summary>
		public Button()
			: base()
		{
			Self = this;
			Style = GUI.skin.button;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Button"/> class.
		/// </summary>
		/// <param name="label">The label text.</param>
		public Button(String label)
			: this()
		{
			SetLabel(label);
		}

		private void Do_OnClicked(bool value)
		{
			if (value)
			{
                Clicked(this);
			}
		}

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected override void Layout_GUILayout()
		{
			Do_OnClicked(GUILayout.Button(Content, Style, LayoutOptions));
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
			Do_OnClicked(GUI.Button(Position, Content, Style));
		}
	}
}