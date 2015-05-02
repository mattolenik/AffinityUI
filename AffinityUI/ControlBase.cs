using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// Generic base for control authoring, providing functionality common to all controls.
	/// </summary>
	/// <typeparam name="TSelf">The type of the implementing subclass.</typeparam>
	public abstract class ControlBase<TSelf> : Control where TSelf : Control
	{
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Rect Position { get; set; }

		/// <summary>
		/// Gets or sets the layout options.
		/// </summary>
		/// <value>The layout options.</value>
		public GUILayoutOption[] LayoutOptions { get; set; }

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Control.Visible"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Visible property.</value>
		public BindableProperty<ControlBase<TSelf>, bool> VisibleProperty { get; private set; }

		/// <summary>
		/// Gets or sets the object to return in fluent methods. Should be set to
		/// <c>this</c> by implementing subclass.
		/// </summary>
		/// <value>a reference to the current control</value>
		protected TSelf Self { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlBase&lt;TSelf&gt;"/> class.
		/// </summary>
		protected ControlBase()
			: base()
		{
			VisibleProperty = new BindableProperty<ControlBase<TSelf>, bool>(this, true);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Control"/> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public override bool Visible
		{
			get { return VisibleProperty.Value; }
			set { VisibleProperty.Value = value; }
		}

		/// <summary>
		/// Sets the position of the control.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>this instance</returns>
		public TSelf SetPosition(Rect position)
		{
			Position = position;
			return Self;
		}

		/// <summary>
		/// Sets the layout options to use.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>this instance</returns>
		public TSelf SetLayoutOptions(params GUILayoutOption[] options)
		{
			LayoutOptions = options;
			return Self;
		}

		/// <summary>
		/// Sets the <see cref="Control.Visible"/> property.
		/// </summary>
		/// <param name="visible">if set to <c>true</c> [visible].</param>
		/// <returns>this instance</returns>
		public TSelf SetVisible(bool visible)
		{
			Visible = visible;
			return Self;
		}
	}
}