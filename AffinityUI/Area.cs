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

        BindableProperty<Area, String> _label;

        public BindableProperty<Area, String> Label()
        {
            return _label;
        }

        public Area Label(string text)
        {
            _label.Value = text;
            return this;
        }

        Rect _dimensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="Area"/> class.
		/// </summary>
		public Area()
			: base()
		{
			Content = new GUIContent();
			_label = new BindableProperty<Area, String>(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Area"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions.</param>
		public Area(Rect dimensions)
			: this()
		{
			_dimensions = dimensions;
		}

		/// <summary>
		/// Sets the <see cref="Dimensions"/> property.
		/// </summary>
		/// <param name="dimensions">The dimensions.</param>
		/// <returns>this instance</returns>
		public Area Dimensions(Rect dimensions)
		{
            _dimensions = dimensions;
			return this;
		}

        public Rect Dimensions()
        {
            return _dimensions;
        }

		/// <summary>
		/// Sets the <see cref="Image"/> property.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>this instance</returns>
		public Area Image(Texture image)
		{
            Content.image = image;
			return this;
		}

        public Texture Image()
        {
            return Content.image;
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
            GUILayout.BeginArea(_dimensions, Label());
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
