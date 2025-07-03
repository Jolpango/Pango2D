using Pango2D.Graphics.Particles.Modifiers;

namespace Editor.Controls.Modifiers
{
    public partial class ScaleModifierControl : UserControl
    {
        public ScaleModifier ScaleModifier { get; set; } = new ScaleModifier([]);
        public ScaleModifierControl()
        {
            InitializeComponent();
        }

        private void ScaleModifierControl_Load(object sender, EventArgs e)
        {
            InterpolationTypeControl.ParticleModifier = ScaleModifier;
            FloatKeyFrameControl.OnKeyframesChanged += FloatKeyFrameControl_OnKeyframesChanged;
        }

        private void FloatKeyFrameControl_OnKeyframesChanged()
        {
            ScaleModifier.Keyframes.Clear();
            var keyframes = FloatKeyFrameControl.GetScaleKeyframes();
            ScaleModifier.Keyframes.AddRange(keyframes.OrderBy(k => k.Time));
        }

        private void InterpolationTypeControl_Load(object sender, EventArgs e)
        {

        }
    }
}
