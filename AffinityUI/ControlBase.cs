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
        Rect _position;

        GUILayoutOption[] _layoutOptions;

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
		}

		/// <summary>
		/// Sets the position of the control.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>this instance</returns>
		public TSelf Position(Rect position)
		{
            _position = position;
			return Self;
		}

        public Rect Position()
        {
            return _position;
        }

		/// <summary>
		/// Sets the layout options to use.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>this instance</returns>
		public TSelf LayoutOptions(params GUILayoutOption[] options)
		{
            LayoutOptions(options);
			return Self;
		}

        public GUILayoutOption[] LayoutOptions()
        {
            return _layoutOptions;
        }

		/// <summary>
		/// Sets the <see cref="Control.Visible"/> property.
		/// </summary>
		/// <param name="visible">if set to <c>true</c> [visible].</param>
		/// <returns>this instance</returns>
		public TSelf SetVisible(bool visible)
		{
			Visible.Value = visible;
			return Self;
		}
	}
}