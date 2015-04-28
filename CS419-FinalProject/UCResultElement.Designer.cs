namespace CS419_FinalProject
{
    partial class UCResultElement
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
            this.FileName = new System.Windows.Forms.LinkLabel();
            this.FilePath = new System.Windows.Forms.Label();
            this.FileContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.Location = new System.Drawing.Point(3, -1);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(83, 18);
            this.FileName.TabIndex = 0;
            this.FileName.TabStop = true;
            this.FileName.Text = "linkLabel1";
            this.FileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FileName_LinkClicked);
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.ForeColor = System.Drawing.Color.Green;
            this.FilePath.Location = new System.Drawing.Point(26, 23);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(35, 13);
            this.FilePath.TabIndex = 1;
            this.FilePath.Text = "label1";
            // 
            // FileContent
            // 
            this.FileContent.BackColor = System.Drawing.SystemColors.ControlLight;
            this.FileContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileContent.Location = new System.Drawing.Point(-1, 39);
            this.FileContent.Multiline = true;
            this.FileContent.Name = "FileContent";
            this.FileContent.ReadOnly = true;
            this.FileContent.Size = new System.Drawing.Size(417, 41);
            this.FileContent.TabIndex = 2;
            // 
            // UCResultElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.FileContent);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.FileName);
            this.Name = "UCResultElement";
            this.Size = new System.Drawing.Size(419, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel FileName;
        private System.Windows.Forms.Label FilePath;
        public System.Windows.Forms.TextBox FileContent;
    }
}
