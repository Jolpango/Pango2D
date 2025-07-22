using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Controls
{
    public partial class InterpolationTypeControl : UserControl
    {
        public IParticleModifier? ParticleModifier { get; set; }
        public InterpolationTypeControl()
        {
            InitializeComponent();
            InterpolationComboBox.Items.Add("Linear");
            InterpolationComboBox.Items.Add("EaseIn");
            InterpolationComboBox.SelectedIndex = 0; // Default to Linear
        }

        private void InterpolationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParticleModifier is null) return;
            if (InterpolationComboBox.SelectedItem is null) return;
            switch (InterpolationComboBox.SelectedItem.ToString())
            {
                case "Linear":
                    ParticleModifier.Interpolator = new LinearInterpolator();
                    break;
                case "EaseIn":
                    ParticleModifier.Interpolator = new EaseInInterpolator();
                    break;
                default:
                    throw new NotSupportedException("Unsupported interpolation type selected.");
            }
        }
    }
}
