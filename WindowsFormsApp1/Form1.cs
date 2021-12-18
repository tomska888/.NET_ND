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
        private string _catSubId;

        public Form1()
        {
            InitializeComponent();
            var httpClient = new HttpClientWrapper();
            _catService = new CatDataService(httpClient);
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (_catImageId == null)
            {
                MessageBox.Show("There is no image!");
            }
            else
            {
                await _catService.SaveFavorites(_catImageId, _catSubId);
                MessageBox.Show("Image saved!");
            }
            
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

        private void ShowFavButton_Click(object sender, EventArgs e)
        {

        }

        private async void ShowAll_ClickAsync(object sender, EventArgs e)
        {
            var catDataList = await _catService.GetBreedList();
            dataGridView1.DataSource = catDataList;
        }

        private void SaveList_Click(object sender, EventArgs e)
        {
            TextWriter writer = new StreamWriter(@"C:\Users\zioma\Desktop\CatsInfo.txt");
            if (dataGridView1 == null)
            {
                MessageBox.Show("There is no data!");
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value?.ToString() + "\t" + "|");
                }
                writer.WriteLine("");
                writer.WriteLine("------------------------------------------------------------");
            }
            writer.Close();
            MessageBox.Show("Data Exported");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var catShowFav = await _catService.GetFavourites();
            dataGridView2.DataSource = catShowFav;
        }
    }
}
