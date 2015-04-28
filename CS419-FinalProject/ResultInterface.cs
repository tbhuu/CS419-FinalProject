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

        private void LoadResult(int k)
        {
            if (Result.Count >=50)
            {
                for (int i = k - 10; i < k; ++i)
                {
                    UCResultElement element = new UCResultElement(Result[i].fileName, Result[i].fileName, Result[i].relevantValue.ToString(),this.query);
                    flowPanelResult.Controls.Add(element);
                }
            }
            else
            {
                foreach(SearchResult r in Result)
                {
                    UCResultElement element = new UCResultElement(r.fileName, r.fileName, r.relevantValue.ToString(),this.query);
                    flowPanelResult.Controls.Add(element);
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

       
    }
}
