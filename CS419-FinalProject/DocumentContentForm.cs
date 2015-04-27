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
        public DocumentContentForm()
        {
            InitializeComponent();
        }
        public DocumentContentForm(string text)
        {
            InitializeComponent(); 
            this.textBoxContent.Text = text;
        }
    }
}
