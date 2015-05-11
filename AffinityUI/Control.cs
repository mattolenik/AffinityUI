using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// The absolute base class for all controls, provides little functionality
    /// and mostly serves as a handle for referencing all other types of controls.
    /// </summary>
	public abstract class Control
	{
        protected internal virtual UIContext Context { get; set; }

        Rect _position;

        /// <summary>
        /// Gets or sets (protected internal) the owning parent of this control.
        /// </summary>
        /// <value>The parent control.</value>
		public Control Parent { get; protected internal set; }

        /// <summary>
        /// Sets the position of the control.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>this instance</returns>
        public Control Position(Rect position)
        {
            _position = position;
            return this;
        }

        public Rect Position()
        {
            return _position;
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
            GUI.skin = SkinValue;

            switch (Context.Layout)
            {
                case LayoutTarget.GUI:
                    Layout_GUI();
                    break;
                case LayoutTarget.GUILayout:
                    Layout_GUILayout();
                    _position = GUILayoutUtility.GetLastRect();
                    break;
                default:
                    throw new InvalidOperationException("Layout must be either GUI or GUILayout");
            }
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected virtual void Layout_GUI()
        {
            throw new NotSupportedException("Layout_GUI not implemented in " + GetType().Name);
        }

		/// <summary>
		/// Called when layout is done using GUILayout.
		/// </summary>
		protected virtual void Layout_GUILayout()
        {
            throw new NotSupportedException("Layout_GUILayout not implemented in " + GetType().Name);
        }

        /// <summary>
        /// Globally registers this control with the specified ID.
        /// </summary>
        /// <param name="id">an ID used to refer to this control.</param>
        public Control ID(string id)
        {
            UI.RegisterID(this, id);
            return this;
        }

        public string ID()
        {
            return UI.IdOf(this);
        }

        /// <summary>
        /// Gets or sets the skin to use when rendering.
        /// </summary>
        /// <value>The skin value.</value>
        protected internal virtual GUISkin SkinValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AffinityUI.Control"/> has 
        /// a skin independant of its parent. Setting the skin of a control that owns this one
        /// will have no effect if this value is true. For example, if this skin is inside a
        /// Window or a panel, setting the skin on the Window or panel will have no effect on
        /// this control when IndependantSkin is set to true.
        /// </summary>
        /// <value><c>true</c> if the control has an independant skin; otherwise, <c>false</c>.</value>
        protected internal virtual bool IndependantSkin { get; set; }
	}
}