using System;
using UnityEngine;

namespace AffinityUI
{
    public class AutoSpace : Control
    {
        BindableProperty<AutoSpace, int> size;

        public AutoSpace()
            : this(0)
        {
        }

        public AutoSpace(int size)
            : base()
        {
            this.size = new BindableProperty<AutoSpace, int>(this, size);
        }

        public AutoSpace Size(int size)
        {
            this.size.Value = size;
            return this;
        }

        public BindableProperty<AutoSpace, int> Size()
        {
            return size;
        }

        protected override void Layout_GUILayout()
        {
            if (size == 0)
            {
                GUILayout.FlexibleSpace();
            }
            else
            {
                GUILayout.Space(size);
            }
        }
    }
}