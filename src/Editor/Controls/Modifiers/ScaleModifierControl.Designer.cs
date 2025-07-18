namespace Editor.Controls.Modifiers
{
    partial class ScaleModifierControl
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
            FloatKeyFrameControl = new FloatKeyframes();
            InterpolationTypeControl = new InterpolationTypeControl();
            SuspendLayout();
            // 
            // FloatKeyFrameControl
            // 
            FloatKeyFrameControl.Location = new Point(3, 42);
            FloatKeyFrameControl.Name = "FloatKeyFrameControl";
            FloatKeyFrameControl.Size = new Size(434, 250);
            FloatKeyFrameControl.TabIndex = 0;
            // 
            // InterpolationTypeControl
            // 
            InterpolationTypeControl.Location = new Point(3, 6);
            InterpolationTypeControl.Name = "InterpolationTypeControl";
            InterpolationTypeControl.ParticleModifier = null;
            InterpolationTypeControl.Size = new Size(229, 30);
            InterpolationTypeControl.TabIndex = 1;
            InterpolationTypeControl.Load += InterpolationTypeControl_Load;
            // 
            // ScaleModifierControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(InterpolationTypeControl);
            Controls.Add(FloatKeyFrameControl);
            Name = "ScaleModifierControl";
            Size = new Size(440, 295);
            Load += ScaleModifierControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private FloatKeyframes FloatKeyFrameControl;
        private InterpolationTypeControl InterpolationTypeControl;
    }
}
