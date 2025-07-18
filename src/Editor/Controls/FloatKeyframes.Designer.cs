namespace Editor.Controls
{
    partial class FloatKeyframes
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            floatKeyframesBindingSource = new BindingSource(components);
            KeyFrameGridView = new DataGridView();
            Time = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)floatKeyframesBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KeyFrameGridView).BeginInit();
            SuspendLayout();
            // 
            // floatKeyframesBindingSource
            // 
            floatKeyframesBindingSource.DataSource = typeof(FloatKeyframes);
            // 
            // KeyFrameGridView
            // 
            KeyFrameGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            KeyFrameGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KeyFrameGridView.Columns.AddRange(new DataGridViewColumn[] { Time, Value });
            KeyFrameGridView.Location = new Point(3, 3);
            KeyFrameGridView.Name = "KeyFrameGridView";
            KeyFrameGridView.Size = new Size(322, 193);
            KeyFrameGridView.TabIndex = 0;
            KeyFrameGridView.CellContentClick += KeyFrameGridView_CellContentClick;
            KeyFrameGridView.CellEndEdit += KeyFrameGridView_CellEndEdit;
            // 
            // Time
            // 
            Time.HeaderText = "Time(0-1)";
            Time.Name = "Time";
            // 
            // Value
            // 
            Value.HeaderText = "Value";
            Value.Name = "Value";
            // 
            // FloatKeyframes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(KeyFrameGridView);
            Name = "FloatKeyframes";
            Size = new Size(328, 199);
            ((System.ComponentModel.ISupportInitialize)floatKeyframesBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)KeyFrameGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BindingSource floatKeyframesBindingSource;
        private DataGridView KeyFrameGridView;
        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn Value;
    }
}
