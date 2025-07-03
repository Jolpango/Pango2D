using Pango2D.Graphics.Particles.Modifiers;

namespace Editor.Controls
{

    public partial class FloatKeyframes : UserControl
    {
        private float currentTime = 0f;
        public event Action OnKeyframesChanged;
        public FloatKeyframes()
        {
            InitializeComponent();
        }


        private void KeyFrameGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void KeyFrameGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            OnKeyframesChanged?.Invoke();
        }

        public List<OpacityModifier.OpacityKeyframe> GetOpacityKeyframes()
        {
            var keyframes = new List<OpacityModifier.OpacityKeyframe>();
            foreach (DataGridViewRow row in KeyFrameGridView.Rows)
            {
                if (row.IsNewRow) continue;
                // try and parse the strings to floats
                if (row.Cells["Time"].Value is null || row.Cells["Value"].Value is null)
                    continue;
                var time = float.TryParse(row.Cells["Time"].Value.ToString(), out var t) ? t : 0f;
                var opacity = float.TryParse(row.Cells["Value"].Value.ToString(), out var o) ? o : 0f;
                keyframes.Add(new OpacityModifier.OpacityKeyframe(time, opacity));
            }
            return keyframes;
        }

        public List<ScaleModifier.ScaleKeyframe> GetScaleKeyframes()
        {
            var keyframes = new List<ScaleModifier.ScaleKeyframe>();
            foreach (DataGridViewRow row in KeyFrameGridView.Rows)
            {
                if (row.IsNewRow) continue;
                // try and parse the strings to floats
                if (row.Cells["Time"].Value is null || row.Cells["Value"].Value is null)
                    continue;
                var time = float.TryParse(row.Cells["Time"].Value.ToString(), out var t) ? t : 0f;
                var scale = float.TryParse(row.Cells["Value"].Value.ToString(), out var o) ? o : 0f;
                keyframes.Add(new ScaleModifier.ScaleKeyframe(time, scale));
            }
            return keyframes;
        }
    }
}
