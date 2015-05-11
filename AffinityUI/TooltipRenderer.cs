using System;
using UnityEngine;
using System.Collections.Generic;

namespace AffinityUI
{
    public class TooltipRenderer : MonoBehaviour
    {
        Dictionary<Control, GUIContent> _tooltips = new Dictionary<Control, GUIContent>();

        Dictionary<Control, State> state = new Dictionary<Control, State>();

        public void StartTooltip(Control control, string text)
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

        public void StopTooltip(Control control)
        {
            if (state.ContainsKey(control))
            {
                var ste = state[control];
                ste.LastEnded = DateTime.Now;
                ste.Expired = false;
            }
        }

        public TimeSpan Timeout { get; set; }

        public TooltipRenderer()
        {
            Timeout = TimeSpan.FromSeconds(2);
        }

        void OnGUI()
        {
            GUI.depth = -10;
            foreach (var key in _tooltips.Keys)
            {
                var ste = state[key];
                if (ste.LastStarted + Timeout > DateTime.Now && ste.LastEnded < ste.LastStarted && !ste.Expired)
                {
                    var pos = Event.current.mousePosition;
                    var size = GUI.skin.label.CalcSize(_tooltips[key]);
                    GUI.Label(new Rect(pos.x + 25, pos.y, size.x, size.y), _tooltips[key], GUI.skin.label);
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
            public bool Expired = false;
        }
    }
}