using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using Pango2D.Extensions;
using Pango2D.Input.Contracts;
using System;
using System.Collections.Generic;

namespace Pango2D.UI
{
    public class UIManager
    {
        private List<UIElement> rootElements = new List<UIElement>();
        private Dictionary<string, UIElement> registry = new Dictionary<string, UIElement>();

        public UIElement this[string id] => GetElementById(id);
        public void AddRootElement(UIElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            if (!rootElements.Contains(element))
            {
                rootElements.Add(element);
                RegisterRecursive(element);
            }
        }

        public void RemoveRootElement(UIElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));
            if (rootElements.Contains(element))
            {
                rootElements.Remove(element);
                UnregisterRecursive(element);
            }
        }
        public UIElement GetElementById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            return registry.TryGetValue(id, out var element) ? element : null;
        }

        public void Update(GameTime time, IInputProvider input)
        {
            foreach (var element in rootElements)
            {
                element.UpdateLayoutIfDirty();
                element.Update(time, input);
            }
        }

        public void Draw(SpriteBatch spriteBatch, RenderPassSettings settings)
        {
            spriteBatch.Begin(settings);
            foreach (var element in rootElements)
            {
                element.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void RegisterRecursive(UIElement element)
        {
            if (element is null) return;
            if (!string.IsNullOrEmpty(element.Id))
                registry[element.Id] = element;
            foreach (var child in element.Children)
            {
                RegisterRecursive(child);
            }
        }
        private void UnregisterRecursive(UIElement element)
        {
            if (element is null) return;
            if (!string.IsNullOrEmpty(element.Id) && registry.ContainsKey(element.Id))
                registry.Remove(element.Id);
            foreach (var child in element.Children)
            {
                UnregisterRecursive(child);
            }
        }
    }
}
