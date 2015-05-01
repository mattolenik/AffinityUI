using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// Generic base class for controls that have content.
	/// </summary>
	/// <typeparam name="TSelf">The type of the implementing subclass.</typeparam>
	public abstract class ContentControl<TSelf> : ControlBase<TSelf> where TSelf : Control
	{
		private GUIContent _content = new GUIContent();

		private GUIStyle _style = GUIStyle.none;

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Label"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Label property.</value>
		public BindableProperty<ContentControl<TSelf>, String> LabelProperty { get; private set; }

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Tooltip"/> property.
		/// </summary>
		/// <value>The BindableProperty for the Tooltip property.</value>
		public BindableProperty<ContentControl<TSelf>, String> TooltipProperty { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentControl&lt;TSelf&gt;"/> class.
		/// </summary>
		protected ContentControl()
			: base()
		{
			LabelProperty = new BindableProperty<ContentControl<TSelf>, String>(this);
			TooltipProperty = new BindableProperty<ContentControl<TSelf>, String>(this);
		}

		/// <summary>
		/// Gets or sets the GUI content.
		/// </summary>
		/// <value>The content.</value>
		public GUIContent Content
		{
			get { return _content; }
			set { _content = value; }
		}

		/// <summary>
		/// Gets or sets the text of the <see cref="Content"/> property.
		/// </summary>
		/// <value>The label text.</value>
		public String Label
		{
			get { return LabelProperty.Value; }
			set
			{
				LabelProperty.Value = value;
				_content.text = value;
			}
		}

		/// <summary>
		/// Gets or sets the tooltip text of the <see cref="Content"/> property.
		/// </summary>
		/// <value>The tooltip text.</value>
		public String Tooltip
		{
			get { return TooltipProperty.Value; }
			set
			{
				TooltipProperty.Value = value;
				_content.tooltip = value;
			}
		}

		/// <summary>
		/// Gets or sets the image of the <see cref="Content"/> property.
		/// </summary>
		/// <value>The image.</value>
		public Texture Image
		{
			get { return _content.image; }
			set { _content.image = value; }
		}

		/// <summary>
		/// Gets or sets the Unity GUI style.
		/// </summary>
		/// <value>The style.</value>
		public GUIStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}

		/// <summary>
		/// Sets the <see cref="Content"/> property.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <returns>this instance</returns>
		public TSelf SetContent(GUIContent content)
		{
			this._content = content;
			return Self;
		}

		/// <summary>
		/// Sets the <see cref="Label"/> property.
		/// </summary>
		/// <param name="text">The label text.</param>
		/// <returns>this instance</returns>
		public TSelf SetLabel(String text)
		{
			Label = text;
			return Self;
		}

		/// <summary>
		/// Sets the <see cref="Image"/> property.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>this instance</returns>
		public TSelf SetImage(Texture image)
		{
			_content.image = image;
			return Self;
		}

		/// <summary>
		/// Sets the tooltip text.
		/// </summary>
		/// <param name="text">The tooltip text.</param>
		/// <returns>this instance</returns>
		public TSelf SetTooltip(String text)
		{
			Tooltip = text;
			return Self;
		}

		/// <summary>
		/// Sets the GUI style.
		/// </summary>
		/// <returns>this instance</returns>
		public TSelf SetStyle(GUIStyle style)
		{
			this._style = style;
			return Self;
		}

		/// <summary>
		/// Performs the necessary calls to UnityGUI to perform layout or updates.
		/// Should be called in the OnGUI methods.
		/// </summary>
		/// <remarks>
		/// Only override this method if you need complete control over GUI layout.
		/// This method automatically calls <see cref="Control.Layout_GUI"/> or
		/// <see cref="Control.Layout_GUILayout"/>.
		/// </remarks>
		public override void Layout()
		{
			UpdateBindings();
			base.Layout();
		}

		/// <summary>
		/// Updates and <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> members.
		/// If overriden in a derived class, be sure to call the base class method.
		/// </summary>
		protected virtual void UpdateBindings()
		{
			if (LabelProperty.IsBound)
			{
				LabelProperty.UpdateBinding();
				_content.text = LabelProperty.Value;
			}
			if (TooltipProperty.IsBound)
			{
				TooltipProperty.UpdateBinding();
				_content.tooltip = TooltipProperty.Value;
			}
		}
	}
}