namespace CS419_FinalProject
{
    partial class SearchForm
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
            this.buttonRunIndexer = new System.Windows.Forms.Button();
            this.labelPreviousRunStatus = new System.Windows.Forms.Label();
            this.textboxQuery = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRunIndexer
            // 
            this.buttonRunIndexer.Location = new System.Drawing.Point(12, 12);
            this.buttonRunIndexer.Name = "buttonRunIndexer";
            this.buttonRunIndexer.Size = new System.Drawing.Size(75, 23);
            this.buttonRunIndexer.TabIndex = 0;
            this.buttonRunIndexer.Text = "Run Indexer";
            this.buttonRunIndexer.UseVisualStyleBackColor = true;
            this.buttonRunIndexer.Click += new System.EventHandler(this.buttonRunIndexer_Click);
            // 
            // labelPreviousRunStatus
            // 
            this.labelPreviousRunStatus.AutoSize = true;
            this.labelPreviousRunStatus.Location = new System.Drawing.Point(9, 239);
            this.labelPreviousRunStatus.Name = "labelPreviousRunStatus";
            this.labelPreviousRunStatus.Size = new System.Drawing.Size(35, 13);
            this.labelPreviousRunStatus.TabIndex = 2;
            this.labelPreviousRunStatus.Text = "label1";
            // 
            // textboxQuery
            // 
            this.textboxQuery.Location = new System.Drawing.Point(12, 41);
            this.textboxQuery.Name = "textboxQuery";
            this.textboxQuery.Size = new System.Drawing.Size(100, 20);
            this.textboxQuery.TabIndex = 3;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 67);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textboxQuery);
            this.Controls.Add(this.labelPreviousRunStatus);
            this.Controls.Add(this.buttonRunIndexer);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRunIndexer;
        private System.Windows.Forms.Label labelPreviousRunStatus;
        private System.Windows.Forms.TextBox textboxQuery;
        private System.Windows.Forms.Button buttonSearch;
    }
}