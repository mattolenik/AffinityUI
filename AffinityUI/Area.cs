using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
	/// <summary>
	/// Contains a <see cref="GUILayout"/> block in a fixed screen area.
	/// </summary>
	public class Area : Composite
	{
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public GUIContent Content { get; set; }

		/// <summary>
		/// Gets the <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> corresponding to the <see cref="Label"/> property.
		/// </summary>
		/// <value>The label property.</value>
		public BindableProperty<Area, String> LabelProperty { get; private set; }

		/// <summary>
		/// Gets or sets the text of the <see cref="Content"/> property.
		/// </summary>
		/// <value>The label text.</value>
		public String Label
		{
			get { return Content.text; }
			set
			{
				Content.text = value;
				LabelProperty.Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the image of the <see cref="Content"/> property.
		/// </summary>
		/// <value>The image.</value>
		public Texture Image
		{
			get { return Content.image; }
			set { Content.image = value; }
		}

		/// <summary>
		/// Gets or sets the dimensions.
		/// </summary>
		/// <value>The dimensions.</value>
		public Rect Dimensions { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Area"/> class.
		/// </summary>
		public Area()
			: base()
		{
			Content = new GUIContent();
			LabelProperty = new BindableProperty<Area, String>(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Area"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions.</param>
		public Area(Rect dimensions)
			: this()
		{
			Dimensions = dimensions;
		}

		/// <summary>
		/// Sets the <see cref="Dimensions"/> property.
		/// </summary>
		/// <param name="dimensions">The dimensions.</param>
		/// <returns>this instance</returns>
		public Area SetDimensions(Rect dimensions)
		{
			Dimensions = dimensions;
			return this;
		}

		/// <summary>
		/// Sets the <see cref="Label"/> property.
		/// </summary>
		/// <param name="text">The label text.</param>
		/// <returns>this instance</returns>
		public Area SetLabel(String text)
		{
			Label = text;
			return this;
		}

		/// <summary>
		/// Sets the <see cref="Image"/> property.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>this instance</returns>
		public Area SetImage(Texture image)
		{
			Image = image;
			return this;
		}

		/// <summary>
		/// Called when layout is done using GUI.
		/// </summary>
		protected override void Layout_GUI()
		{
			throw new NotSupportedException("GUI is not supported by the Area control.");
		}
		/// <summary>
		/// Called when layout begins when using GUILayout.
		/// </summary>
		protected override void OnBeginLayout_GUILayout()
		{
			GUILayout.BeginArea(Dimensions, Label);
		}

		/// <summary>
		/// Called when layout ends when using GUILayout.
		/// </summary>
		protected override void OnEndLayout_GUILayout()
		{
			GUILayout.EndArea();
		}
	}
}
