using System;
using UnityEngine;

namespace AffinityUI
{
    internal interface ITooltipStyle
    {
        GUIStyle Style();
    }

    public class Tooltip<TOwner> : ITooltipStyle where TOwner : Control
    {
        TOwner owner;
        BindableProperty<TOwner, string> tooltip;
        Func<Rect> tooltipAreaGetter;
        Func<GUIStyle> styleGetter;

        public Tooltip(TOwner owner)
        {
            this.owner = owner;
            tooltip = new BindableProperty<TOwner, string>(owner);
            tooltipAreaGetter = () => owner.Position();
            styleGetter = () => GUI.skin.label;
        }

        public TOwner OK { get { return owner; } }

        public BindableProperty<TOwner, string> Text()
        {
            return tooltip;
        }

        public Tooltip<TOwner> Text(string text)
        {
            tooltip.Value = text;
            return this;
        }

        public Tooltip<TOwner> Area(Func<Rect> getter)
        {
            tooltipAreaGetter = getter;
            return this;
        }

        public Rect Area()
        {
            return tooltipAreaGetter();
        }

        public GUIStyle Style()
        {
            return styleGetter();
        }

        public Tooltip<TOwner>Style(Func<GUIStyle> getter)
        {
            styleGetter = getter;
            return this;
        }

        public void Layout()
        {
            var renderer = owner.Context.Owner.gameObject.GetComponent<TooltipRenderer>();
            if (renderer == null ||
                string.IsNullOrEmpty(tooltip.Value) ||
                Event.current.type != EventType.Repaint)
            {
                return;
            }
            if (Area().Contains(Event.current.mousePosition))
            {
                renderer.StartTooltip(this, Text());
            }
            else
            {
                renderer.StopTooltip(this);
            }
        }

        public void UpdateBinding()
        {
            tooltip.UpdateBinding();
        }
    }
}