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
		GUIContent _content = new GUIContent();

        BindableProperty<ContentControl<TSelf>, String> _label;

        BindableProperty<ContentControl<TSelf>, String> _tooltip;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentControl&lt;TSelf&gt;"/> class.
		/// </summary>
		protected ContentControl()
			: base()
		{
            _label = new BindableProperty<ContentControl<TSelf>, String>(this);
            _tooltip = new BindableProperty<ContentControl<TSelf>, String>(this);
            _label.OnPropertyChanged((source, old, nw) => _content.text = nw);
            _tooltip.OnPropertyChanged((source, old, nw) =>
            {
                _content.tooltip = nw;
//                var renderer = Context.Owner.gameObject.GetComponent<TooltipRenderer>();
//                if(renderer != null)
//                {
//                    renderer.Tooltip = new GUIContent(nw);
//                }
            });
		}

        public GUIContent Content()
        {
            return _content;
        }

        public TSelf Content(GUIContent content)
        {
            _content = content;
            return this as TSelf;
        }

        public BindableProperty<ContentControl<TSelf>, String> Label()
        {
            return _label;
        }

        public TSelf Label(String text)
        {
            _label.Value = text;
            return this as TSelf;
        }
            
        public BindableProperty<ContentControl<TSelf>, String> Tooltip()
        {
            return _tooltip;
        }

        public TSelf Tooltip(String text)
        {
            _tooltip.Value = text;
            return this as TSelf;
        }

        public Texture Image()
        {
            return _content.image;
        }

        public TSelf Image(Texture image)
        {
            _content.image = image;
            return this as TSelf;
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
            if (!Visible())
            {
                return;
            }
			UpdateBindings();
			base.Layout();
		}

		/// <summary>
		/// Updates and <see cref="BindableProperty&lt;TOwner, TProperty&gt;"/> members.
		/// If overriden in a derived class, be sure to call the base class method.
		/// </summary>
		protected virtual void UpdateBindings()
        {
            _label.UpdateBinding();
            _tooltip.UpdateBinding();
        }
	}
}