using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles;

namespace Editor
{
    public partial class Form1 : Form
    {
        public static readonly Dictionary<string, Microsoft.Xna.Framework.Color> ColorMap = new()
        {
            { "AliceBlue", Microsoft.Xna.Framework.Color.AliceBlue },
            { "AntiqueWhite", Microsoft.Xna.Framework.Color.AntiqueWhite },
            { "Aqua", Microsoft.Xna.Framework.Color.Aqua },
            { "Aquamarine", Microsoft.Xna.Framework.Color.Aquamarine },
            { "Azure", Microsoft.Xna.Framework.Color.Azure },
            { "Beige", Microsoft.Xna.Framework.Color.Beige },
            { "Bisque", Microsoft.Xna.Framework.Color.Bisque },
            { "Black", Microsoft.Xna.Framework.Color.Black },
            { "BlanchedAlmond", Microsoft.Xna.Framework.Color.BlanchedAlmond },
            { "Blue", Microsoft.Xna.Framework.Color.Blue },
            { "BlueViolet", Microsoft.Xna.Framework.Color.BlueViolet },
            { "Brown", Microsoft.Xna.Framework.Color.Brown },
            { "BurlyWood", Microsoft.Xna.Framework.Color.BurlyWood },
            { "CadetBlue", Microsoft.Xna.Framework.Color.CadetBlue },
            { "Chartreuse", Microsoft.Xna.Framework.Color.Chartreuse },
            { "Chocolate", Microsoft.Xna.Framework.Color.Chocolate },
            { "Coral", Microsoft.Xna.Framework.Color.Coral },
            { "CornflowerBlue", Microsoft.Xna.Framework.Color.CornflowerBlue },
            { "Cornsilk", Microsoft.Xna.Framework.Color.Cornsilk },
            { "Crimson", Microsoft.Xna.Framework.Color.Crimson },
            { "Cyan", Microsoft.Xna.Framework.Color.Cyan },
            { "DarkBlue", Microsoft.Xna.Framework.Color.DarkBlue },
            { "DarkCyan", Microsoft.Xna.Framework.Color.DarkCyan },
            { "DarkGoldenrod", Microsoft.Xna.Framework.Color.DarkGoldenrod },
            { "DarkGray", Microsoft.Xna.Framework.Color.DarkGray },
            { "DarkGreen", Microsoft.Xna.Framework.Color.DarkGreen },
            { "DarkKhaki", Microsoft.Xna.Framework.Color.DarkKhaki },
            { "DarkMagenta", Microsoft.Xna.Framework.Color.DarkMagenta },
            { "DarkOliveGreen", Microsoft.Xna.Framework.Color.DarkOliveGreen },
            { "DarkOrange", Microsoft.Xna.Framework.Color.DarkOrange },
            { "DarkOrchid", Microsoft.Xna.Framework.Color.DarkOrchid },
            { "DarkRed", Microsoft.Xna.Framework.Color.DarkRed },
            { "DarkSalmon", Microsoft.Xna.Framework.Color.DarkSalmon },
            { "DarkSeaGreen", Microsoft.Xna.Framework.Color.DarkSeaGreen },
            { "DarkSlateBlue", Microsoft.Xna.Framework.Color.DarkSlateBlue },
            { "DarkSlateGray", Microsoft.Xna.Framework.Color.DarkSlateGray },
            { "DarkTurquoise", Microsoft.Xna.Framework.Color.DarkTurquoise },
            { "DarkViolet", Microsoft.Xna.Framework.Color.DarkViolet },
            { "DeepPink", Microsoft.Xna.Framework.Color.DeepPink },
            { "DeepSkyBlue", Microsoft.Xna.Framework.Color.DeepSkyBlue },
            { "DimGray", Microsoft.Xna.Framework.Color.DimGray },
            { "DodgerBlue", Microsoft.Xna.Framework.Color.DodgerBlue },
            { "Firebrick", Microsoft.Xna.Framework.Color.Firebrick },
            { "FloralWhite", Microsoft.Xna.Framework.Color.FloralWhite },
            { "ForestGreen", Microsoft.Xna.Framework.Color.ForestGreen },
            { "Fuchsia", Microsoft.Xna.Framework.Color.Fuchsia },
            { "Gainsboro", Microsoft.Xna.Framework.Color.Gainsboro },
            { "GhostWhite", Microsoft.Xna.Framework.Color.GhostWhite },
            { "Gold", Microsoft.Xna.Framework.Color.Gold },
            { "Goldenrod", Microsoft.Xna.Framework.Color.Goldenrod },
            { "Gray", Microsoft.Xna.Framework.Color.Gray },
            { "Green", Microsoft.Xna.Framework.Color.Green },
            { "GreenYellow", Microsoft.Xna.Framework.Color.GreenYellow },
            { "Honeydew", Microsoft.Xna.Framework.Color.Honeydew },
            { "HotPink", Microsoft.Xna.Framework.Color.HotPink },
            { "IndianRed", Microsoft.Xna.Framework.Color.IndianRed },
            { "Indigo", Microsoft.Xna.Framework.Color.Indigo },
            { "Ivory", Microsoft.Xna.Framework.Color.Ivory },
            { "Khaki", Microsoft.Xna.Framework.Color.Khaki },
            { "Lavender", Microsoft.Xna.Framework.Color.Lavender },
            { "LavenderBlush", Microsoft.Xna.Framework.Color.LavenderBlush },
            { "LawnGreen", Microsoft.Xna.Framework.Color.LawnGreen },
            { "LemonChiffon", Microsoft.Xna.Framework.Color.LemonChiffon },
            { "LightBlue", Microsoft.Xna.Framework.Color.LightBlue },
            { "LightCoral", Microsoft.Xna.Framework.Color.LightCoral },
            { "LightCyan", Microsoft.Xna.Framework.Color.LightCyan },
            { "LightGoldenrodYellow", Microsoft.Xna.Framework.Color.LightGoldenrodYellow },
            { "LightGray", Microsoft.Xna.Framework.Color.LightGray },
            { "LightGreen", Microsoft.Xna.Framework.Color.LightGreen },
            { "LightPink", Microsoft.Xna.Framework.Color.LightPink },
            { "LightSalmon", Microsoft.Xna.Framework.Color.LightSalmon },
            { "LightSeaGreen", Microsoft.Xna.Framework.Color.LightSeaGreen },
            { "LightSkyBlue", Microsoft.Xna.Framework.Color.LightSkyBlue },
            { "LightSlateGray", Microsoft.Xna.Framework.Color.LightSlateGray },
            { "LightSteelBlue", Microsoft.Xna.Framework.Color.LightSteelBlue },
            { "LightYellow", Microsoft.Xna.Framework.Color.LightYellow },
            { "Lime", Microsoft.Xna.Framework.Color.Lime },
            { "LimeGreen", Microsoft.Xna.Framework.Color.LimeGreen },
            { "Linen", Microsoft.Xna.Framework.Color.Linen },
            { "Magenta", Microsoft.Xna.Framework.Color.Magenta },
            { "Maroon", Microsoft.Xna.Framework.Color.Maroon },
            { "MediumAquamarine", Microsoft.Xna.Framework.Color.MediumAquamarine },
            { "MediumBlue", Microsoft.Xna.Framework.Color.MediumBlue },
            { "MediumOrchid", Microsoft.Xna.Framework.Color.MediumOrchid },
            { "MediumPurple", Microsoft.Xna.Framework.Color.MediumPurple },
            { "MediumSeaGreen", Microsoft.Xna.Framework.Color.MediumSeaGreen },
            { "MediumSlateBlue", Microsoft.Xna.Framework.Color.MediumSlateBlue },
            { "MediumSpringGreen", Microsoft.Xna.Framework.Color.MediumSpringGreen },
            { "MediumTurquoise", Microsoft.Xna.Framework.Color.MediumTurquoise },
            { "MediumVioletRed", Microsoft.Xna.Framework.Color.MediumVioletRed },
            { "MidnightBlue", Microsoft.Xna.Framework.Color.MidnightBlue },
            { "MintCream", Microsoft.Xna.Framework.Color.MintCream },
            { "MistyRose", Microsoft.Xna.Framework.Color.MistyRose },
            { "Moccasin", Microsoft.Xna.Framework.Color.Moccasin },
            { "NavajoWhite", Microsoft.Xna.Framework.Color.NavajoWhite },
            { "Navy", Microsoft.Xna.Framework.Color.Navy },
            { "OldLace", Microsoft.Xna.Framework.Color.OldLace },
            { "Olive", Microsoft.Xna.Framework.Color.Olive },
            { "OliveDrab", Microsoft.Xna.Framework.Color.OliveDrab },
            { "Orange", Microsoft.Xna.Framework.Color.Orange },
            { "OrangeRed", Microsoft.Xna.Framework.Color.OrangeRed },
            { "Orchid", Microsoft.Xna.Framework.Color.Orchid },
            { "PaleGoldenrod", Microsoft.Xna.Framework.Color.PaleGoldenrod },
            { "PaleGreen", Microsoft.Xna.Framework.Color.PaleGreen },
            { "PaleTurquoise", Microsoft.Xna.Framework.Color.PaleTurquoise },
            { "PaleVioletRed", Microsoft.Xna.Framework.Color.PaleVioletRed },
            { "PapayaWhip", Microsoft.Xna.Framework.Color.PapayaWhip },
            { "PeachPuff", Microsoft.Xna.Framework.Color.PeachPuff },
            { "Peru", Microsoft.Xna.Framework.Color.Peru },
            { "Pink", Microsoft.Xna.Framework.Color.Pink },
            { "Plum", Microsoft.Xna.Framework.Color.Plum },
            { "PowderBlue", Microsoft.Xna.Framework.Color.PowderBlue },
            { "Purple", Microsoft.Xna.Framework.Color.Purple },
            { "Red", Microsoft.Xna.Framework.Color.Red },
            { "RosyBrown", Microsoft.Xna.Framework.Color.RosyBrown },
            { "RoyalBlue", Microsoft.Xna.Framework.Color.RoyalBlue },
            { "SaddleBrown", Microsoft.Xna.Framework.Color.SaddleBrown },
            { "Salmon", Microsoft.Xna.Framework.Color.Salmon },
            { "SandyBrown", Microsoft.Xna.Framework.Color.SandyBrown },
            { "SeaGreen", Microsoft.Xna.Framework.Color.SeaGreen },
            { "SeaShell", Microsoft.Xna.Framework.Color.SeaShell },
            { "Sienna", Microsoft.Xna.Framework.Color.Sienna },
            { "Silver", Microsoft.Xna.Framework.Color.Silver },
            { "SkyBlue", Microsoft.Xna.Framework.Color.SkyBlue },
            { "SlateBlue", Microsoft.Xna.Framework.Color.SlateBlue },
            { "SlateGray", Microsoft.Xna.Framework.Color.SlateGray },
            { "Snow", Microsoft.Xna.Framework.Color.Snow },
            { "SpringGreen", Microsoft.Xna.Framework.Color.SpringGreen },
            { "SteelBlue", Microsoft.Xna.Framework.Color.SteelBlue },
            { "Tan", Microsoft.Xna.Framework.Color.Tan },
            { "Teal", Microsoft.Xna.Framework.Color.Teal },
            { "Thistle", Microsoft.Xna.Framework.Color.Thistle },
            { "Tomato", Microsoft.Xna.Framework.Color.Tomato },
            { "Turquoise", Microsoft.Xna.Framework.Color.Turquoise },
            { "Violet", Microsoft.Xna.Framework.Color.Violet },
            { "Wheat", Microsoft.Xna.Framework.Color.Wheat },
            { "White", Microsoft.Xna.Framework.Color.White },
            { "WhiteSmoke", Microsoft.Xna.Framework.Color.WhiteSmoke },
            { "Yellow", Microsoft.Xna.Framework.Color.Yellow },
            { "YellowGreen", Microsoft.Xna.Framework.Color.YellowGreen }
        };
        public ParticleEffect ParticleEffect { get; set; } = new() { Position = new Vector2(325, 350) };
        public Microsoft.Xna.Framework.Color BackgroundColor
        {
            get => ParticleDraw.BackgroundColor;
            set => ParticleDraw.BackgroundColor = value;
        }
        public MonoGame.Forms.NET.Services.EditorService MonoGameEditor { get; set; }
        public Form1()
        {
            InitializeComponent();
            ParticleDraw.ParticleEffect = ParticleEffect;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BColorDropDown.Items.AddRange(ColorMap.Keys.ToArray());
            BColorDropDown.SelectedIndex = ColorMap.Keys.ToList().FindIndex((item) => item == "CornflowerBlue");
            MonoGameEditor = ParticleDraw.Editor;
        }

        private void ParticleDraw_Click(object sender, EventArgs e)
        {

        }

        private void EmitterTabs_Click(object sender, EventArgs e)
        {

        }

        private void AddEmitter_Click(object sender, EventArgs e)
        {
            var count = EmitterTabs.TabPages.Count;
            var newTab = new TabPage("E" + count);
            var emitterControl = new EmitterSettingsControl
            {
                Dock = DockStyle.Fill
            };
            ParticleEffect.Emitters.Add(emitterControl.ParticleEmitter);
            emitterControl.NameChanged += (name) =>
            {
                newTab.Text = name;
            };
            emitterControl.OnDelete += () =>
            {
                ParticleEffect.Emitters.Remove(emitterControl.ParticleEmitter);
                EmitterTabs.TabPages.Remove(newTab);
            };
            emitterControl.MonoGameEditor = MonoGameEditor;
            newTab.Controls.Add(emitterControl);
            EmitterTabs.TabPages.Add(newTab);
        }

        private void BColorDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedColor = BColorDropDown.SelectedItem.ToString();
            if (selectedColor != null)
            {
                BackgroundColor = ColorMap.TryGetValue(selectedColor, out var color) 
                    ? color 
                    : Microsoft.Xna.Framework.Color.CornflowerBlue;
            }
        }
    }
}
