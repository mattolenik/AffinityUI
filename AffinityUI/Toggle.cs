using System;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// A checkbox toggle.
    /// </summary>
    public class Toggle : ContentControl<Toggle>
    {
        BindableProperty<Toggle, bool> isChecked;

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        public Toggle()
            : base()
        {
            Style(() => GUI.skin.toggle);
            isChecked = new BindableProperty<Toggle, bool>(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        /// <param name="label">The label text.</param>
        public Toggle(string label)
            : this()
        {
            Label(label);
        }

        /// <summary>
        /// Called when the control is toggled on or off.
        /// </summary>
        /// <param name="handler">The event handler method.</param>
        /// <returns>this instance</returns>
        public Toggle OnToggled(PropertyChangedEventHandler<Toggle, bool> handler)
        {
            isChecked.PropertyChanged += handler;
            return this;
        }

        public BindableProperty<Toggle, bool> IsChecked()
        {
            return isChecked;
        }

        public Toggle IsChecked(bool value)
        {
            isChecked.Value = value;
            return this;
        }

        /// <summary>
        /// Called when layout is done using GUILayout.
        /// </summary>
        protected override void Layout_GUILayout()
        {
            isChecked.Value = GUILayout.Toggle(isChecked, Content(), Style(), LayoutOptions());
        }
    }
}