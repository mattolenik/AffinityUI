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
	public abstract class ContentControl<TSelf> : TypedControl<TSelf> where TSelf : Control
	{
		GUIContent _content;

        BindableProperty<TSelf, string> _label;

        BindableProperty<TSelf, string> _tooltip;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentControl&lt;TSelf&gt;"/> class.
		/// </summary>
		protected ContentControl()
			: base()
		{
            _content = new GUIContent();
            _label = new BindableProperty<TSelf, string>(this as TSelf);
            _tooltip = new BindableProperty<TSelf, string>(this as TSelf);
            _label.OnPropertyChanged((source, old, nw) => _content.text = nw);
            _tooltip.OnPropertyChanged((source, old, nw) => _content.tooltip = nw);
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

        public BindableProperty<TSelf, string> Label()
        {
            return _label;
        }

        public TSelf Label(string text)
        {
            _label.Value = text;
            return this as TSelf;
        }
            
        public BindableProperty<TSelf, string> Tooltip()
        {
            return _tooltip;
        }

        public TSelf Tooltip(string text)
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

            var renderer = Context.Owner.gameObject.GetComponent<TooltipRenderer>();
            if (renderer != null)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                    {
                        renderer.ShouldRender[this] = true;
                        renderer.Tooltip[this] = new GUIContent(_tooltip);
                    }
                    else
                    {
                        renderer.ShouldRender[this] = false;
                    }
                }
            }
		}

        protected virtual void UpdateBindings()
        {
            _label.UpdateBinding();
            _tooltip.UpdateBinding();
        }
	}
}