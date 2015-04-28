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
        public UCResultElement()
        {
            InitializeComponent();
        }

        public UCResultElement(string fileTile, string filePath, string fileContent,string text)
        {
            
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

        private void FileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
