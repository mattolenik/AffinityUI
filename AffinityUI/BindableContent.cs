using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// A bindable drop-in for GUIContent
    /// </summary>
    public class BindableContent
    {
        BindableProperty<BindableContent, String> _label;

        BindableProperty<BindableContent, String> _tooltip;

        public GUIContent Content { get; set; }

        public BindableProperty<BindableContent, String> Label()
        {
            return _label;
        }

        public BindableContent Label(String text)
        {
            _label.Value = text;
            Content.text = text;
            return this;
        }

        public BindableProperty<BindableContent, String>Tooltip()
        {
            return _tooltip;
        }

        public BindableContent Tooltip(string text)
        {
            _tooltip.Value = text;
            Content.tooltip = text;
            return this;
        }

        public Texture Image()
        {
            return Content.image;
        }

        public BindableContent Image(Texture image)
        {
            Content.image = image;
            return this;
        }

        public BindableContent()
        {
            Content = new GUIContent();

            _label = new BindableProperty<BindableContent, String>(this);
            _tooltip = new BindableProperty<BindableContent, String>(this);
        }
    }
}