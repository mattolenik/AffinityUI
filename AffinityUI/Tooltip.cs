using System;
using UnityEngine;

namespace AffinityUI
{
    public class Tooltip<TOwner> where TOwner : Control
    {
        TOwner owner;

        BindableProperty<TOwner, string> tooltip;

        Func<Rect> tooltipAreaGetter;

        public Tooltip(TOwner owner)
        {
            this.owner = owner;
            tooltip = new BindableProperty<TOwner, string>(owner);
            tooltipAreaGetter = () => owner.Position();
        }

        public TOwner _ { get { return owner; } }

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

        public void Layout()
        {
            var renderer = owner.Context.Owner.gameObject.GetComponent<TooltipRenderer>();
            if (renderer != null)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    if (Area().Contains(Event.current.mousePosition))
                    {
                        renderer.StartTooltip(owner, Text());
                    }
                    else
                    {
                        renderer.StopTooltip(owner);
                    }
                }
            }
        }
    }
}