namespace MediaSearchSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bySketch_Click(object sender, EventArgs e)
        {
            this.Hide();

            SearchBySketch searchbySketch = new SearchBySketch();
            searchbySketch.ShowDialog();

            this.Close();
        }
        private void byText_Click(object sender, EventArgs e) {

            this.Hide();

            SearchByText searchbyText = new SearchByText();
            searchbyText.ShowDialog();

            this.Close();
        }

        private void bySound_Click(object obj, EventArgs e) {

            this.Hide();

            SearchBySound searchbySound = new SearchBySound();
            searchbySound.ShowDialog();

            this.Close();
        }

        private void byImg_Click(object obj, EventArgs e) {
            this.Hide();

            SearchByImage searchbyImage = new SearchByImage();
            searchbyImage.ShowDialog();

            this.Close();
        }
    }
}
