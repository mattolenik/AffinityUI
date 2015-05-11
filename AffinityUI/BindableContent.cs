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
        TOwner owner;

        BindableProperty<TOwner, string> _label;

        GUIContent _content;

        public TOwner _ { get { return owner; } }

        public BindableProperty<TOwner, string> Label()
        {
            return _label;
        }

        public BindableContent<TOwner> Label(string text)
        {
            _label.Value = text;
            return this;
        }

        public GUIContent Content()
        {
            return _content;
        }

        public BindableContent<TOwner> Content(GUIContent content)
        {
            _content = content;
            return this;
        }

        public Texture Image()
        {
            return _content.image;
        }

        public BindableContent<TOwner> Image(Texture image)
        {
            _content.image = image;
            return this;
        }

        public BindableContent(TOwner owner)
            : this(owner, new GUIContent())
        {
        }

        public BindableContent(TOwner owner, GUIContent content)
        {
            this.owner = owner;
            _content = content;
            _label = new BindableProperty<TOwner, string>(owner);
            _label.OnPropertyChanged(((src, old, nw) => _content.text = nw));
        }

        public void UpdateBinding()
        {
            _label.UpdateBinding();
        }
    }
}