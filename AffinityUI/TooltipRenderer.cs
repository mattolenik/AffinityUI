using System;
using UnityEngine;
using System.Collections.Generic;

namespace AffinityUI
{
    public class TooltipRenderer : MonoBehaviour
    {
        public readonly IDictionary<Control, GUIContent> Tooltip = new Dictionary<Control, GUIContent>();

        public readonly IDictionary<Control, bool> ShouldRender = new Dictionary<Control, bool>();

        void OnGUI()
        {
            GUI.depth = -10;
            foreach (var key in Tooltip.Keys)
            {
                if (ShouldRender[key])
                {
                    var pos = Event.current.mousePosition;
                    var size = GUI.skin.label.CalcSize(Tooltip[key]);
                    GUI.Label(new Rect(pos.x + 25, pos.y, size.x, size.y), Tooltip[key], GUI.skin.label);
                }
            }
        }
    }
}

