# AffinityUI
AffinityUI is an abstraction over the legacy GUI system for the Unity game engine. It provides a declarative layout model and rich data binding, with minimal overhead.

This complex UI:

![AffinityUI KSP example](http://i.imgur.com/KOzkbaj.gif)

Is generated only with this block of code:

    UI.GUILayout(this, new Window(new Rect(100, 100, 500, 500))
    .ID("window")
    .Title("Window Title")
    .DragTitlebar()
    .Skin(HighLogic.Skin)
    .Content(new TabControl()
        .AddPage("Page 1",
            new VerticalPanel()
            .Add(new Button("A Button")
                .Tooltip("tooltip!")
                    .Style(() => GUI.skin.textField).OK
                // This will print "A Button was clicked" on each click
                .OnClicked(s => print(s.Label() + " was clicked"))
                .Image(buttonIcon)
            )
            .Add(new PasswordField("Password")
                .ID("pw1")
                .Tooltip("Your secret's safe with me").OK
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
                .OnToggled((source, old, nw) => print(source.Label() + " is now " + nw))
            )
            .Add(new Button("Unity skin")
                .OnClicked(s => ui.ByID<Window>("window").Skin(null))
                .Skin(null, true)
            )
            .Add(new Button("KSP skin")
                .OnClicked(s => ui.ByID<Window>("window").Skin(HighLogic.Skin))
                .Skin(HighLogic.Skin, true)
            )
            .Add(new HorizontalPanel()
                .Add(new TextField("0")
                    .ID("sl1")
                    .LayoutOptions(GUILayout.MaxWidth(50))
                )
                .Add(new HorizontalSlider(0, 50)
                    .LayoutOptions(GUILayout.ExpandWidth(true))
                    .Value().BindTwoWay(
                        () => ui.ByID<TextField>("sl1").Text().SafeToFloat(),
                        (v) => ui.ByID<TextField>("sl1").Text(v.ToString("0.00"))
                    )
                )
            )
        )
        .AddPage("Page 2",
            new VerticalPanel()
            .Add(new Button("Another Button")
                .Image(buttonIcon)
            )
            .Add(new AutoSpace(50))
            .Add(new TextField("Text goes here"))
            .Add(new Toggle("Checkbox 1")
                .Visible().BindOneWay(() => Option1)
            )
        )
        .AddPage("Page 3",
            new ScrollView()
            .AlwaysShowHorizontal()
            .AlwaysShowVertical()
            .Add(new VerticalPanel()
                .ID("panel")
                .Add(new Button("This button adds controls")
                    .OnClicked(src => ui.ByID<VerticalPanel>("panel").Add(
                            new Button("Hello!").OnClicked(s => s.ParentAs<VerticalPanel>().Remove(s))
                        ))
                )
            )
        )
        .AddPage("Page 4",
            new TextArea()
        )
    )
    .Title().BindOneWay(() => "title is " + ui.ByID<PasswordField>("pw1").Password())

It's quite a bit of code, but a fraction of what you'd need to do it with raw Unity GUI, and also much, much easier to write and maintain.

Documentation is thin a the moment, but I'll improve it if I get some community feedback. I put together this project years ago but lost interest in game development as more pressing life issues took over. I'm re-releasing it with the KSP community in mind. If you think this is useful to you, please drop me a line!
