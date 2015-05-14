using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// A control that displays its children as tabs.
    /// </summary>
    public class TabControl : TypedControl<TabControl>
    {
        SelectionGrid tabButtons;
        Dictionary<int, Control> pageMap = new Dictionary<int, Control>();
        int currentPage = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        public TabControl()
            : base()
        {
            tabButtons = new SelectionGrid();
            tabButtons.Selected().OnPropertyChanged((source, old, nw) => ShowTab(nw));
            tabButtons.Context = Context;
            tabButtons.Parent = this;
        }

        /// <summary>
        /// Adds a tab page.
        /// </summary>
        /// <param name="name">The name of the page.</param>
        /// <param name="page">The page contents.</param>
        /// <returns>this instance</returns>
        public TabControl AddPage(string name, Control page)
        {
            tabButtons.Add(new BindableContent<SelectionGrid>(tabButtons).Label(name));
            pageMap.Add(pageMap.Count, page);
            page.Parent = this;
            page.Context = Context;
            if (!page.IndependantSkin)
            {
                page.SkinValue = SkinValue;
            }
            return this;
        }

        /// <summary>
        /// Shows a tab by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>this control</returns>
        public TabControl ShowTab(int index)
        {
            currentPage = index;
            tabButtons.Selected().SetIgnoreBinding(index);
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
                foreach (var page in pageMap.Values)
                {
                    if (!page.IndependantSkin)
                    {
                        page.SkinValue = value;
                    }
                }
                if (!tabButtons.IndependantSkin)
                {
                    tabButtons.SkinValue = value;
                }
            }
        }

        protected override void Layout_GUILayout()
        {
            GUILayout.BeginVertical(LayoutOptions());
            tabButtons.Layout();
            pageMap[currentPage].Layout();
            GUILayout.EndVertical();
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
                foreach (var page in pageMap.Values)
                {
                    page.Context = value;
                }
                tabButtons.Context = value;
            }
        }
    }
}