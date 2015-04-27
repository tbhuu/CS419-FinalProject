namespace CS419_FinalProject
{
    partial class DocumentContentForm
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
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(13, 13);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ReadOnly = true;
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContent.ShortcutsEnabled = false;
            this.textBoxContent.Size = new System.Drawing.Size(445, 309);
            this.textBoxContent.TabIndex = 0;
            // 
            // DocumentContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 334);
            this.Controls.Add(this.textBoxContent);
            this.Name = "DocumentContentForm";
            this.Text = "Document Content Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxContent;
    }
}