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
	public abstract class Composite : Control, IEnumerable<Control>
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
		/// Gets or sets a value indicating whether or not child controls should have their skins set on next update.
		/// </summary>
		/// <value><c>true</c> if child skins should be updated; otherwise, <c>false</c>.</value>
		protected bool SetChildSkins { get; set; }

		/// <summary>
		/// Gets or sets the Unity <see cref="GUISkin"/>.
		/// </summary>
		/// <value>The skin.</value>
		/// <remarks>
		/// Skins are applied recursively to all child controls.
		/// To exclude a control, set its skin to null or to another skin.
		/// </remarks>
		public override GUISkin Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
				SetChildSkins = true;
			}
		}

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Control.Visible"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Visible property.</value>
		public BindableProperty<Composite, bool> VisibleProperty { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Composite"/> class.
		/// </summary>
		public Composite()
			: base()
		{
			VisibleProperty = new BindableProperty<Composite, bool>(this, true);
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
			if (TargetType == typeof(GUILayout))
			{
				OnBeginLayout_GUILayout();
			}
			else if (TargetType == typeof(GUI))
			{
				OnBeginLayout_GUI();
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
            if (TargetType == typeof(GUILayout))
            {
                OnEndLayout_GUILayout();
            }
            else if (TargetType == typeof(GUI))
            {
                OnEndLayout_GUI();
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

		/// <summary>
		/// Gets or sets the type of the target, either GUILayout or EditorGUILayout.
		/// </summary>
		/// <value>The type of the layout target.</value>
		protected internal override Type TargetType
		{
			get
			{
				return base.TargetType;
			}
			set
			{
				base.TargetType = value;
				foreach (var control in Children)
				{
					control.TargetType = value;
				}
			}
		}

		/// <summary>
		/// Adds the specified control.
		/// </summary>
		/// <typeparam name="TControl">The type of the control.</typeparam>
		/// <param name="control">The control.</param>
		/// <returns>The current composite instance.</returns>
		public Composite Add<TControl>(TControl control) where TControl : Control
		{
			Children.Add(control);
			control.TargetType = TargetType;
			control.Parent = this;
			return this;
		}

		/// <summary>
		/// Removes the specified control.
		/// </summary>
		/// <typeparam name="TControl">The type of the control.</typeparam>
		/// <param name="control">The control.</param>
		/// <returns>The current composite instance.</returns>
		public Composite Remove<TControl>(TControl control) where TControl : Control
		{
			Children.Remove(control);
			control.Parent = null;
			return this;
		}

		/// <summary>
		/// Sets the <see cref="Control.Visible"/> property.
		/// </summary>
		/// <param name="visible">if set to <c>true</c> [visible].</param>
		/// <returns></returns>
		public Composite SetVisible(bool visible)
		{
            Visible.Value = visible;
			return this;
		}

		/// <summary>
		/// Recursively sets the skin on all child controls.
		/// </summary>
		/// <param name="skin">The skin.</param>
		protected void RecursiveSetSkin(GUISkin skin)
		{
			_skin = skin;
			foreach (var child in Children)
			{
				var composite = child as Composite;
				if (composite != null)
				{
					composite.RecursiveSetSkin(skin);
				}
				else
				{
					child.Skin = skin;
				}
			}
		}

		/// <summary>
		/// Performs the necessary calls to UnityGUI to perform layout or updates.
		/// Should be called in the OnGUI methods.
		/// </summary>
		public override void Layout()
		{
            if (!Visible)
            {
                return;
            }

			if (SetChildSkins)
			{
				RecursiveSetSkin(Skin);
				SetChildSkins = false;
			}

			LayoutSetSkin();

			OnBeginLayout();

			foreach (var control in Children)
			{
                control.Layout();
			}

			OnEndLayout();
		}

		/// <summary>
		/// Creates a default composite for the specified GUI target.
		/// </summary>
		/// <typeparam name="TGuiTarget">The type of the GUI target.</typeparam>
		/// <typeparam name="TComposite">The type of the root composite.</typeparam>
		/// <returns>
		/// A composite of type TComposite to which other controls can be added.
		/// </returns>
		public static TComposite Create<TGuiTarget, TComposite>() where TComposite : Composite, new()
		{
			var result = new TComposite();
			result.TargetType = typeof(TGuiTarget);
			if (result.TargetType != typeof(GUILayout) &&
				result.TargetType != typeof(GUI))
			{
				throw new ArgumentException("Generic argument must be type GUI or GUILayout ", "TGuiTarget");
			}
			return result;
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