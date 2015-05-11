using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// A composite control that's composed of zero or more child controls.
	/// </summary>
    public abstract class Composite<TSelf> : ContentControl<TSelf>, IEnumerable<Control> where TSelf : Control
	{
		 IList<Control> children = new List<Control>();

		/// <summary>
		/// Gets a list of child controls.
		/// </summary>
		/// <value>The children.</value>
		public IList<Control> Children
		{
			get { return children; }
		}

		/// <summary>
		/// Called when layout begins. Automatically calls <see cref="OnBeginLayout_GUILayout"/>
		/// or <see cref="OnBeginLayout_GUI"/>.
		/// </summary>
		/// <remarks>
		/// Override this method if you need complete control over GUI layout. Otherwise override
		/// <see cref="OnBeginLayout_GUILayout"/>
		/// </remarks>
		protected virtual void OnBeginLayout()
		{
            switch (Context.Layout)
            {
                case LayoutTarget.GUI:
                    OnBeginLayout_GUI();
                    break;
                case LayoutTarget.GUILayout:
                    OnBeginLayout_GUILayout();
                    break;
                default:
                    throw new InvalidOperationException("Layout must be either GUI or GUILayout");
            }
		}

		/// <summary>
		/// Called when layout end. Automatically calls <see cref="OnBeginLayout_GUILayout"/> or
		/// <see cref="OnBeginLayout_GUI"/>.
		/// </summary>
		/// <remarks>
		/// Override this method if you need complete control over GUI layout. Otherwise override
		/// <see cref="OnEndLayout_GUILayout"/>
		/// </remarks>
		protected virtual void OnEndLayout()
        {
            switch (Context.Layout)
            {
                case LayoutTarget.GUI:
                    OnEndLayout_GUI();
                    break;
                case LayoutTarget.GUILayout:
                    OnEndLayout_GUILayout();
                    break;
                default:
                    throw new InvalidOperationException("Layout must be either GUI or GUILayout");
            }
        }

		/// <summary>
		/// Called when layout begins using GUI.
		/// </summary>
		protected virtual void OnBeginLayout_GUI() { }

		/// <summary>
		/// Called when layout ends using GUI.
		/// </summary>
		protected virtual void OnEndLayout_GUI() { }
		/// <summary>
		/// Called when layout begins when using GUILayout.
		/// </summary>
		protected virtual void OnBeginLayout_GUILayout() { }

		/// <summary>
		/// Called when layout ends when using GUILayout.
		/// </summary>
		protected virtual void OnEndLayout_GUILayout() { }

		protected internal override UIContext Context
		{
			get
			{
				return base.Context;
			}
			set
			{
				base.Context = value;
				foreach (var control in Children)
				{
					control.Context = value;
				}
			}
		}

		/// <summary>
		/// Adds the specified control.
		/// </summary>
		/// <typeparam name="TControl">The type of the control.</typeparam>
		/// <param name="control">The control.</param>
		/// <returns>The current composite instance.</returns>
        public TSelf Add(Control control)
		{
			Children.Add(control);
			control.Context = Context;
			control.Parent = this;
            if (!control.IndependantSkin)
            {
                control.SkinValue = SkinValue;
            }
            return this as TSelf;
		}

		/// <summary>
		/// Removes the specified control.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>The current composite instance.</returns>
        public TSelf Remove(Control control)
		{
			Children.Remove(control);
			control.Parent = null;
            control.Context = null;
            return this as TSelf;
		}

		public override void Layout()
		{
            if (!Visible())
            {
                return;
            }
            GUI.skin = SkinValue;

			OnBeginLayout();

			foreach (var control in Children)
			{
                control.Layout();
			}

			OnEndLayout();
		}

        public TSelf Skin(GUISkin skin)
        {
            SkinValue = skin;
            return this as TSelf;
        }

        protected internal override GUISkin SkinValue
        {
            get
            {
                return base.SkinValue;
            }
            set
            {
                base.SkinValue = value;
                foreach (var control in Children)
                {
                    if (!control.IndependantSkin)
                    {
                        control.SkinValue = value;
                    }
                }
            }
        }

        #region IEnumerable<Control> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Control> GetEnumerator()
        {
            foreach (var control in children)
            {
                yield return control;
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var control in children)
            {
                yield return control;
            }
        }

        #endregion
    }
}