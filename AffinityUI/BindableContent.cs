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
    public class BindableContent<TOwner>
    {
        BindableProperty<TOwner, string> _label;

        BindableProperty<TOwner, string> _tooltip;

        public GUIContent Content { get; set; }

        public BindableProperty<TOwner, string> Label()
        {
            return _label;
        }

        public BindableContent<TOwner> Label(string text)
        {
            _label.Value = text;
            return this;
        }

        public BindableProperty<TOwner, string>Tooltip()
        {
            return _tooltip;
        }

        public BindableContent<TOwner> Tooltip(string text)
        {
            _tooltip.Value = text;
            return this;
        }

        public Texture Image()
        {
            return Content.image;
        }

        public BindableContent<TOwner> Image(Texture image)
        {
            Content.image = image;
            return this;
        }

        public BindableContent(TOwner owner)
            : this(owner, new GUIContent())
        {
        }

        public BindableContent(TOwner owner, GUIContent content)
        {
            Content = content;
            _label = new BindableProperty<TOwner, string>(owner);
            _label.OnPropertyChanged(((src, old, nw) => Content.text = nw));
            _tooltip = new BindableProperty<TOwner, string>(owner);
            _tooltip.OnPropertyChanged(((src, old, nw) => Content.tooltip = nw));
        }

        public void UpdateBinding()
        {
            _label.UpdateBinding();
            _tooltip.UpdateBinding();
        }
    }
}