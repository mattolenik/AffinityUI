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
		/// Gets or sets the type of the layout target.
		/// </summary>
		/// <value>The type of the layout target.</value>
		protected internal virtual UIContext Context { get; set; }

		/// <summary>
		/// Gets or sets (protected internal) the parent control.
		/// </summary>
		/// <value>The parent control.</value>
		public Control Parent { get; protected internal set; }

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
            switch (Context.Layout)
            {
                case LayoutTarget.GUI:
                    Layout_GUI();
                    break;
                case LayoutTarget.GUILayout:
                    Layout_GUILayout();
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
		/// Initializes a new instance of the <see cref="Control"/> class.
		/// </summary>
		protected Control()
		{
		}

        public Control ID(string id)
        {
            UI.RegisterID(this, id);
            return this;
        }
	}
}