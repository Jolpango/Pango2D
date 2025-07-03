using Pango2D.Graphics.Particles.Dispersion;

namespace Editor.Controls.Dispersion
{
    public partial class RandomDispersionControl : UserControl
    {
        public RandomDispersion Dispersion { get; set; } = new(50, 100);
        public RandomDispersionControl()
        {
            InitializeComponent();
        }

        private void MinSpeedControl_ValueChanged(object sender, EventArgs e)
        {
            Dispersion.MinSpeed = (float)MinSpeedControl.Value;
        }

        private void MaxSpeedControl_ValueChanged(object sender, EventArgs e)
        {
            Dispersion.MaxSpeed = (float)MaxSpeedControl.Value;
        }

        private void MinAngleControl_ValueChanged(object sender, EventArgs e)
        {
            Dispersion.MinAngle = (float)MinAngleControl.Value;
        }

        private void MaxAngleControl_ValueChanged(object sender, EventArgs e)
        {
            Dispersion.MaxAngle = (float)MaxAngleControl.Value;
        }

        private void RandomDispersionControl_Load(object sender, EventArgs e)
        {
            MinAngleControl.Value = (decimal)Dispersion.MinAngle;
            MaxAngleControl.Value = (decimal)Dispersion.MaxAngle;
            MinSpeedControl.Value = (decimal)Dispersion.MinSpeed;
            MaxSpeedControl.Value = (decimal)Dispersion.MaxSpeed;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DirectionX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void DirectionY_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
