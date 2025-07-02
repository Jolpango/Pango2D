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
            SuspendLayout();
            // 
            // ParticleDraw
            // 
            ParticleDraw.Location = new Point(-2, -79);
            ParticleDraw.MouseHoverUpdatesOnly = false;
            ParticleDraw.Name = "ParticleDraw";
            ParticleDraw.ParticleEffect = null;
            ParticleDraw.Size = new Size(710, 666);
            ParticleDraw.TabIndex = 0;
            ParticleDraw.Text = "particleDraw1";
            ParticleDraw.Click += ParticleDraw_Click;
            // 
            // EmitterTabs
            // 
            EmitterTabs.Location = new Point(714, 3);
            EmitterTabs.Name = "EmitterTabs";
            EmitterTabs.SelectedIndex = 0;
            EmitterTabs.Size = new Size(299, 526);
            EmitterTabs.TabIndex = 2;
            // 
            // AddEmitter
            // 
            AddEmitter.Location = new Point(714, 535);
            AddEmitter.Name = "AddEmitter";
            AddEmitter.Size = new Size(209, 28);
            AddEmitter.TabIndex = 3;
            AddEmitter.Text = "Add new emitter";
            AddEmitter.UseVisualStyleBackColor = true;
            AddEmitter.Click += AddEmitter_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(929, 535);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(84, 28);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save effect";
            SaveButton.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 575);
            Controls.Add(BColorDropDown);
            Controls.Add(SaveButton);
            Controls.Add(AddEmitter);
            Controls.Add(EmitterTabs);
            Controls.Add(ParticleDraw);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ParticleDraw ParticleDraw;
        private TabControl EmitterTabs;
        private Button AddEmitter;
        private Button SaveButton;
        private ComboBox BColorDropDown;
    }
}
