using System;
using UnityEngine;

namespace AffinityUI
{
    public class Window : TypedControl<Window>
    {
        readonly System.Random rand = new System.Random();
        readonly int id;
        Control content;
        Rect windowRect;
        Rect dragArea;
        bool autoDrag;
        BindableProperty<Window, string> text;

        public int ID { get { return id; } }

        protected Window()
            : base()
        {
            id = rand.Next();
            Style(() => GUI.skin.window);
            text = new BindableProperty<Window, string>(this);
        }

        public Window(Rect windowRect)
            : this()
        {
            this.windowRect = windowRect;
        }

        public Window Content(Control content)
        {
            this.content = content;
            content.Parent = this;
            content.Context = Context;
            content.SkinValue = SkinValue;
            return this;
        }

        public BindableProperty<Window, string> Title()
        {
            return text;
        }

        public Window Title(string text)
        {
            this.text.Value = text;
            return this;
        }

        public Window Drag(Rect dragArea)
        {
            this.dragArea = dragArea;
            return this;
        }

        public Window Drag()
        {
            autoDrag = true;
            return this;
        }

        public Window DragTitlebar(int height)
        {
            dragArea = new Rect(0, 0, windowRect.width, height);
            return this;
        }

        public Window DragTitlebar()
        {
            dragArea = new Rect(0, 0, windowRect.width, Style().border.top);
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
                if (content != null)
                {
                    content.SkinValue = value;
                }
            }
        }

        protected override void Layout_GUI()
        {
            windowRect = GUI.Window(id, windowRect, windowFunc, text);
        }

        protected override void Layout_GUILayout()
        {
            windowRect = GUILayout.Window(id, windowRect, windowFunc, text, Style(), LayoutOptions());
        }

        void windowFunc(int windowID)
        {
            if (autoDrag)
            {
                GUI.DragWindow();
            }
            else
            {
                GUI.DragWindow(dragArea);
            }
            content.Layout();
        }

        public override UI Context
        {
            get
            {
                return base.Context;
            }
            set
            {
                base.Context = value;
                if (content != null)
                {
                    content.Context = value;
                }
            }
        }
    }
}