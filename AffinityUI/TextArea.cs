using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// A multi-line text area.
    /// </summary>
    public class TextArea : ContentControl<TextArea>
    {
        int maxLength;
        BindableProperty<TextArea, string> text;

        public BindableProperty<TextArea, string> Text()
        {
            return text;
        }

        public TextArea Text(string text)
        {
            this.text.Value = text;
            return this;
        }

        public int MaxLength()
        {
            return maxLength;
        }

        public TextArea MaxLength(int length)
        {
            maxLength = length;
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextArea"/> class.
        /// </summary>
        public TextArea()
            : base()
        {
            Style(() => GUI.skin.textArea);
            text = new BindableProperty<TextArea, string>(this, string.Empty);
            maxLength = int.MaxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextArea"/> class.
        /// </summary>
        /// <param name="label">The label text.</param>
        public TextArea(string label)
            : this()
        {
            Label(label);
        }

        /// <summary>
        /// Called when layout is done using GUI.
        /// </summary>
        protected override void Layout_GUI()
        {
            Text(GUI.TextArea(Position(), Text(), maxLength, Style()));
        }

        /// <summary>
        /// Called when layout is done using GUILayout.
        /// </summary>
        protected override void Layout_GUILayout()
        {
            Text(GUILayout.TextArea(Text(), maxLength, Style(), LayoutOptions()));
        }
    }
}
