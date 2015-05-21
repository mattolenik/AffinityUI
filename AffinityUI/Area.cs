using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AffinityUI
{
    /// <summary>
    /// Contains a <see cref="GUILayout"/> block in a fixed screen area.
    /// </summary>
    public class Area : Composite<Area>
    {
        Rect dimensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        /// <param name="dimensions">The dimensions.</param>
        public Area(Rect dimensions)
            : base()
        {
            this.dimensions = dimensions;
        }

        public Area Dimensions(Rect dimensions)
        {
            this.dimensions = dimensions;
            return this;
        }

        public Rect Dimensions()
        {
            return dimensions;
        }

        /// <summary>
        /// Called when layout begins when using GUILayout.
        /// </summary>
        protected override void OnBeginLayout_GUILayout()
        {
            GUILayout.BeginArea(dimensions, Label());
        }

        /// <summary>
        /// Called when layout ends when using GUILayout.
        /// </summary>
        protected override void OnEndLayout_GUILayout()
        {
            GUILayout.EndArea();
        }
    }
}
