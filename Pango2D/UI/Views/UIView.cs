using Pango2D.Core.Graphics;
using Pango2D.Core.Services;
using Pango2D.UI.Elements;
using System.Reflection;

namespace Pango2D.UI.Views
{
    /// <summary>
    /// Base class for all UI views.
    /// Updates and draw  calls for this base class are managed by the UIManager.
    /// </summary>
    [ViewPath("UIView.xaml")]

    public abstract class UIView
    {
        /// <summary>
        /// The root element of the view.
        /// </summary>
        public UIElement RootElement { get; set; }
        public GameServices Services { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the view's root element.
        /// </summary>
        public bool IsVisible
        {
            get => RootElement?.IsVisible ?? false;
            set
            {
                if (RootElement is not null)
                {
                    RootElement.IsVisible = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the enabled state of the view's root element.
        /// </summary>
        public bool IsEnabled
        {
            get => RootElement?.IsEnabled ?? false;
            set
            {
                if (RootElement is not null)
                {
                    RootElement.IsEnabled = value;
                }
            }
        }

        /// <summary>
        /// Called when the view is loaded. This method can be overridden to perform initialization tasks.
        /// </summary>
        public virtual void OnLoaded() { }

        /// <summary>
        /// Hides the view by setting its root element's visibility to false. Prevents it from being drawn.
        /// </summary>
        public void Hide()
        {
            if (RootElement is not null)
            {
                RootElement.IsVisible = false;
            }
        }

        /// <summary>
        /// Shows the view by setting its root element's visibility to true. Allows it to be drawn.
        /// </summary>
        public void Show()
        {
            if (RootElement is not null)
            {
                RootElement.IsVisible = true;
            }
        }

        /// <summary>
        /// Toggles the visibility of the view's root element.
        /// </summary>
        public void ToggleVisibility()
        {
            if (RootElement is not null)
            {
                RootElement.IsVisible = !RootElement.IsVisible;
            }
        }

        /// <summary>
        /// Disables the view by setting its root element's IsEnabled property to false.
        /// </summary>
        public void Disable()
        {
            if (RootElement is not null)
            {
                RootElement.IsEnabled = false;
            }
        }

        /// <summary>
        /// Enables the view by setting its root element's IsEnabled property to true.
        /// </summary>
        public void Enable()
        {
            if (RootElement is not null)
            {
                RootElement.IsEnabled = true;
            }
        }

        /// <summary>
        /// Toggles the enabled state of the view's root element.
        /// </summary>
        public void ToggleEnabled()
        {
            if (RootElement is not null)
            {
                RootElement.IsEnabled = !RootElement.IsEnabled;
            }
        }

        /// <summary>
        /// Creates a new instance of the specified view type, loading it from the default path based on its type name or sing a custom path defined by the ViewPathAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T Create<T>(GameServices services) where T : UIView, new()
        {
            var loader = new UILoader(services.Get<FontRegistry>());
            var view = loader.LoadWithContext<T>(GetViewPath<T>());
            view.Services = services;
            view.OnLoaded();
            return view;
        }
        private static string GetViewPath<T>() where T : UIView
        {
            var type = typeof(T);

            var attr = type.GetCustomAttribute<ViewPathAttribute>();
            if (attr != null)
                return attr.Path;

            return $"Content/UI/Views/{type.Name}.xaml";
        }
    }
}
