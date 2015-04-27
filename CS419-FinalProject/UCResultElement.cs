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
        public UCResultElement()
        {
           
        }

        public UCResultElement(string fileTile, string filePath, string fileContent)
        {
            InitializeComponent();
            Preprocessing(fileTile,filePath,fileContent);
            
        }

        private void Preprocessing(string fileTile, string filePath, string fileContent)
        {
            fileTile = fileTile.Substring(16);
            this.FileName.Text = fileTile;
            this.FilePath.Text = filePath;
            this.FileContent.Text = fileContent;
        }

        private void FileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string docPath = this.FilePath.Text;
            using (StreamReader sr = new StreamReader(docPath))
            {
                string content = sr.ReadToEnd();
                DocumentContentForm contentForm = new DocumentContentForm(content);
                contentForm.ShowDialog();
            }
        }
    }
}
