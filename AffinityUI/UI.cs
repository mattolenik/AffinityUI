using System;
using System.Collections.Generic;
using UnityEngine;

namespace AffinityUI
{
    public static class UI
    {
        static readonly Dictionary<String, Control> controlsWithID = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);

        internal static void RegisterID(Control control, String id)
        {
            controlsWithID[id] = control;
        }

        public static TControl ByID<TControl>(String id) where TControl : Control
        {
            Control value;
            if (controlsWithID.TryGetValue(id, out value))
            {
                return value as TControl;
            }
            //controls.Add(new Button("asdf"));
            throw new KeyNotFoundException("Could not find control with ID " + id);

        }

        public static UIContext GUI(MonoBehaviour owner, Control content)
        {
            var context = new UIContext{ Layout = LayoutTarget.GUI, Owner = owner, Content = content };
            content.Context = context;
            return context;
        }

        public static UIContext GUILayout(MonoBehaviour owner, Control content)
        {
            var context = new UIContext{ Layout = LayoutTarget.GUILayout, Owner = owner, Content = content };
            content.Context = context;
            return context;
        }
    }
        
    public class UIContext
    {
        internal LayoutTarget Layout { get; set; }
        internal MonoBehaviour Owner { get; set; }
        internal Control Content { get; set; }

        public void OnGUI()
        {
            Content.Layout();
        }
    }

    public enum LayoutTarget
    {
        GUI = 0,
        GUILayout = 1
    }
}

