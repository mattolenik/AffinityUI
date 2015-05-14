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
        GUILayoutOption[] layoutOptions;
        Func<GUIStyle> styleGetter = () => GUIStyle.none;
        BindableProperty<TSelf, bool> visible;

		protected TypedControl()
			: base()
		{
            visible = new BindableProperty<TSelf, bool>(this as TSelf, true);
		}

        public new TSelf ID(string id)
        {
            base.ID(id);
            return this as TSelf;
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
            return layoutOptions;
        }

		public TSelf Visible(bool visible)
		{
            this.visible.Value = visible;
			return this as TSelf;
		}

        public BindableProperty<TSelf, bool> Visible()
        {
            return visible;
        }

        public GUIStyle Style()
        {
            return styleGetter();
        }

        public TSelf Style(Func<GUIStyle> styleGetter)
        {
            this.styleGetter = styleGetter;
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