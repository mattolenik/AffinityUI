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

        GUIStyle _style = GUIStyle.none;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlBase&lt;TSelf&gt;"/> class.
		/// </summary>
		protected ControlBase()
			: base()
		{
            _visible = new BindableProperty<TSelf, bool>(this as TSelf, true);
		}

        public new TSelf ID(string id)
        {
            UI.RegisterID(this, id);
            return this as TSelf;
        }

		/// <summary>
		/// Sets the position of the control.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>this instance</returns>
		public TSelf Position(Rect position)
		{
            _position = position;
            return this as TSelf;
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
			return this as TSelf;
		}

        public GUILayoutOption[] LayoutOptions()
        {
            return _layoutOptions;
        }

        BindableProperty<TSelf, bool> _visible;

		public TSelf Visible(bool visible)
		{
            _visible.Value = visible;
			return this as TSelf;
		}

        public BindableProperty<TSelf, bool> Visible()
        {
            return _visible;
        }

        public GUIStyle Style()
        {
            return _style;
        }

        public TSelf Style(GUIStyle style)
        {
            _style = style;
            return this as TSelf;
        }
	}
}