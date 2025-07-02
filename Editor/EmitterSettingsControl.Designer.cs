namespace Editor
{
    partial class EmitterSettingsControl
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
            TextBoxName = new TextBox();
            IsActive = new CheckBox();
            label1 = new Label();
            MaxParticlesControl = new NumericUpDown();
            label2 = new Label();
            EmissionRateControl = new NumericUpDown();
            TextureFileDialog = new OpenFileDialog();
            ButtonChangeTexture = new Button();
            TextureNameLabel = new Label();
            label3 = new Label();
            OffsetX = new NumericUpDown();
            OffsetY = new NumericUpDown();
            AddModifierButton = new Button();
            label4 = new Label();
            label5 = new Label();
            DispersionPanel = new Panel();
            DeleteButton = new Button();
            ModifierTabs = new TabControl();
            NewModifierDropDown = new ComboBox();
            DispersionMethodDropDown = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)MaxParticlesControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EmissionRateControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OffsetX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OffsetY).BeginInit();
            SuspendLayout();
            // 
            // TextBoxName
            // 
            TextBoxName.Location = new Point(3, 3);
            TextBoxName.Name = "TextBoxName";
            TextBoxName.Size = new Size(185, 23);
            TextBoxName.TabIndex = 0;
            TextBoxName.TextChanged += TextboxName_TextChanged;
            // 
            // IsActive
            // 
            IsActive.AutoSize = true;
            IsActive.Checked = true;
            IsActive.CheckState = CheckState.Checked;
            IsActive.Location = new Point(5, 30);
            IsActive.Name = "IsActive";
            IsActive.Size = new Size(68, 19);
            IsActive.TabIndex = 1;
            IsActive.Text = "Is active";
            IsActive.UseVisualStyleBackColor = true;
            IsActive.CheckedChanged += IsActive_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 60);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 3;
            label1.Text = "Max particles";
            // 
            // MaxParticlesControl
            // 
            MaxParticlesControl.Location = new Point(92, 54);
            MaxParticlesControl.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            MaxParticlesControl.Name = "MaxParticlesControl";
            MaxParticlesControl.Size = new Size(75, 23);
            MaxParticlesControl.TabIndex = 4;
            MaxParticlesControl.ValueChanged += MaxParticlesControl_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 87);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "EmissionRate";
            // 
            // EmissionRateControl
            // 
            EmissionRateControl.DecimalPlaces = 2;
            EmissionRateControl.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            EmissionRateControl.Location = new Point(92, 84);
            EmissionRateControl.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            EmissionRateControl.Name = "EmissionRateControl";
            EmissionRateControl.Size = new Size(75, 23);
            EmissionRateControl.TabIndex = 6;
            EmissionRateControl.ValueChanged += EmissionRateControl_ValueChanged;
            // 
            // TextureFileDialog
            // 
            TextureFileDialog.FileName = "texture";
            // 
            // ButtonChangeTexture
            // 
            ButtonChangeTexture.Location = new Point(3, 453);
            ButtonChangeTexture.Name = "ButtonChangeTexture";
            ButtonChangeTexture.Size = new Size(98, 23);
            ButtonChangeTexture.TabIndex = 7;
            ButtonChangeTexture.Text = "Change texture";
            ButtonChangeTexture.UseVisualStyleBackColor = true;
            ButtonChangeTexture.Click += ButtonChangeTexture_Click;
            // 
            // TextureNameLabel
            // 
            TextureNameLabel.AutoSize = true;
            TextureNameLabel.Location = new Point(109, 457);
            TextureNameLabel.Name = "TextureNameLabel";
            TextureNameLabel.Size = new Size(38, 15);
            TextureNameLabel.TabIndex = 8;
            TextureNameLabel.Text = "label3";
            TextureNameLabel.Click += TextureNameLabel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 118);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 9;
            label3.Text = "Offset(X, Y)";
            // 
            // OffsetX
            // 
            OffsetX.Location = new Point(77, 114);
            OffsetX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OffsetX.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OffsetX.Name = "OffsetX";
            OffsetX.Size = new Size(53, 23);
            OffsetX.TabIndex = 10;
            OffsetX.ValueChanged += OffsetX_ValueChanged;
            // 
            // OffsetY
            // 
            OffsetY.Location = new Point(136, 114);
            OffsetY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            OffsetY.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            OffsetY.Name = "OffsetY";
            OffsetY.Size = new Size(55, 23);
            OffsetY.TabIndex = 11;
            OffsetY.ValueChanged += OffsetY_ValueChanged;
            // 
            // AddModifierButton
            // 
            AddModifierButton.Location = new Point(173, 283);
            AddModifierButton.Name = "AddModifierButton";
            AddModifierButton.Size = new Size(98, 23);
            AddModifierButton.TabIndex = 12;
            AddModifierButton.Text = "Add modifier";
            AddModifierButton.UseVisualStyleBackColor = true;
            AddModifierButton.Click += AddModifierButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 288);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 13;
            label4.Text = "Modifiers:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 153);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 14;
            label5.Text = "Dispersion:";
            // 
            // DispersionPanel
            // 
            DispersionPanel.Location = new Point(6, 175);
            DispersionPanel.Name = "DispersionPanel";
            DispersionPanel.Size = new Size(283, 82);
            DispersionPanel.TabIndex = 15;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(194, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(98, 23);
            DeleteButton.TabIndex = 16;
            DeleteButton.Text = "Delete emitter";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // ModifierTabs
            // 
            ModifierTabs.Location = new Point(5, 310);
            ModifierTabs.Name = "ModifierTabs";
            ModifierTabs.SelectedIndex = 0;
            ModifierTabs.Size = new Size(283, 137);
            ModifierTabs.TabIndex = 17;
            // 
            // NewModifierDropDown
            // 
            NewModifierDropDown.FormattingEnabled = true;
            NewModifierDropDown.Location = new Point(71, 284);
            NewModifierDropDown.Name = "NewModifierDropDown";
            NewModifierDropDown.Size = new Size(97, 23);
            NewModifierDropDown.TabIndex = 18;
            NewModifierDropDown.SelectedIndexChanged += NewModifierDropDown_SelectedIndexChanged;
            // 
            // DispersionMethodDropDown
            // 
            DispersionMethodDropDown.FormattingEnabled = true;
            DispersionMethodDropDown.Location = new Point(82, 149);
            DispersionMethodDropDown.Name = "DispersionMethodDropDown";
            DispersionMethodDropDown.Size = new Size(206, 23);
            DispersionMethodDropDown.TabIndex = 19;
            DispersionMethodDropDown.SelectedIndexChanged += DispersionMethodDropDown_SelectedIndexChanged;
            // 
            // EmitterSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(DispersionMethodDropDown);
            Controls.Add(NewModifierDropDown);
            Controls.Add(ModifierTabs);
            Controls.Add(DeleteButton);
            Controls.Add(DispersionPanel);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(AddModifierButton);
            Controls.Add(OffsetY);
            Controls.Add(OffsetX);
            Controls.Add(label3);
            Controls.Add(TextureNameLabel);
            Controls.Add(ButtonChangeTexture);
            Controls.Add(EmissionRateControl);
            Controls.Add(label2);
            Controls.Add(MaxParticlesControl);
            Controls.Add(label1);
            Controls.Add(IsActive);
            Controls.Add(TextBoxName);
            Name = "EmitterSettingsControl";
            Size = new Size(292, 479);
            Load += EmitterSettingsControl_Load;
            ((System.ComponentModel.ISupportInitialize)MaxParticlesControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)EmissionRateControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)OffsetX).EndInit();
            ((System.ComponentModel.ISupportInitialize)OffsetY).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBoxName;
        private CheckBox IsActive;
        private Label label1;
        private NumericUpDown MaxParticlesControl;
        private Label label2;
        private NumericUpDown EmissionRateControl;
        private OpenFileDialog TextureFileDialog;
        private Button ButtonChangeTexture;
        private Label TextureNameLabel;
        private Label label3;
        private NumericUpDown OffsetX;
        private NumericUpDown OffsetY;
        private Button AddModifierButton;
        private Label label4;
        private Label label5;
        private Panel DispersionPanel;
        private Button DeleteButton;
        private TabControl ModifierTabs;
        private ComboBox NewModifierDropDown;
        private ComboBox DispersionMethodDropDown;
    }
}
