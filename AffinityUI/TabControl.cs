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
    public class TabControl : ControlBase<TabControl>
    {
        SelectionGrid tabButtons = new SelectionGrid();

        Dictionary<int, Control> pageMap = new Dictionary<int, Control>();

        int currentPage = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        public TabControl() : base()
        {
            Self = this;
            tabButtons.Selected().OnPropertyChanged((source, old, nw) => ShowTab(nw));
        }

        /// <summary>
        /// Adds a tab page.
        /// </summary>
        /// <param name="name">The name of the page.</param>
        /// <param name="page">The page contents.</param>
        /// <returns>this instance</returns>
        public TabControl AddPage(String name, Control page)
        {
            tabButtons.AddButton(new BindableContent().Label(name));
            pageMap.Add(pageMap.Count, page);
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

        protected override void Layout_GUILayout()
        {
            GUILayout.BeginVertical(LayoutOptions());
            tabButtons.Layout();
            pageMap[currentPage].Layout();
            GUILayout.EndVertical();
        }

        internal protected override Type TargetType
        {
            get
            {
                return base.TargetType;
            }
            set
            {
                base.TargetType = value;
                foreach (var page in pageMap.Values)
                {
                    page.TargetType = value;
                }
                tabButtons.TargetType = value;
            }
        }
    }
}