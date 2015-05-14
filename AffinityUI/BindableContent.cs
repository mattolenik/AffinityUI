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
        BindableProperty<TOwner, string> label;
        GUIContent content;

        public TOwner _ { get { return owner; } }

        public BindableProperty<TOwner, string> Label()
        {
            return label;
        }

        public BindableContent<TOwner> Label(string text)
        {
            label.Value = text;
            return this;
        }

        public GUIContent Content()
        {
            return content;
        }

        public BindableContent<TOwner> Content(GUIContent content)
        {
            this.content = content;
            return this;
        }

        public Texture Image()
        {
            return content.image;
        }

        public BindableContent<TOwner> Image(Texture image)
        {
            content.image = image;
            return this;
        }

        public BindableContent(TOwner owner)
            : this(owner, new GUIContent())
        {
        }

        public BindableContent(TOwner owner, GUIContent content)
        {
            this.owner = owner;
            this.content = content;
            label = new BindableProperty<TOwner, string>(owner);
            label.OnPropertyChanged(((src, old, nw) => content.text = nw));
        }

        public void UpdateBinding()
        {
            label.UpdateBinding();
        }
    }
}