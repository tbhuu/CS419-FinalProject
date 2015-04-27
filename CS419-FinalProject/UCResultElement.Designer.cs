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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Location = new System.Drawing.Point(93, 0);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(55, 13);
            this.FileName.TabIndex = 0;
            this.FileName.TabStop = true;
            this.FileName.Text = "linkLabel1";
            this.FileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FileName_LinkClicked);
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.Location = new System.Drawing.Point(93, 25);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(35, 13);
            this.FilePath.TabIndex = 1;
            this.FilePath.Text = "label1";
            // 
            // FileContent
            // 
            this.FileContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileContent.Location = new System.Drawing.Point(96, 51);
            this.FileContent.Name = "FileContent";
            this.FileContent.ReadOnly = true;
            this.FileContent.Size = new System.Drawing.Size(100, 13);
            this.FileContent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Document Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Document Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Relevance Score";
            // 
            // UCResultElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileContent);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.FileName);
            this.Name = "UCResultElement";
            this.Size = new System.Drawing.Size(205, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel FileName;
        private System.Windows.Forms.Label FilePath;
        private System.Windows.Forms.TextBox FileContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
