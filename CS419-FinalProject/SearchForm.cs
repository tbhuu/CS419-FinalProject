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

        private Indexer myIndexer;
        private SearchEngine mySearchEngine;

        private string resourcePath = GlobalParameter.resourcesPath;
        private int docCount = GlobalParameter.docCount;
        #region UILogic
        public SearchForm()
        {
            InitializeComponent();
            
            CreateTemporaryFolder();

            if (PreviousRunExist())
            {
                this.ChangeIRStatus(SearchForm.IRStatus.HasIndexing);
                BuildSearchEngine();
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
            BuildIndexer();
            BuildSearchEngine();

            //Update UI thread from working thread
            Invoke((Action)delegate()
            {
                EnableForm();
            });

            });

            newThread.Start();
        }
        #endregion

        private bool PreviousRunExist()
        {
            if (File.Exists(GlobalParameter.indexPath)
                && File.Exists(GlobalParameter.indexMapPath)
                && File.Exists(GlobalParameter.indexLengthPath))
                return true;
            return false;
        }

        public void BuildSearchEngine()
        {
            mySearchEngine = new SearchEngine(
                GlobalParameter.indexPath,
                GlobalParameter.indexMapPath,
                GlobalParameter.indexLengthPath,
                docCount,
                //TODO: no groundtrust
                "..//..//Resources//ohsumed.87");
        }

        public void BuildIndexer()
        {
            myIndexer = new Indexer(new SPIMIndexer(resourcePath));
            docCount = myIndexer.Index();
        }

        private void CreateTemporaryFolder()
        {
            //Only create new if folder doesnot exist
            Directory.CreateDirectory("SPIMI");
            Directory.CreateDirectory("Resources");
            Directory.CreateDirectory("Index");
        }
    }
}
