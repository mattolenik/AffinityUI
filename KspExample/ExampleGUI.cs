using UnityEngine;
using AffinityUI;
using KSP;
using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Reflection;

namespace KspExample
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class Starter : MonoBehaviour
    {
        protected void Start()
        {
            gameObject.AddComponent<TooltipRenderer>();
            gameObject.AddComponent<ExampleGUI>();
        }
    }

    public class ExampleGUI : MonoBehaviour
    {
        UIContext gui;

        bool Option1 { get; set; }

        Texture2D buttonIcon = GameDatabase.Instance.GetTexture("AffinityUIExample/icon", false);

        void OnGUI()
        {
            GUI.skin = HighLogic.Skin;
            GUI.depth = 100;

            if (gui == null)
            {
                gui = UI.GUILayout(this,
                    new Window(new Rect(100, 100, 500, 500))
                    .Title("Window Title")
                    .DragTitlebar()
                    .Content(
                        new TabControl()
                        .AddPage(
                            "Page 1",
                            new VerticalPanel()
                            .Add(new Button("A Button").Tooltip("tooltip!")
                                 // This will print "A Button was clicked" on each click
                                .OnClicked(s => print(s.Label() + " was clicked"))
                                .Image(buttonIcon)
                            )
                            .Add(new PasswordField("Password").ID("pw1").Tooltip("Your secret's safe with me"))
                            .Add(new Toggle("Checkbox 1")
                                 // Bind the value of the checkbox to the Option1 variable
                                .IsChecked().BindTwoWay(() => Option1, v => Option1 = v)
                                .OnToggled((source, old, nw) => print(source.Label() + " is now " + nw))
                                 // Binding to the visible property makes this control only visible
                                 // when Option1 is true, letting us show/hide it using the Toggle control below
                                .Visible().BindOneWay(() => Option1)
                            )
                            .Add(new Toggle("Toggle hidden options")
                                 // Bind the value of the checkbox to the Option1 variable
                                .IsChecked().BindTwoWay(() => Option1, v => Option1 = v)
                                 // Print to the console each time the value changes
                                .OnToggled((source, old, nw) => print(source.Label() + " is now " + nw)))
                        )
                        .AddPage(
                            "Page 2",
                            new VerticalPanel()
                            .Add(new Button("Another Button")
                                .Image(buttonIcon)
                            )
                            .Add(new Toggle("Checkbox 1")
                                .Visible().BindOneWay(() => Option1)
                            )
                        )
                        .AddPage(
                            "Page 3",
                            new VerticalPanel()
                            //.ID("panel")
                           // .Add(new Button("This button adds controls")
                           //     .OnClicked(source => Control.ByID<VerticalPanel>("panel").Add(new Button("Hello!")))
                           // )
                        )
                        .AddPage(
                            "Page 4",
                            new TextArea()
                        )
                    ).Title().BindOneWay(()=>"title is " + UI.ByID<PasswordField>("pw1").Password())
                );
            }
            gui.OnGUI();
        }
    }
}