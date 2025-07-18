namespace Editor.Controls
{
    partial class InterpolationTypeControl
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
            InterpolationComboBox = new ComboBox();
            InterpolatonLabel = new Label();
            SuspendLayout();
            // 
            // InterpolationComboBox
            // 
            InterpolationComboBox.FormattingEnabled = true;
            InterpolationComboBox.Location = new Point(87, 4);
            InterpolationComboBox.Name = "InterpolationComboBox";
            InterpolationComboBox.Size = new Size(133, 23);
            InterpolationComboBox.TabIndex = 0;
            InterpolationComboBox.SelectedIndexChanged += InterpolationComboBox_SelectedIndexChanged;
            // 
            // InterpolatonLabel
            // 
            InterpolatonLabel.AutoSize = true;
            InterpolatonLabel.Location = new Point(3, 7);
            InterpolatonLabel.Name = "InterpolatonLabel";
            InterpolatonLabel.Size = new Size(78, 15);
            InterpolatonLabel.TabIndex = 1;
            InterpolatonLabel.Text = "Interpolation:";
            // 
            // InterpolationTypeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(InterpolatonLabel);
            Controls.Add(InterpolationComboBox);
            Name = "InterpolationTypeControl";
            Size = new Size(229, 30);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox InterpolationComboBox;
        private Label InterpolatonLabel;
    }
}
