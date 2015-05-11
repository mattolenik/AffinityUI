using System;
using UnityEngine;
using System.Collections.Generic;

namespace AffinityUI
{
    public class TooltipRenderer : MonoBehaviour
    {
        Dictionary<ITooltipStyle, GUIContent> _tooltips = new Dictionary<ITooltipStyle, GUIContent>();

        Dictionary<ITooltipStyle, State> state = new Dictionary<ITooltipStyle, State>();

        public void StartTooltip<TOwner>(Tooltip<TOwner> control, string text) where TOwner : Control
        {
            if (state.ContainsKey(control))
            {
                var ste = state[control];
                if (ste.LastEnded > ste.LastStarted)
                {
                    ste.LastStarted = DateTime.Now;
                    _tooltips[control] = new GUIContent(text);
                }
            }
            else
            {
                state[control] = new State { LastStarted = DateTime.Now, LastEnded = DateTime.MinValue };
                _tooltips[control] = new GUIContent(text);
            }
        }

        public void StopTooltip<TOwner>(Tooltip<TOwner> control) where TOwner : Control
        {
            if (state.ContainsKey(control))
            {
                var ste = state[control];
                ste.LastEnded = DateTime.Now;
                ste.Expired = false;
            }
        }

        public TimeSpan Timeout { get; set; }

        public TimeSpan Delay { get; set; }

        public Vector2 CursorOffset { get; set; }

        public TooltipRenderer()
        {
            Delay = TimeSpan.FromMilliseconds(500);
            Timeout = TimeSpan.FromSeconds(8);
            CursorOffset = new Vector2(25, 10);
        }

        void OnGUI()
        {
            GUI.depth = -10;
            foreach (var key in _tooltips.Keys)
            {
                var ste = state[key];
                if (ste.LastStarted + Timeout > DateTime.Now &&
                    ste.LastStarted + Delay < DateTime.Now &&
                    ste.LastEnded < ste.LastStarted &&
                    !ste.Expired)
                {
                    var pos = Event.current.mousePosition;
                    var size = key.Style().CalcSize(_tooltips[key]);
                    GUI.Label(new Rect(pos.x + CursorOffset.x, pos.y + CursorOffset.y, size.x, size.y), _tooltips[key], key.Style());
                }
                if (ste.LastStarted + Timeout < DateTime.Now)
                {
                    ste.LastEnded = DateTime.Now;
                    ste.Expired = true;
                }
            }
        }

        class State
        {
            public DateTime LastStarted;
            public DateTime LastEnded;
            public bool Expired;
        }
    }
}