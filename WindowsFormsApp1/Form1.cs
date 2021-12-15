using CatProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            var searchData = SearchTextBox.Text;
            var catData = _catService.GetBreedSearch(searchData);
            IdTextBox.Text = catData.Id.ToString();
            //NameTextBox.Text = catData.Name.ToString();
            //DescriptionTextBox.Text = catData.Description.ToString();
            //TemperamentTextBox.Text = catData.Temperament.ToString();
            //OriginTextBox.Text = catData.Origin.ToString();
            //LifeSpanTextBox.Text = catData.LifeSpan.ToString();
            //LinkTextBox.Text = catData.WikipediaUrl.ToString();

            //var catDataImage = _catService.GetImage(searchData);
            //ImageBox.Image = catDataImage.Url;
        }

        private void LinkTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LaunchWeblink(e.LinkText);
        }
        private void LaunchWeblink(string url)
        {
            Process.Start(url);
        }
    }
}
