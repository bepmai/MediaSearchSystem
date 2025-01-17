using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace MediaSearchSystem
{
    public partial class SearchByText : Form
    {
        public SearchByText()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true; // cuon doc
        }

        public class SearchResult
        {
            public string Description { get; set; }
            public string ImagePath { get; set; }
            public float Similarity { get; set; }
        }

        private void ic_back_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }

        private async void btnTextSearch_Click(object sender, EventArgs e)
        {
            string inputText = txtContentSearch.Text;
            int topN = 20;

            var client = new HttpClient();
            var requestData = new
            {
                input_text = inputText,
                top_n = topN
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("http://192.168.1.13:5001/find_similar", content);
                response.EnsureSuccessStatusCode();

                var reponseContent = await response.Content.ReadAsStringAsync();
                //Xu ly ket qua Json tra ve

                var searchResults = JsonConvert.DeserializeObject<List<SearchResult>>(reponseContent);
                flowLayoutPanel1.Controls.Clear();

                //Them anh vao flowLayoutPanel
                foreach (var result in searchResults)
                {
                    var pictureBox = new PictureBox
                    {
                        Width = 240,
                        Height = 220,
                        ImageLocation = "C:\\Users\\tuanh\\Downloads" + result.ImagePath, //Duong dan anh
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    flowLayoutPanel1.Controls.Add(pictureBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtContentSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
