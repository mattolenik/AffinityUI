using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    public class SelectionGrid : TypedControl<SelectionGrid>
    {
        HashSet<BindableContent<SelectionGrid>> _buttons;

        BindableProperty<SelectionGrid, int> _selected;

        BindableProperty<SelectionGrid, int> _xCount;

        bool autoXCount = true;

        GUIContent[] buttonContents;

        public BindableProperty<SelectionGrid, int> Selected()
        {
            return _selected;
        }

        public SelectionGrid Selected(int index)
        {
            _selected.Value = index;
            return this;
        }

        public SelectionGrid() : base()
        {
            _buttons = new HashSet<BindableContent<SelectionGrid>>();
            _selected = new BindableProperty<SelectionGrid, int>(this);
            _xCount = new BindableProperty<SelectionGrid, int>(this);
            _xCount.OnPropertyChanged((source, old, nw) => autoXCount = false);
            Style(() => GUI.skin.button);
        }

        public BindableProperty<SelectionGrid, int> XCount()
        {
            return _xCount;
        }

        public SelectionGrid XCount(int xCount)
        {
            _xCount.Value = xCount;
            return this;
        }

        public SelectionGrid Add(string label)
        {
            return Add(new BindableContent<SelectionGrid>(this).Label(label));
        }

        public SelectionGrid Add(BindableContent<SelectionGrid> content)
        {
            _buttons.Add(content);
            // GUILayout.SelectionGrid requires an array of GUIContent
            buttonContents = _buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Remove(BindableContent<SelectionGrid> content)
        {
            _buttons.Remove(content);
            buttonContents = _buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Remove(string label)
        {
            _buttons.RemoveWhere(x => x.Label() == label);
            buttonContents = _buttons.Select(x => x.Content()).ToArray();
            return this;
        }

        public SelectionGrid Clear()
        {
            _buttons.Clear();
            buttonContents = new GUIContent[]{ };
            return this;
        }

        protected override void Layout_GUILayout()
        {
            Selected(GUILayout.SelectionGrid(Selected(), buttonContents, autoXCount ? _buttons.Count : _xCount, Style(), LayoutOptions()));
        }
    }
}