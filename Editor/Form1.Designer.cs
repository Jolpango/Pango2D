namespace Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ParticleDraw = new ParticleDraw();
            EmitterTabs = new TabControl();
            AddEmitter = new Button();
            SaveButton = new Button();
            BColorDropDown = new ComboBox();
            SaveEffectDialog = new SaveFileDialog();
            ParticleEffectNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // ParticleDraw
            // 
            ParticleDraw.Location = new Point(-2, -79);
            ParticleDraw.MouseHoverUpdatesOnly = false;
            ParticleDraw.Name = "ParticleDraw";
            ParticleDraw.ParticleEffect = null;
            ParticleDraw.Size = new Size(710, 1007);
            ParticleDraw.TabIndex = 0;
            ParticleDraw.Text = "particleDraw1";
            ParticleDraw.Click += ParticleDraw_Click;
            // 
            // EmitterTabs
            // 
            EmitterTabs.Location = new Point(714, 3);
            EmitterTabs.Name = "EmitterTabs";
            EmitterTabs.SelectedIndex = 0;
            EmitterTabs.Size = new Size(538, 645);
            EmitterTabs.TabIndex = 2;
            // 
            // AddEmitter
            // 
            AddEmitter.Location = new Point(953, 651);
            AddEmitter.Name = "AddEmitter";
            AddEmitter.Size = new Size(209, 28);
            AddEmitter.TabIndex = 3;
            AddEmitter.Text = "Add new emitter";
            AddEmitter.UseVisualStyleBackColor = true;
            AddEmitter.Click += AddEmitter_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(1168, 651);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(84, 28);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save effect";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // BColorDropDown
            // 
            BColorDropDown.FormattingEnabled = true;
            BColorDropDown.Location = new Point(584, 3);
            BColorDropDown.Name = "BColorDropDown";
            BColorDropDown.Size = new Size(121, 23);
            BColorDropDown.TabIndex = 5;
            BColorDropDown.SelectedIndexChanged += BColorDropDown_SelectedIndexChanged;
            // 
            // SaveEffectDialog
            // 
            SaveEffectDialog.DefaultExt = "pef";
            // 
            // ParticleEffectNameTextBox
            // 
            ParticleEffectNameTextBox.Location = new Point(714, 654);
            ParticleEffectNameTextBox.Name = "ParticleEffectNameTextBox";
            ParticleEffectNameTextBox.Size = new Size(135, 23);
            ParticleEffectNameTextBox.TabIndex = 6;
            ParticleEffectNameTextBox.TextChanged += ParticleEffectNameTextBox_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(ParticleEffectNameTextBox);
            Controls.Add(BColorDropDown);
            Controls.Add(SaveButton);
            Controls.Add(AddEmitter);
            Controls.Add(EmitterTabs);
            Controls.Add(ParticleDraw);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ParticleDraw ParticleDraw;
        private TabControl EmitterTabs;
        private Button AddEmitter;
        private Button SaveButton;
        private ComboBox BColorDropDown;
        private SaveFileDialog SaveEffectDialog;
        private TextBox ParticleEffectNameTextBox;
    }
}
