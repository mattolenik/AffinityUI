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
	public abstract class TypedControl<TSelf> : Control where TSelf : Control
	{
        Rect _position;

        GUILayoutOption[] _layoutOptions;

        Func<GUIStyle> _styleFunc = () => GUIStyle.none;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlBase&lt;TSelf&gt;"/> class.
		/// </summary>
		protected TypedControl()
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
            return _styleFunc();
        }

        public TSelf Style(Func<GUIStyle> styleGetter)
        {
            _styleFunc = styleGetter;
            return this as TSelf;
        }

        public GUISkin Skin()
        {
            return SkinValue;
        }

        /// <summary>
        /// Sets the control's skin to the specified value. If independant is true, this control
        /// will have it's own skin that is independant of any control that owns it. For example,
        /// it if independant is true, setting the skin on a Window that contains this control
        /// will have no effect on this control.
        /// </summary>
        /// <param name="skin">the skin to set</param>
        /// <param name="independant">If set to <c>true</c> independant.</param>
        public virtual TSelf Skin(GUISkin skin, bool independant = false)
        {
            SkinValue = skin;
            IndependantSkin = independant;
            return this as TSelf;
        }
	}
}