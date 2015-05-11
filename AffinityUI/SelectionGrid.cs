using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    public class SelectionGrid : TypedControl<SelectionGrid>
    {
        public IList<BindableContent<SelectionGrid>> Buttons { get; private set; }

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
            Buttons = new List<BindableContent<SelectionGrid>>();
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

        public SelectionGrid AddButton(string label)
        {
            return AddButton(new BindableContent<SelectionGrid>(this).Label(label));
        }

        public SelectionGrid AddButton(BindableContent<SelectionGrid> content)
        {
            Buttons.Add(content);
            buttonContents = Buttons.Select(x => x.Content).ToArray();
            return this;
        }

        protected override void Layout_GUILayout()
        {
            Selected(GUILayout.SelectionGrid(Selected(), buttonContents, autoXCount ? Buttons.Count : _xCount, Style(), LayoutOptions()));
        }
    }
}