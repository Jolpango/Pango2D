using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS
{
    public enum RenderPhase
    {
        World,
        UI,
        Debug
    }
    public class RenderPassRegistry
    {
        private readonly Dictionary<RenderPhase, RenderPassSettings> settings = new();

        public RenderPassRegistry()
        {
            settings[RenderPhase.World] = new RenderPassSettings();
            settings[RenderPhase.UI] = new RenderPassSettings();
            settings[RenderPhase.Debug] = new RenderPassSettings();
        }

        public void Set(RenderPhase phase, RenderPassSettings renderPassSettings)
        {
            settings[phase] = renderPassSettings;
        }

        public RenderPassSettings Get(RenderPhase phase)
        {
            if (settings.TryGetValue(phase, out var renderPassSettings))
            {
                return renderPassSettings;
            }
            throw new KeyNotFoundException($"Render pass settings for {phase} not found.");
        }

        public RenderPassSettings this[RenderPhase phase]
        {
            get => Get(phase);
            set => Set(phase, value);
        }
    }
}
