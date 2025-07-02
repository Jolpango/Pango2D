using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Graphics.Particles;
using System.Drawing.Imaging;

namespace Editor
{
    public partial class EmitterSettingsControl : UserControl
    {
        public ParticleEmitter ParticleEmitter { get; set; }
        public MonoGame.Forms.NET.Services.EditorService? MonoGameEditor { get; set; }

        public event Action<string>? NameChanged;
        public event Action OnDelete;
        public EmitterSettingsControl()
        {
            InitializeComponent();
            ParticleEmitter = new ParticleEmitter
            {
                Name = "New Emitter",
                EmissionRate = 10,
                MaxParticles = 100,
                Lifetime = 1f,
                IsActive = true,
                Dispersion = new RandomDispersion(100, 200),
                Texture = TextureCache.White4
            };
            MaxParticlesControl.Value = ParticleEmitter.MaxParticles;
            EmissionRateControl.Value = (decimal)ParticleEmitter.EmissionRate;
            TextureNameLabel.Text = "White4x4";

            DispersionMethodDropDown.Items.Add("Random Dispersion");
            DispersionMethodDropDown.Items.Add("Cone Dispersion");
            DispersionMethodDropDown.SelectedIndex = 0; // Default to Random Dispersion

            NewModifierDropDown.Items.Add("Color");
            NewModifierDropDown.Items.Add("Scale");
            NewModifierDropDown.Items.Add("Rotation");
            NewModifierDropDown.Items.Add("Gravity");
            NewModifierDropDown.SelectedIndex = 0; // Default to Color Modifier
        }

        private void EmitterSettingsControl_Load(object sender, EventArgs e)
        {
            TextBoxName.Text = ParticleEmitter.Name;
            NameChanged?.Invoke(ParticleEmitter.Name);
        }

        private void TextboxName_TextChanged(object sender, EventArgs e)
        {
            ParticleEmitter.Name = TextBoxName.Text;
            NameChanged?.Invoke(ParticleEmitter.Name);
        }

        private void IsActive_CheckedChanged(object sender, EventArgs e)
        {
            ParticleEmitter.IsActive = IsActive.Checked;
        }

        private void MaxParticlesControl_ValueChanged(object sender, EventArgs e)
        {
            ParticleEmitter.MaxParticles = (int)MaxParticlesControl.Value;
        }

        private void EmissionRateControl_ValueChanged(object sender, EventArgs e)
        {
            ParticleEmitter.EmissionRate = (float)EmissionRateControl.Value;
        }

        private void TextureNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void ButtonChangeTexture_Click(object sender, EventArgs e)
        {
            var dialogResult = TextureFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                using var stream = TextureFileDialog.OpenFile();
                var texture = Texture2D.FromStream(MonoGameEditor?.GraphicsDevice, stream);
                ParticleEmitter.Texture = texture;
                TextureNameLabel.Text = Path.GetFileName(TextureFileDialog.FileName);
            }
        }

        private void OffsetX_ValueChanged(object sender, EventArgs e)
        {
            ParticleEmitter.Position = new Vector2((float)OffsetX.Value, (float)OffsetY.Value);

        }

        private void OffsetY_ValueChanged(object sender, EventArgs e)
        {
            ParticleEmitter.Position = new Vector2((float)OffsetX.Value, (float)OffsetY.Value);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDelete?.Invoke();
        }

        private void AddModifierButton_Click(object sender, EventArgs e)
        {
            var tabPage = new TabPage(NewModifierDropDown.Text);
            //Create a new modifier control and instance based on the selected type and add it to the tab page and emitter
            ModifierTabs.Controls.Add(tabPage);
        }

        private void NewModifierDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DispersionMethodDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the dispersion method of the ParticleEmitter based on the selected item
            // Update the dispersion panel
        }
    }
}
