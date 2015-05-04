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
    public class TabControl : Composite
    {
        class DefaultComposite : Composite { }

        SelectionGrid tabButtons = new SelectionGrid();

        Composite pages = new DefaultComposite();

        Dictionary<int, Control> pageMap = new Dictionary<int, Control>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        public TabControl()
        {
            Add(tabButtons);
            Add(pages);
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
            tabButtons.Selected().OnPropertyChanged((source, old, nw) => ShowTab(nw));
            pageMap.Add(pageMap.Count, page);
            pages.Add(page);
            page.Visible.Value = false;

            // Show the first page
            if (pages.Children.Count == 1)
            {
                ShowTab(0);
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
            foreach (var page in pages)
            {
                page.Visible.Value = false;
            }
            pageMap[index].Visible.Value = true;
            tabButtons.Selected().SetIgnoreBinding(index);
            return this;
        }
    }
}