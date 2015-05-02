using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A non-composite UI control.
	/// </summary>
	public abstract class Control
	{
		/// <summary>
		/// Backing field for <see cref="Visible"/> property.
		/// </summary>
		private bool _visible;

		/// <summary>
		/// Type of <see cref="GUI"/>.
		/// </summary>
		protected static readonly Type GUIType = typeof(GUI);

		/// <summary>
		/// Type of <see cref="GUILayout"/>.
		/// </summary>
		protected static readonly Type GUILayoutType = typeof(GUILayout);

		/// <summary>
		/// Gets or sets the type of the layout target.
		/// </summary>
		/// <value>The type of the layout target.</value>
		protected internal virtual Type TargetType { get; set; }

		/// <summary>
		/// Backing field for <see cref="Skin"/> property.
		/// </summary>
		protected GUISkin _skin;

		/// <summary>
		/// Gets or sets (protected internal) the parent control.
		/// </summary>
		/// <value>The parent control.</value>
		public Control Parent { get; protected internal set; }

		/// <summary>
		/// Gets or sets the Unity <see cref="GUISkin"/>.
		/// </summary>
		/// <remarks>
		/// Skins are applied recursively to all child controls.
		/// To exclude a control, set its skin to null or to another skin.
		/// </remarks>
		/// <value>The skin.</value>
		public virtual GUISkin Skin
		{
			get { return _skin; }
			set { _skin = value; }
		}

		/// <summary>
		/// Sets the Unity <see cref="GUISkin"/>.
		/// </summary>
		/// <param name="skin">The skin.</param>
		public TControl SetSkin<TControl>(GUISkin skin) where TControl : Control
		{
			Skin = skin;
			return this as TControl;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Control"/> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		/// <summary>
		/// Performs the necessary calls to UnityGUI to perform layout or updates.
		/// Should be called in the OnGUI methods.
		/// </summary>
		/// <remarks>
		/// Only override this method if you need complete control over GUI layout.
		/// This method automatically calls <see cref="Layout_GUI"/> or
		/// <see cref="Layout_GUILayout"/> depending on the GUI target.
		/// </remarks>
		public virtual void Layout()
		{
			LayoutSetSkin();

			if (TargetType == GUIType)
			{
				Layout_GUI();
			}
			else if (TargetType == GUILayoutType)
			{
				Layout_GUILayout();
			}
		}

		/// <summary>
		/// Sets the Unity GUI skin during layout.
		/// </summary>
		protected void LayoutSetSkin()
		{
			if (!System.Object.ReferenceEquals(GUI.skin, Skin))
			{
				GUI.skin = Skin;
			}
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected virtual void Layout_GUI() { }

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected virtual void Layout_GUILayout() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Control"/> class.
		/// </summary>
		protected Control()
		{
			_visible = true;
		}
	}
}