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

        public BindableProperty<SelectionGrid, int> SelectedProperty { get; private set; }

        public int Selected
        {
            get { return SelectedProperty.Value; }
            set { SelectedProperty.Value = value; }
        }

        public SelectionGrid()
        {
            Buttons = new List<BindableContent>();
            SelectedProperty = new BindableProperty<SelectionGrid, int>(this);
        }

        public SelectionGrid AddButton(String label)
        {
            Buttons.Add(new BindableContent { Label = label });
            return this;
        }

        public SelectionGrid AddButton(BindableContent content)
        {
            Buttons.Add(content);
            return this;
        }
    }
}