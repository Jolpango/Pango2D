using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Modifiers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Controls.Modifiers
{
    public partial class OpacityModifierControl : UserControl
    {
        public OpacityModifier OpacityModifier { get; set; } = new OpacityModifier([]);
        public OpacityModifierControl()
        {
            InitializeComponent();
        }

        private void OpacityModifierControl_Load(object sender, EventArgs e)
        {
            InterpolationControl.ParticleModifier = OpacityModifier;
            KeyFramesControl.OnKeyframesChanged += KeyFramesControl_OnKeyframesChanged;
        }

        private void KeyFramesControl_OnKeyframesChanged()
        {
            var keyframes = KeyFramesControl.GetOpacityKeyframes();
            OpacityModifier.KeyFrames.Clear();
            OpacityModifier.KeyFrames.AddRange(keyframes.OrderBy(k => k.Time));
        }
    }
}
