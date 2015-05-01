using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CS419_FinalProject
{
    public partial class UCResultElement : UserControl
    {
        private string query = "";
        private int index;
        public UCResultElement()
        {
            InitializeComponent();
        }

        public UCResultElement(string fileTile, string filePath, string fileContent,string text, int index = -1)
        {
            this.index = index;
            InitializeComponent();
            this.query = text;
            Preprocessing(fileTile,filePath,fileContent);
            
        }

        private void Preprocessing(string fileTile, string filePath, string fileContent)
        {
         
            using (StreamReader sr = new StreamReader(filePath))
            {
                this.FileName.Text = sr.ReadLine();
                this.FilePath.Text = filePath;
                while (sr.ReadLine() == null) sr.ReadLine();
                this.FileContent.Text = sr.ReadLine();
            }
        }

        #region ElementClickEvent
        public delegate void ElementClickedEventHandler(Object sender, ElementClickEventArgs e);
        public event ElementClickedEventHandler ElementClicked;

        public class ElementClickEventArgs : EventArgs
        {
            public int index;
            public ElementClickEventArgs(int index)
                : base()
            {
                this.index = index;
            }
        }
        #endregion

        private void FileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.ElementClicked != null)
                this.ElementClicked(this, new ElementClickEventArgs(this.index));

            string docPath = this.FilePath.Text;
            string fileName = docPath.Substring(16);
            using (StreamReader sr = new StreamReader(docPath))
            {
                string content = sr.ReadToEnd();
                DocumentContentForm contentForm = new DocumentContentForm(fileName,content,query);
                contentForm.ShowDialog();
            }
        }
    }
}
