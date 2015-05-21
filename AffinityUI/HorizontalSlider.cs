using System;
using UnityEngine;

namespace AffinityUI
{
    public class VerticalSlider : TypedControl<VerticalSlider>
    {
        Func<GUIStyle> thumbStyle;
        BindableProperty<VerticalSlider, float> sliderValue;
        float leftValue;
        float rightValue;

        public BindableProperty<VerticalSlider, float> Value()
        {
            return sliderValue;
        }

        public VerticalSlider Value(float value)
        {
            sliderValue.Value = value;
            return this;
        }

        public GUIStyle ThumbStyle()
        {
            return thumbStyle();
        }

        public VerticalSlider ThumbStyle(Func<GUIStyle> styleGetter)
        {
            thumbStyle = styleGetter;
            return this;
        }

        public VerticalSlider(float leftValue, float rightValue, float defaultValue = 0)
            : base()
        {
            Style(() => GUI.skin.horizontalSlider);
            thumbStyle = () => GUI.skin.horizontalSliderThumb;
            sliderValue = new BindableProperty<VerticalSlider, float>(this, defaultValue);
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        protected override void Layout_GUILayout()
        {
            sliderValue.Value = GUILayout.VerticalSlider(sliderValue, leftValue, rightValue, Style(), thumbStyle(), LayoutOptions());
        }
    }
}