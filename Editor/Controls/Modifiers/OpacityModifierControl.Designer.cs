namespace Editor.Controls.Modifiers
{
    partial class OpacityModifierControl
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
            InterpolationControl = new InterpolationTypeControl();
            KeyFramesControl = new FloatKeyframes();
            SuspendLayout();
            // 
            // InterpolationControl
            // 
            InterpolationControl.Location = new Point(3, 5);
            InterpolationControl.Name = "InterpolationControl";
            InterpolationControl.ParticleModifier = null;
            InterpolationControl.Size = new Size(229, 30);
            InterpolationControl.TabIndex = 0;
            // 
            // KeyFramesControl
            // 
            KeyFramesControl.Location = new Point(3, 41);
            KeyFramesControl.Name = "KeyFramesControl";
            KeyFramesControl.Size = new Size(328, 253);
            KeyFramesControl.TabIndex = 1;
            // 
            // OpacityModifierControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(KeyFramesControl);
            Controls.Add(InterpolationControl);
            Name = "OpacityModifierControl";
            Size = new Size(438, 302);
            Load += OpacityModifierControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private InterpolationTypeControl InterpolationControl;
        private FloatKeyframes KeyFramesControl;
    }
}
