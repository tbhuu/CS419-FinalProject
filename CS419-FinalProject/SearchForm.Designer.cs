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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRunIndexer
            // 
            this.buttonRunIndexer.Location = new System.Drawing.Point(12, 154);
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
            this.labelPreviousRunStatus.Location = new System.Drawing.Point(93, 159);
            this.labelPreviousRunStatus.Name = "labelPreviousRunStatus";
            this.labelPreviousRunStatus.Size = new System.Drawing.Size(35, 13);
            this.labelPreviousRunStatus.TabIndex = 2;
            this.labelPreviousRunStatus.Text = "label1";
            // 
            // textboxQuery
            // 
            this.textboxQuery.Location = new System.Drawing.Point(12, 56);
            this.textboxQuery.Multiline = true;
            this.textboxQuery.Name = "textboxQuery";
            this.textboxQuery.Size = new System.Drawing.Size(348, 29);
            this.textboxQuery.TabIndex = 3;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(144, 91);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(78, 44);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Text Retrieval System";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 181);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textboxQuery);
            this.Controls.Add(this.labelPreviousRunStatus);
            this.Controls.Add(this.buttonRunIndexer);
            this.Name = "SearchForm";
            this.Text = "Search Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRunIndexer;
        private System.Windows.Forms.Label labelPreviousRunStatus;
        private System.Windows.Forms.TextBox textboxQuery;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label1;
    }
}