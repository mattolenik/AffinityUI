using System;
using System.Collections.Generic;
using UnityEngine;

namespace AffinityUI
{
    public static class UI
    {
        static readonly Dictionary<string, Control> controlsById = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<Control, string> idsByControl = new Dictionary<Control, string>();

        internal static void RegisterID(Control control, string id)
        {
            if (controlsById.ContainsKey(id))
            {
                throw new InvalidOperationException(string.Format("ID '{0}' already taken by control of type '{1}'", id, control.GetType().Name));
            }
            controlsById[id] = control;
            idsByControl[control] = id;
        }

        public static TControl ByID<TControl>(string id) where TControl : Control
        {
            Control value;
            if (controlsById.TryGetValue(id, out value))
            {
                return value as TControl;
            }
            throw new KeyNotFoundException(string.Format("Could not find control with ID '{0}'", id));
        }

        public static string IdOf(Control control)
        {
            string value;
            if (idsByControl.TryGetValue(control, out value))
            {
                return value;
            }
            return null;
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