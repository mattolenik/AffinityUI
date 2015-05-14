using System;
using UnityEngine;
using System.Collections.Generic;

namespace AffinityUI
{
    public class TooltipRenderer : MonoBehaviour
    {
        Dictionary<ITooltipStyle, GUIContent> tooltips = new Dictionary<ITooltipStyle, GUIContent>();
        Dictionary<ITooltipStyle, State> state = new Dictionary<ITooltipStyle, State>();

        public TimeSpan Timeout { get; set; }

        public TimeSpan Delay { get; set; }

        public Vector2 CursorOffset { get; set; }

        public int Depth { get; set; }

        public TooltipRenderer()
        {
            Delay = TimeSpan.FromMilliseconds(500);
            Timeout = TimeSpan.FromSeconds(8);
            CursorOffset = new Vector2(25, 10);
            Depth = -10;
        }

        public void StartTooltip<TOwner>(Tooltip<TOwner> control, string text) where TOwner : Control
        {
            if (state.ContainsKey(control))
            {
                var ste = state[control];
                if (ste.LastEnded > ste.LastStarted)
                {
                    ste.LastStarted = DateTime.Now;
                    tooltips[control] = new GUIContent(text);
                }
            }
            else
            {
                state[control] = new State { LastStarted = DateTime.Now, LastEnded = DateTime.MinValue };
                tooltips[control] = new GUIContent(text);
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

        void OnGUI()
        {
            GUI.depth = Depth;
            foreach (var key in tooltips.Keys)
            {
                var ste = state[key];
                if (ste.LastStarted + Timeout > DateTime.Now &&
                    ste.LastStarted + Delay < DateTime.Now &&
                    ste.LastEnded < ste.LastStarted &&
                    !ste.Expired)
                {
                    var pos = Event.current.mousePosition;
                    var size = key.Style().CalcSize(tooltips[key]);
                    GUI.Label(new Rect(pos.x + CursorOffset.x, pos.y + CursorOffset.y, size.x, size.y), tooltips[key], key.Style());
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