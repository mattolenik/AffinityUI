using System;
using UnityEngine;

namespace AffinityUI
{
    public class HorizontalSlider : TypedControl<HorizontalSlider>
    {
        Func<GUIStyle> thumbStyle;
        BindableProperty<HorizontalSlider, float> sliderValue;
        float leftValue;
        float rightValue;

        public BindableProperty<HorizontalSlider, float> Value()
        {
            return sliderValue;
        }

        public HorizontalSlider Value(float value)
        {
            sliderValue.Value = value;
            return this;
        }

        public GUIStyle ThumbStyle()
        {
            return thumbStyle();
        }

        public HorizontalSlider ThumbStyle(Func<GUIStyle> styleGetter)
        {
            thumbStyle = styleGetter;
            return this;
        }

        public HorizontalSlider(float leftValue, float rightValue, float defaultValue = 0)
            : base()
        {
            Style(() => GUI.skin.horizontalSlider);
            thumbStyle = () => GUI.skin.horizontalSliderThumb;
            sliderValue = new BindableProperty<HorizontalSlider, float>(this, defaultValue);
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        protected override void Layout_GUILayout()
        {
            sliderValue.Value = GUILayout.HorizontalSlider(sliderValue, leftValue, rightValue, Style(), thumbStyle(), LayoutOptions());
        }
    }
}