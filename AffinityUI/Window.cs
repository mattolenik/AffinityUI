using System;
using UnityEngine;

namespace AffinityUI
{
    public class Window : TypedControl<Window>
    {
        readonly System.Random _rand = new System.Random();
        readonly int _id;
        Control _content;
        Rect _windowRect;
        Rect _dragArea;
        bool _autoDrag;

        BindableProperty<Window, string> _text;

        public int ID { get { return _id; }}

        protected Window() : base()
        {
            _id = _rand.Next();
            Style(() => GUI.skin.window);
            _text = new BindableProperty<Window, string>(this);
        }

        public Window(Rect windowRect) : this()
        {
            _windowRect = windowRect;
        }

        public Window Content(Control content)
        {
            _content = content;
            _content.Parent = this;
            _content.Context = Context;
            _content.SkinValue = SkinValue;
            return this;
        }

        public BindableProperty<Window, string> Title()
        {
            return _text;
        }

        public Window Title(string text)
        {
            _text.Value = text;
            return this;
        }

        public Window Drag(Rect dragArea)
        {
            _dragArea = dragArea;
            return this;
        }

        public Window Drag()
        {
            _autoDrag = true;
            return this;
        }

        public Window DragTitlebar(int height)
        {
            _dragArea = new Rect(0, 0, _windowRect.width, height);
            return this;
        }

        public Window DragTitlebar()
        {
            _dragArea = new Rect(0, 0, _windowRect.width, Style().border.top);
            return this;
        }

        protected internal override GUISkin SkinValue
        {
            get
            {
                return base.SkinValue;
            }
            set
            {
                base.SkinValue = value;
                if (_content != null)
                {
                    _content.SkinValue = value;
                }
            }
        }

        protected override void Layout_GUI()
        {
            _windowRect = GUI.Window(_id, _windowRect, windowFunc, _text);
        }

        protected override void Layout_GUILayout()
        {
            _windowRect = GUILayout.Window(_id, _windowRect, windowFunc, _text, Style(), LayoutOptions());
        }

        void windowFunc(int windowID)
        {
            if (_autoDrag)
            {
                GUI.DragWindow();
            }
            else
            {
                GUI.DragWindow(_dragArea);
            }
            _content.Layout();
        }

        protected internal override UIContext Context
        {
            get
            {
                return base.Context;
            }
            set
            {
                base.Context = value;
                if (_content != null)
                {
                    _content.Context = value;
                }
            }
        }
    }
}