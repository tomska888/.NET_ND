using CatProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private CatDataService _catService;
        private string _catImageId; 

        public Form1()
        {
            InitializeComponent();
            var httpClient = new HttpClientWrapper();
            _catService = new CatDataService(httpClient);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _catService.SaveFavorites(_catImageId);
            MessageBox.Show("Image saved to favourites!");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program about cat breeds");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            var searchData = SearchTextBox.Text;
            var catDataList = await _catService.GetBreedSearch(searchData);
            if (catDataList == null || catDataList.Count == 0)
            {
                MessageBox.Show("Not found!");
                return;
            }
            var catData = catDataList[0];
            IdTextBox.Text = catData.Id.ToString();
            NameTextBox.Text = catData.Name.ToString();
            DescriptionTextBox.Text = catData.Description.ToString();
            TemperamentTextBox.Text = catData.Temperament.ToString();
            OriginTextBox.Text = catData.Origin.ToString();
            LifeSpanTextBox.Text = catData.LifeSpan.ToString();
            LinkTextBox.Text = catData.WikipediaUrl.ToString();

            var catDataImage = await _catService.GetImage(searchData);
            _catImageId = catDataImage[0].Id;
            ImageBox.Load(catDataImage[0].Url);
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
