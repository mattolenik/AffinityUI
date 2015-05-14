using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    public class SelectionGrid : TypedControl<SelectionGrid>
    {
        HashSet<BindableContent<SelectionGrid>> buttons;
        BindableProperty<SelectionGrid, int> selected;
        BindableProperty<SelectionGrid, int> xCount;
        bool autoXCount = true;
        GUIContent[] buttonContents;

        public BindableProperty<SelectionGrid, int> Selected()
        {
            return selected;
        }

        public SelectionGrid Selected(int index)
        {
            selected.Value = index;
            return this;
        }

        public SelectionGrid()
            : base()
        {
            buttons = new HashSet<BindableContent<SelectionGrid>>();
            selected = new BindableProperty<SelectionGrid, int>(this);
            xCount = new BindableProperty<SelectionGrid, int>(this);
            xCount.OnPropertyChanged((source, old, nw) => autoXCount = false);
            Style(() => GUI.skin.button);
        }

        public BindableProperty<SelectionGrid, int> XCount()
        {
            return xCount;
        }

        public SelectionGrid XCount(int xCount)
        {
            this.xCount.Value = xCount;
            return this;
        }

        public SelectionGrid Add(string label)
        {
            return Add(new BindableContent<SelectionGrid>(this).Label(label));
        }

        public SelectionGrid Add(BindableContent<SelectionGrid> content)
        {
            buttons.Add(content);
            // GUILayout.SelectionGrid requires an array of GUIContent
            buttonContents = buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Remove(BindableContent<SelectionGrid> content)
        {
            buttons.Remove(content);
            buttonContents = buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Remove(string label)
        {
            buttons.RemoveWhere(x => x.Label() == label);
            buttonContents = buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Clear()
        {
            buttons.Clear();
            buttonContents = new GUIContent[]{ };
            return this;
        }

        protected override void Layout_GUILayout()
        {
            Selected(GUILayout.SelectionGrid(Selected(), buttonContents, autoXCount ? buttons.Count : xCount, Style(), LayoutOptions()));
        }
    }
}