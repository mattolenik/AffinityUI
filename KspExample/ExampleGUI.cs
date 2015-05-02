using UnityEngine;
using AffinityUI;

namespace KspExample
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class Starter : MonoBehaviour
    {
        protected void Start()
        {
            gameObject.AddComponent<ExampleGUI>();
        }
    }

    public class ExampleGUI : MonoBehaviour
    {
        Control gui;

        bool Option1 { get; set; }

        Texture2D buttonIcon = GameDatabase.Instance.GetTexture("AffinityUIExample/icon", false);

        void OnGUI()
        {
            if(gui == null)
            {
                //var gui = Window.Create<GUILayout>(new Rect(100, 100, 400, 400), "window test")
                //.SetContent(
                gui = Composite.Create<GUILayout, Area>()
                    .SetDimensions(new Rect(20, 20, 200, 200))
                    .Add(new Button("A Button")
                        // This will print "A Button was clicked" on each click
                        .OnClicked(s => Debug.Log(s.Label + " was clicked"))
                    )
                    .Add(new Toggle("Checkbox 1")
                        // Bind the value of the checkbox to the Option1 variable
                        .IsCheckedProperty.BindTwoWay(() => Option1, v => Option1 = v)
                        // Binding to the visible property makes this control only visible
                        // when Option1 is true, letting us show/hide it using the Toggle control below
                        .VisibleProperty.BindOneWay(() => Option1)
                    )
                    .Add(new Toggle("Checkbox 2")
                        // Bind the value of the checkbox to the Option1 variable
                        .IsCheckedProperty.BindTwoWay(() => Option1, v => Option1 = v)
                        // Print to the console each time the value changes
                        .OnToggled((source, old, @new) => Debug.Log(source.Label + " is now " + @new))
                    )
                    .Add(new Box().SetLabel("sadf"));//);
            }
            gui.Layout();
        }
    }
}