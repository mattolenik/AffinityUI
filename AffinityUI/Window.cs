using System;
using UnityEngine;

namespace AffinityUI
{
    public class Window : Control
    {
        Control _content;
        static readonly System.Random _rand;
        readonly int _id;
        Rect _dimensions;
        string _text;

        public int ID { get { return _id; }}

        public Window()
        {
            _id = _rand.Next();
        }

        public static Window Create<TGuiTarget>(Rect dimensions, string text)
        {
            var result = new Window{ _dimensions = dimensions, _text = text };
            result.TargetType = typeof(TGuiTarget);
            if (result.TargetType != typeof(GUILayout) &&
                result.TargetType != typeof(GUI))
            {
                throw new ArgumentException("Generic argument must be type GUI or GUILayout ", "TGuiTarget");
            }
            return result;
        }

        public Window SetContent(Control content)
        {
            _content = content;
            return this;
        }

        protected override void Layout_GUI()
        {
        }

        protected override void Layout_GUILayout()
        {
            GUILayout.Window(_id, _dimensions, windowFunc, _text);
        }

        void windowFunc(int windowID)
        {
            _content.Layout();
        }
    }
}