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
            ParticleDraw.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;
            ParticleDraw.Location = new Point(-4, -169);
            ParticleDraw.Margin = new Padding(6, 6, 6, 6);
            ParticleDraw.MouseHoverUpdatesOnly = false;
            ParticleDraw.Name = "ParticleDraw";
            ParticleDraw.ParticleEffect = null;
            ParticleDraw.Size = new Size(1319, 2148);
            ParticleDraw.TabIndex = 0;
            ParticleDraw.Text = "particleDraw1";
            ParticleDraw.Click += ParticleDraw_Click;
            // 
            // EmitterTabs
            // 
            EmitterTabs.Location = new Point(1326, 6);
            EmitterTabs.Margin = new Padding(6, 6, 6, 6);
            EmitterTabs.Name = "EmitterTabs";
            EmitterTabs.SelectedIndex = 0;
            EmitterTabs.Size = new Size(999, 1376);
            EmitterTabs.TabIndex = 2;
            // 
            // AddEmitter
            // 
            AddEmitter.Location = new Point(1770, 1389);
            AddEmitter.Margin = new Padding(6, 6, 6, 6);
            AddEmitter.Name = "AddEmitter";
            AddEmitter.Size = new Size(388, 60);
            AddEmitter.TabIndex = 3;
            AddEmitter.Text = "Add new emitter";
            AddEmitter.UseVisualStyleBackColor = true;
            AddEmitter.Click += AddEmitter_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(2169, 1389);
            SaveButton.Margin = new Padding(6, 6, 6, 6);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(156, 60);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save effect";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // BColorDropDown
            // 
            BColorDropDown.FormattingEnabled = true;
            BColorDropDown.Location = new Point(1085, 6);
            BColorDropDown.Margin = new Padding(6, 6, 6, 6);
            BColorDropDown.Name = "BColorDropDown";
            BColorDropDown.Size = new Size(221, 40);
            BColorDropDown.TabIndex = 5;
            BColorDropDown.SelectedIndexChanged += BColorDropDown_SelectedIndexChanged;
            // 
            // SaveEffectDialog
            // 
            SaveEffectDialog.DefaultExt = "pef";
            // 
            // ParticleEffectNameTextBox
            // 
            ParticleEffectNameTextBox.Location = new Point(1326, 1395);
            ParticleEffectNameTextBox.Margin = new Padding(6, 6, 6, 6);
            ParticleEffectNameTextBox.Name = "ParticleEffectNameTextBox";
            ParticleEffectNameTextBox.Size = new Size(247, 39);
            ParticleEffectNameTextBox.TabIndex = 6;
            ParticleEffectNameTextBox.TextChanged += ParticleEffectNameTextBox_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2347, 1453);
            Controls.Add(ParticleEffectNameTextBox);
            Controls.Add(BColorDropDown);
            Controls.Add(SaveButton);
            Controls.Add(AddEmitter);
            Controls.Add(EmitterTabs);
            Controls.Add(ParticleDraw);
            Margin = new Padding(6, 6, 6, 6);
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
