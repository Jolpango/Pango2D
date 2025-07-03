# Pango2D

**Pango2D** is an upcoming lightweight 2D game library for MonoGame.

Built by [@jolpango](https://github.com/jolpango)

---

## Getting Started

### GameHost

The core of the Engine is the GameHost, found in Pango2D.Core. The GameHost manages services and the scenemanager.
To load up an instance of the engine, in your Game class

1. Add create a new GameHost.
2. Call its update and draw functions in Game.
3. In LoadContent you can load your initial scene with gamehost.LoadInitalScene(new YourScene())

### SceneManager

### SceneBase

This is the base scene class. Derive from it to create your own custom scenes.

#### ECSScene

This is subclass of SceneBase with a world(ECS), derive from this class to create your own. You must override the function ConfigureWorld and there you can configure the world. You cannot create the world before this since this is called after the scene is initialized with all necessary services.

#### UIScene

This is a subclass of SceneBase with a UIManager, derive from this class to create your own. You must override the function ConfigureUI with the provided uimanager. You cannot configure the UI before this since initalization logic must be called priot(which it is automatically)

#### HybridScene

This is a combination of ECSScene and UISCene with the same requirements from both of them.

### UIManager

This is a manager that automatically manages your UI. It manages updates and draw calls for all registerd elements.

### UIElement

The base UI class from which all other elements derives. Supports databinding to all attributes of primites, Color and Point.

#### UIStackPanel

Arranges its children in either vertical or horizontal layout with support for spacing of children, auto layouts if children changes.

#### UIButton

A simple buttonclass with background color, hover color and an OnClick event. OnClick can be databinded to.

#### UIView

The "bread and butter" of UI creation. Derive from this class to create your own UI view. This is combined with a corresponding xaml file. Reference to the file using the attribute [ViewPath("Path/view.xaml")]. Recomended to name them the same, for example View1.xaml.cs and View1.xaml. Create an instance of your view using UIView.Create<View1>(); and add it to a UIManager. To bind an attribute to a property use: "{Bind:PropertyName}", and to bind OnClick use OnClick="FunctionName". Does not support functions with parameters. Color is defined using "rbg(255, 255, 255)". See Demo/Views for examples. Every view has access to the services.
