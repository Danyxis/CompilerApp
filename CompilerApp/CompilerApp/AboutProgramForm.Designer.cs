namespace CompilerApp
{
    partial class AboutProgramForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutProgramForm));
            aboutTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // aboutTextBox
            // 
            aboutTextBox.BackColor = Color.Lavender;
            aboutTextBox.Dock = DockStyle.Fill;
            aboutTextBox.Location = new Point(0, 0);
            aboutTextBox.Name = "aboutTextBox";
            aboutTextBox.ReadOnly = true;
            aboutTextBox.Size = new Size(800, 450);
            aboutTextBox.TabIndex = 0;
            aboutTextBox.Text = "";
            // 
            // AboutProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(aboutTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(818, 497);
            MinimumSize = new Size(818, 497);
            Name = "AboutProgramForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "О программе";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox aboutTextBox;
    }
}