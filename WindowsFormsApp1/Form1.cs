using CatProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private CatDataService _catService;

        public Form1()
        {
            InitializeComponent();
            var httpClient = new HttpClientWrapper();
            _catService = new CatDataService(httpClient);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program about cat breeds");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchData = textBox1.Text;
            var catData = _catService.GetBreedSearch(searchData);
            textBox2.Text = catData.Id.ToString();
        }
    }
}
