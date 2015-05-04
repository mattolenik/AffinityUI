using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    public class SelectionGrid : ControlBase<SelectionGrid>
    {
        public IList<BindableContent> Buttons { get; private set; }

        BindableProperty<SelectionGrid, int> _selected;

        public BindableProperty<SelectionGrid, int> Selected()
        {
            return _selected;
        }

        public SelectionGrid Selected(int index)
        {
            _selected.Value = index;
            return this;
        }

        public SelectionGrid()
        {
            Buttons = new List<BindableContent>();
            _selected = new BindableProperty<SelectionGrid, int>(this);
        }

        public SelectionGrid AddButton(String label)
        {
            Buttons.Add(new BindableContent().Label(label));
            return this;
        }

        public SelectionGrid AddButton(BindableContent content)
        {
            Buttons.Add(content);
            return this;
        }
    }
}