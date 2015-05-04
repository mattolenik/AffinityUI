using System;
using UnityEngine;

namespace AffinityUI
{
    public class Window : ControlBase<Window>
    {
        Control _content;
        readonly System.Random _rand = new System.Random();
        readonly int _id;
        Rect _windowRect;
        string _text;

        public int ID { get { return _id; }}

        protected Window() : base()
        {
            _id = 0;//_rand.Next();
            Self = this;
        }

        public static Window Create<TGuiTarget>(Rect windowRect, string text)
        {
            var result = new Window{ _windowRect = windowRect, _text = text };
            result.TargetType = typeof(TGuiTarget);
            if (result.TargetType != typeof(GUILayout) &&
                result.TargetType != typeof(GUI))
            {
                throw new ArgumentException("Generic argument must be type GUI or GUILayout ", "TGuiTarget");
            }
            return result;
        }

        public Window Content(Control content)
        {
            _content = content;
            _content.TargetType = TargetType;
            return this;
        }

        protected override void Layout_GUI()
        {
            _windowRect = GUI.Window(_id, _windowRect, windowFunc, _text);
        }

        protected override void Layout_GUILayout()
        {
            _windowRect = GUILayout.Window(_id, _windowRect, windowFunc, _text, LayoutOptions());
        }

        void windowFunc(int windowID)
        {
            _content.Layout();
        }
    }
}