using System;
using UnityEngine;

namespace AffinityUI
{
    public class ScrollView : Composite<ScrollView>
    {
        Vector2 scrollPosition;
        Func<GUIStyle> horizontalStyle;
        Func<GUIStyle> verticalStyle;
        Func<GUIStyle> backgroundStyle;
        bool alwaysShowHorizontal;
        bool alwaysShowVertical;

        public ScrollView() : base()
        {
            Style(() => GUI.skin.scrollView);
            BackgroundStyle(() => GUI.skin.scrollView);
        }

        public GUIStyle HorizontalStyle()
        {
            return horizontalStyle();
        }

        public GUIStyle VerticalStyle()
        {
            return verticalStyle();
        }

        public ScrollView Style(Func<GUIStyle> horizontalStyle, Func<GUIStyle> verticalStyle)
        {
            this.horizontalStyle = horizontalStyle;
            this.verticalStyle = verticalStyle;
            return this;
        }

        public ScrollView BackgroundStyle(Func<GUIStyle> backgroundStyle)
        {
            this.backgroundStyle = backgroundStyle;
            return this;
        }

        public GUIStyle BackgroundStyle()
        {
            return backgroundStyle();
        }

        public ScrollView AlwaysShowHorizontal()
        {
            alwaysShowHorizontal = true;
            return this;
        }

        public ScrollView AlwaysShowVertical()
        {
            alwaysShowVertical = true;
            return this;
        }

        /// <summary>
        /// Called when layout begins when using GUILayout.
        /// </summary>
        protected override void OnBeginLayout_GUILayout()
        {
            if (horizontalStyle != null && verticalStyle != null)
            {
                scrollPosition = GUILayout.BeginScrollView(
                    scrollPosition,
                    alwaysShowHorizontal,
                    alwaysShowVertical,
                    horizontalStyle(),
                    verticalStyle(),
                    backgroundStyle(),
                    LayoutOptions());
            }
            else
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, Style(), LayoutOptions());
            }
        }

        /// <summary>
        /// Called when layout ends when using GUILayout.
        /// </summary>
        protected override void OnEndLayout_GUILayout()
        {
            GUILayout.EndScrollView();
        }
    }
}