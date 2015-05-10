using System;
using UnityEngine;

namespace AffinityUI
{
    public class TooltipRenderer : MonoBehaviour
    {
        public GUIContent Tooltip { get; set; }

        void OnGUI()
        {
            if (Tooltip == null)
            {
                return;
            }
            GUI.depth = 10;
            var pos = Event.current.mousePosition;
            var size = GUI.skin.label.CalcSize(Tooltip);
            GUI.Label(new Rect(pos.x + 25, pos.y, size.x, size.y), Tooltip, GUI.skin.label);
        }
    }
}

