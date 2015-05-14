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
        UI ui;

        bool Option1 { get; set; }

        Texture2D buttonIcon = GameDatabase.Instance.GetTexture("AffinityUIExample/icon", false);

        void OnGUI()
        {
            GUI.depth = 20;

            if (ui == null)
            {
                ui = BuildUI();
            }
            ui.OnGUI();
        }

        UI BuildUI()
        {
            return
                UI.GUILayout(this, new Window(new Rect(100, 100, 500, 500))
                .ID("window")
                .Title("Window Title")
                .DragTitlebar()
                .Skin(HighLogic.Skin)
                .Content(
                    new TabControl()
                    .AddPage("Page 1",
                        new VerticalPanel()
                        .Add(new Button("A Button")
                            .Tooltip("tooltip!")
                            .Style(() => GUI.skin.textField)._
                            // This will print "A Button was clicked" on each click
                            .OnClicked(s => print(s.Label() + " was clicked"))
                            .Image(buttonIcon)
                        )
                        .Add(new PasswordField("Password")
                            .ID("pw1")
                            .Tooltip("Your secret's safe with me")._
                        )
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
                        .Add(
                            new Button("Unity skin")
                            .OnClicked(s => ui.ByID<Window>("window").Skin(null))
                            .Skin(null, true))
                        .Add(
                            new Button("KSP skin")
                            .OnClicked(s => ui.ByID<Window>("window").Skin(HighLogic.Skin))
                            .Skin(HighLogic.Skin, true)
                        )
                    )
                    .AddPage("Page 2",
                        new VerticalPanel()
                        .Add(new Button("Another Button")
                            .Image(buttonIcon)
                        )
                        .Add(new TextField("Text goes here"))
                        .Add(new Toggle("Checkbox 1")
                            .Visible().BindOneWay(() => Option1)
                        )
                    )
                    .AddPage("Page 3",
                        new VerticalPanel()
                        .ID("panel")
                        .Add(new Button("This button adds controls")
                            .OnClicked(src => ui.ByID<VerticalPanel>("panel").Add(
                                new Button("Hello!").OnClicked(s => s.ParentAs<VerticalPanel>().Remove(s))
                            ))
                        )
                    )
                    .AddPage("Page 4",
                        new TextArea()
                    )
                )
                .Title().BindOneWay(()=>"title is " + ui.ByID<PasswordField>("pw1").Password())
            );
        }
    }
}