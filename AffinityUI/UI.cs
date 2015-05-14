using System;
using System.Collections.Generic;
using UnityEngine;

namespace AffinityUI
{
    public class UI
    {
        readonly Dictionary<string, Control> controlsById = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<Control, string> idsByControl = new Dictionary<Control, string>();

        internal LayoutTarget Layout { get; set; }

        internal MonoBehaviour Owner { get; set; }

        internal Control Content { get; set; }

        internal void RegisterID(Control control, string id)
        {
            controlsById[id] = control;
            idsByControl[control] = id;
        }

        public TControl ByID<TControl>(string id) where TControl : Control
        {
            Control value;
            if (controlsById.TryGetValue(id, out value))
            {
                return value as TControl;
            }
            throw new KeyNotFoundException(string.Format("Could not find control with ID '{0}'", id));
        }

        public string IdOf(Control control)
        {
            string value;
            if (idsByControl.TryGetValue(control, out value))
            {
                return value;
            }
            return null;
        }

        public static UI GUI(MonoBehaviour owner, Control content)
        {
            var ui = new UI{ Layout = LayoutTarget.GUI, Owner = owner, Content = content };
            content.Context = ui;
            return ui;
        }

        public static UI GUILayout(MonoBehaviour owner, Control content)
        {
            var ui = new UI { Layout = LayoutTarget.GUILayout, Owner = owner, Content = content };
            content.Context = ui;
            return ui;
        }

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