using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaSearchSystem
{
    public partial class SearchByText : Form
    {
        public SearchByText()
        {
            InitializeComponent();
        }

        private void ic_back_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }

    }
}
