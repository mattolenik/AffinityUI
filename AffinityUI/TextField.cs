using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Text.RegularExpressions;

namespace AffinityUI
{
    /// <summary>
    /// An inline text field.
    /// </summary>
    public class TextField : TypedControl<TextField>
    {
        int maxLength;
        BindableProperty<TextField, string> text;

        public BindableProperty<TextField, string> Text()
        {
            return text;
        }

        public TextField Text(string text)
        {
            this.text.Value = text;
            return this;
        }

        public int MaxLength()
        {
            return maxLength;
        }

        public TextField MaxLength(int length)
        {
            this.maxLength = length;
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        public TextField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="label">The label text.</param>
        public TextField(string text)
            : base()
        {
            Style(() => GUI.skin.textField);
            this.text = new BindableProperty<TextField, string>(this, text);
            maxLength = int.MaxValue;
        }

        /// <summary>
        /// Called when layout is done using GUI.
        /// </summary>
        protected override void Layout_GUI()
        {
            Text(GUI.TextField(Position(), Text(), maxLength, Style()));
        }

        /// <summary>
        /// Called when layout is done using GUILayout.
        /// </summary>
        protected override void Layout_GUILayout()
        {

            Text(GUILayout.TextField(Text(), maxLength, Style(), LayoutOptions()));
        }
    }
}
