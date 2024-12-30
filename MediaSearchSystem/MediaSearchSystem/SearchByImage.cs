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
    public partial class SearchByImage : Form
    {
        public SearchByImage()
        {
            InitializeComponent();
        }
        private void ic_back_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
