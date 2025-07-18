namespace Editor.Controls.Dispersion
{
    partial class RandomDispersionControl
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
            MinSpeedControl = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            MaxSpeedControl = new NumericUpDown();
            MinAngleControl = new NumericUpDown();
            MaxAngleControl = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)MinSpeedControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxSpeedControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinAngleControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxAngleControl).BeginInit();
            SuspendLayout();
            // 
            // MinSpeedControl
            // 
            MinSpeedControl.Location = new Point(96, 22);
            MinSpeedControl.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            MinSpeedControl.Name = "MinSpeedControl";
            MinSpeedControl.Size = new Size(120, 23);
            MinSpeedControl.TabIndex = 0;
            MinSpeedControl.ValueChanged += MinSpeedControl_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 24);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 1;
            label1.Text = "Min speed";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 51);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 2;
            label2.Text = "Max speed";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 77);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 3;
            label3.Text = "Min angle";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 103);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 4;
            label4.Text = "Max angle";
            label4.Click += label4_Click;
            // 
            // MaxSpeedControl
            // 
            MaxSpeedControl.Location = new Point(96, 49);
            MaxSpeedControl.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            MaxSpeedControl.Name = "MaxSpeedControl";
            MaxSpeedControl.Size = new Size(120, 23);
            MaxSpeedControl.TabIndex = 5;
            MaxSpeedControl.ValueChanged += MaxSpeedControl_ValueChanged;
            // 
            // MinAngleControl
            // 
            MinAngleControl.DecimalPlaces = 2;
            MinAngleControl.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            MinAngleControl.Location = new Point(96, 75);
            MinAngleControl.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            MinAngleControl.Name = "MinAngleControl";
            MinAngleControl.Size = new Size(120, 23);
            MinAngleControl.TabIndex = 6;
            MinAngleControl.ValueChanged += MinAngleControl_ValueChanged;
            // 
            // MaxAngleControl
            // 
            MaxAngleControl.DecimalPlaces = 2;
            MaxAngleControl.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            MaxAngleControl.Location = new Point(96, 101);
            MaxAngleControl.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            MaxAngleControl.Name = "MaxAngleControl";
            MaxAngleControl.Size = new Size(120, 23);
            MaxAngleControl.TabIndex = 7;
            MaxAngleControl.ValueChanged += MaxAngleControl_ValueChanged;
            // 
            // RandomDispersionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MaxAngleControl);
            Controls.Add(MinAngleControl);
            Controls.Add(MaxSpeedControl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(MinSpeedControl);
            Name = "RandomDispersionControl";
            Size = new Size(248, 138);
            Load += RandomDispersionControl_Load;
            ((System.ComponentModel.ISupportInitialize)MinSpeedControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxSpeedControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinAngleControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxAngleControl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown MinSpeedControl;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown MaxSpeedControl;
        private NumericUpDown MinAngleControl;
        private NumericUpDown MaxAngleControl;
    }
}
