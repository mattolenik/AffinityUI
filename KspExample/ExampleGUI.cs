using UnityEngine;
using AffinityUI;
using KSP;
using System.Runtime.InteropServices;
using System.Threading;

namespace KspExample
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class Starter : MonoBehaviour
    {
        protected void Start()
        {
            if (gameObject.GetComponent<ExampleGUI>() == null)
            {
                gameObject.AddComponent<ExampleGUI>();
            }
        }
    }

    public class ExampleGUI : MonoBehaviour
    {
        Control gui;

        bool Option1 { get; set; }

        Texture2D buttonIcon = GameDatabase.Instance.GetTexture("AffinityUIExample/icon", false);

        void OnGUI()
        {
            GUI.skin = HighLogic.Skin;
            if (gui == null)
            {
                gui = Window.Create<GUILayout>(new Rect(100, 100, 120, 50), "window test")
                    .Content(Composite.Create<GUILayout, VerticalPanel>()
                        .Add(new Button("A Button")
                                         // This will print "A Button was clicked" on each click
                            .OnClicked(s => print(s.Label() + " was clicked"))
                            .Image(buttonIcon)
                                     )
                        .Add(new Toggle("Checkbox 1")
                                         // Bind the value of the checkbox to the Option1 variable
                            .IsChecked().BindTwoWay(() => Option1, v => Option1 = v)
                            .OnToggled((source, old, @new) => print(source.Label() + " is now " + @new))
                                         // Binding to the visible property makes this control only visible
                                         // when Option1 is true, letting us show/hide it using the Toggle control below
                            .Visible.BindOneWay(() => Option1)
                                     )
                        .Add(new PasswordField("Password"))
                        .Add(new Toggle("Checkbox 2")
                                         // Bind the value of the checkbox to the Option1 variable
                             .IsChecked().BindTwoWay(() => Option1, v => Option1 = v)
                                         // Print to the console each time the value changes
                            .OnToggled((source, old, @new) => print(source.Label() + " is now " + @new))
                     ));
            }
            gui.Layout();
        }
    }
}