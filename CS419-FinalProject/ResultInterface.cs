using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS419_FinalProject
{
    public partial class ResultInterface : Form
    {
        private List<SearchResult> _result;
        private string query = "";
        private HashSet<int> feedback = new HashSet<int>();

        internal List<SearchResult> Result
        {
            get { return _result; }
            set { _result = value; }
        }
      
        public ResultInterface()
        {
            InitializeComponent();
        }
        public ResultInterface(List<SearchResult> re,string text)
        {
            InitializeComponent();
            Result = re;
            this.query = text;
            LoadResult(10);
        }

        private void ItemClicked(object sender, CS419_FinalProject.UCResultElement.ElementClickEventArgs e)
        {
            if (feedback.Count < 5)
                feedback.Add(e.index);
        }

        private void LoadResult(int k)
        {
            if (Result.Count >=50)
            {
                for (int i = k - 10; i < k; ++i)
                {
                    UCResultElement element = new UCResultElement(Result[i].fileName, Result[i].fileName, Result[i].relevantValue.ToString(),this.query, i);
                    element.ElementClicked += ItemClicked;
                    flowPanelResult.Controls.Add(element);
                }
            }
            else
            {
                int i = 0;
                foreach(SearchResult r in Result)
                {
                    UCResultElement element = new UCResultElement(r.fileName, r.fileName, r.relevantValue.ToString(),this.query, i);
                    element.ElementClicked += ItemClicked;
                    flowPanelResult.Controls.Add(element);
                    ++i;
                }
            }
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flowPanelResult.Controls.Clear();
            LinkLabel linkLabel = (LinkLabel)sender;
            int  index =Int16.Parse(linkLabel.Text)*10;
            LoadResult(index);
        }

        #region RefreshEvent
        public delegate void RefreshEventHandler(Object sender, RefreshEventArgs e);
        public event RefreshEventHandler RefreshEvent;

        public class RefreshEventArgs : EventArgs
        {
            public int[] items;
            public string query;
            public RefreshEventArgs(string query, int[] items)
                : base()
            {
                this.items = items;
                this.query = query;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (feedback.Count > 0)
            {
                int[] items = feedback.ToArray<int>();
                feedback.Clear();
                if (this.RefreshEvent != null)
                    this.RefreshEvent(this, new RefreshEventArgs(query, items));
                flowPanelResult.Controls.Clear();
                LoadResult(10);
            }
        }
        #endregion
    }
}
