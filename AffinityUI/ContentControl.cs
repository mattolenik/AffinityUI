using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// Generic base class for controls that have text and image content.
    /// </summary>
    /// <typeparam name="TSelf">The type of the implementing subclass.</typeparam>
    public abstract class ContentControl<TSelf> : TypedControl<TSelf> where TSelf : Control
    {
        BindableContent<TSelf> content;
        Tooltip<TSelf> tooltip;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentControl&lt;TSelf&gt;"/> class.
        /// </summary>
        protected ContentControl()
            : base()
        {
            content = new BindableContent<TSelf>(this as TSelf);
            tooltip = new Tooltip<TSelf>(this as TSelf);
        }

        public GUIContent Content()
        {
            return content.Content();
        }

        public TSelf Content(GUIContent content)
        {
            this.content = new BindableContent<TSelf>(this as TSelf, content);
            return this as TSelf;
        }

        public BindableProperty<TSelf, string> Label()
        {
            return content.Label();
        }

        public TSelf Label(string text)
        {
            content.Label(text);
            return this as TSelf;
        }

        public BindableProperty<TSelf, string> Tooltip()
        {
            return tooltip.Text();
        }

        public Tooltip<TSelf> Tooltip(string text)
        {
            tooltip.Text(text);
            return tooltip;
        }

        public Texture Image()
        {
            return content.Image();
        }

        public TSelf Image(Texture image)
        {
            content.Image(image);
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
            tooltip.Layout();
        }

        protected virtual void UpdateBindings()
        {
            content.UpdateBinding();
            tooltip.UpdateBinding();
        }
    }
}