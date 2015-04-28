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
    public partial class DocumentContentForm : Form
    {
        private string query = "";
        public DocumentContentForm()
        {
            InitializeComponent();
        }
        public DocumentContentForm(string fileName, string text, string query)
        {
            InitializeComponent();
            this.query = query;
            this.richTextBoxContent.Text = text;
            this.Text = fileName;
            ColoringQuery(this.query);
        }

        private void ColoringQuery(string query)
        {
            string temp = richTextBoxContent.Text.ToLower();
            richTextBoxContent.Text = "";
            richTextBoxContent.Text = temp;

            string[] words = query.Split(' ');
            foreach (string word in words)
            {
                int index = 0;
                while (index < this.richTextBoxContent.Text.LastIndexOf(word))
                {
                    this.richTextBoxContent.Find(word, index, this.richTextBoxContent.TextLength, RichTextBoxFinds.WholeWord);
                    richTextBoxContent.SelectionBackColor = Color.Yellow;
                    index = richTextBoxContent.Text.IndexOf(word, index) + 1;
                }
            }

        }
    }
}
