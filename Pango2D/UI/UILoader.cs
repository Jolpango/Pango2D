using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.UI.Elements;
using Pango2D.UI.Views;
using Pango2D.Utilities;

namespace Pango2D.UI
{
    public class UILoader
    {
        private readonly Dictionary<string, Func<XElement, object, UIElement>> builders = [];
        private readonly FontRegistry fontRegistry;

        public UILoader(FontRegistry fontRegistry)
        {
            builders.Add("Button", BuildButton);
            builders.Add("StackPanel", BuildStackPanel);
            this.fontRegistry = fontRegistry ?? throw new ArgumentNullException(nameof(fontRegistry));
        }
        public T LoadWithContext<T>(string filePath) where T : UIView, new()
        {
            var view = new T();
            var xmlContent = XDocument.Load(filePath);
            var rootNode = xmlContent.Root;

            var rootElement = BuildRecursiveWithContext(rootNode, view);
            view.RootElement = rootElement;
            return view;
        }

        private UIElement BuildRecursiveWithContext(XElement element, object context)
        {
            if (!builders.TryGetValue(element.Name.LocalName, out var builder))
                throw new InvalidOperationException($"No builder registered for element type '{element.Name.LocalName}'");

            var uiElement = builder(element, context);

            foreach (var xChild in element.Elements())
            {
                var childElement = BuildRecursiveWithContext(xChild, context);
                uiElement.AddChild(childElement);
            }

            return uiElement;
        }

        private void ApplyAttributes(UIElement uiElement, XElement xElement, object bindingContext)
        {
            foreach (var attr in xElement.Attributes())
            {
                var value = attr.Value;

                if (value.StartsWith("{Bind:"))
                {
                    string propName = value[6..^1]; // strip {Bind:...}
                    var member = bindingContext.GetType().GetField(propName)
                                 ?? (MemberInfo)bindingContext.GetType().GetProperty(propName);

                    if (member != null)
                    {
                        Func<object> getter = member switch
                        {
                            FieldInfo fi => () => fi.GetValue(bindingContext),
                            PropertyInfo pi => () => pi.GetValue(bindingContext),
                            _ => () => null
                        };

                        uiElement.AddBinding(attr.Name.LocalName, getter);
                        SetValue(uiElement, attr.Name.LocalName, getter());
                    }
                }
                else if (attr.Name == "OnClick" && uiElement is UIButton btn)
                {
                    var method = bindingContext.GetType().GetMethod(attr.Value);
                    if (method != null)
                        btn.OnClick += () => method.Invoke(bindingContext, null);
                }
                else
                {
                    // Static property assignment
                    SetValue(uiElement, attr.Name.LocalName, value);
                }
            }
        }

        private UIButton BuildButton(XElement xButton, object context)
        {
            UIButton button = new();
            ApplyAttributes(button, xButton, context);
            return button;
        }

        private UIStackPanel BuildStackPanel(XElement xPanel, object context)
        {
            UIStackPanel panel = new();
            ApplyAttributes(panel, xPanel, context);
            return panel;
        }

        private void SetValue(object obj, string propertyName, object rawValue)
        {
            var prop = obj.GetType().GetProperty(propertyName);
            if (prop == null || !prop.CanWrite)
                return;

            object parsed = rawValue;

            // Handle common types
            if (rawValue is string str)
            {
                if (prop.PropertyType == typeof(Color))
                    parsed = ParseColor(str);
                else if (prop.PropertyType == typeof(Point))
                    parsed = ParsePoint(str);
                else if (prop.PropertyType == typeof(int))
                    parsed = int.TryParse(str, out var i) ? i : 0;
                else if (prop.PropertyType == typeof(float))
                    parsed = float.TryParse(str, out var f) ? f : 0f;
                else if (prop.PropertyType.IsEnum)
                    parsed = Enum.Parse(prop.PropertyType, str, ignoreCase: true);
                else if (prop.PropertyType == typeof(SpriteFont))
                    parsed = fontRegistry.Get(str) ?? fontRegistry.Get("DefaultFont");
                // Add more types as needed
            }

            prop.SetValue(obj, parsed);
        }

        private static Color ParseColor(string str)
        {
            return StringTranslator.ColorFromRGB(str);
        }
        
        private static Point ParsePoint(string str)
        {
            return StringTranslator.PointFromString(str);
        }
    }
}
