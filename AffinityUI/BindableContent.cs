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
        public BindableProperty<BindableContent, String> LabelProperty { get; private set; }

        public BindableProperty<BindableContent, String> TooltipProperty { get; private set; }

        public GUIContent Content { get; set; }

        public String Label
        {
            get { return LabelProperty.Value; }
            set
            {
                LabelProperty.Value = value;
                Content.text = value;
            }
        }

        public String Tooltip
        {
            get { return TooltipProperty.Value; }
            set
            {
                TooltipProperty.Value = value;
                Content.tooltip = value;
            }
        }

        public Texture Image
        {
            get { return Content.image; }
            set { Content.image = value; }
        }

        public BindableContent()
        {
            Content = new GUIContent();

            LabelProperty = new BindableProperty<BindableContent, String>(this);
            TooltipProperty = new BindableProperty<BindableContent, String>(this);
        }
    }
}