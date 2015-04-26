using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS419_FinalProject
{
    public partial class SearchForm : Form
    {
        IRSystem myIR = new IRSystem();
        
        #region UILogic
        public SearchForm()
        {
            InitializeComponent();

            if (myIR.PreviousRunExist())
            {
                this.ChangeIRStatus(SearchForm.IRStatus.HasIndexing);
                myIR.BuildSearchEngine();
            }
            else
            {
                this.ChangeIRStatus(SearchForm.IRStatus.NoIndexing);
            }
        }

        private void DisableForm() {
            buttonRunIndexer.Enabled = false;
        }

        private void EnableForm()
        {
            buttonRunIndexer.Enabled = true;
        }

        public enum IRStatus{
            NoIndexing,
            HasIndexing
        }

        public void ChangeIRStatus(IRStatus status) {
            if (status.Equals(IRStatus.NoIndexing)) {
                labelPreviousRunStatus.Text = "No previous run, please click Build IR button to initiate process.";
            } else {
                buttonRunIndexer.Text = "Rerun Indexer";
                labelPreviousRunStatus.Text = "Ready";
            }
        }
        #endregion

        #region Event
        private void buttonRunIndexer_Click(object sender, EventArgs e)
        {
            DisableForm();

            //So the UI do not frozen
            Thread newThread = new Thread(() => {

            myIR.BuildIndexer();
            myIR.BuildSearchEngine();

            //Update UI thread from working thread
            Invoke((Action)delegate()
            {
                EnableForm();
            });

            });

            newThread.Start();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string text = textboxQuery.Lines[0].Trim();
            List<SearchResult> result;
            if (!text.Equals("")) {
                result = myIR.SearchQuery(text);
            }
        }
        #endregion
        
    }
}
