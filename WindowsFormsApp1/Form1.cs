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

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (_catImageId == null)
            {
                MessageBox.Show("There is no image!");
            }
            else
            {
                await _catService.SaveFavorites(_catImageId);
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

        private async void ShowAll_ClickAsync(object sender, EventArgs e)
        {
            var catDataList = await _catService.GetBreedList();
            dataGridView1.DataSource = catDataList;

            SetHyperLinkOnGrid();
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

        private async void ShowFavButton_Click(object sender, EventArgs e)
        {
            var catShowFav = await _catService.GetFavourites();
            dataGridView2.DataSource = catShowFav;

            SetHyperLinkOnGrid();
        }

        private DataGridViewCellStyle GetHyperLinkStyleForGridCell()
        {
            {
                DataGridViewCellStyle test = new DataGridViewCellStyle();
                System.Drawing.Font l_objFont = new System.Drawing.Font(FontFamily.GenericSansSerif, 8, FontStyle.Underline);
                test.Font = l_objFont;
                test.ForeColor = Color.Blue;
                return test;
            }
        }

        private void SetHyperLinkOnGrid()
        {
            if (dataGridView1.Columns.Contains("WikipediaUrl"))
            {
                dataGridView1.Columns["WikipediaUrl"].DefaultCellStyle = GetHyperLinkStyleForGridCell();
            }

            if (dataGridView2.Columns.Contains("ImageId"))
            {
                dataGridView2.Columns["ImageId"].DefaultCellStyle = GetHyperLinkStyleForGridCell();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.Contains("WikipediaUrl"))
            {
                if (!String.IsNullOrWhiteSpace(dataGridView1.CurrentCell.EditedFormattedValue.ToString()))
                {
                    System.Diagnostics.Process.Start("" + dataGridView1.CurrentCell.EditedFormattedValue);
                }
            }
        }

        private void dgv2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].HeaderText.Contains("ImageId"))
            {
                if (!String.IsNullOrWhiteSpace(dataGridView2.CurrentCell.EditedFormattedValue.ToString()))
                {
                    System.Diagnostics.Process.Start("https://cdn2.thecatapi.com/images/" + dataGridView2.CurrentCell.EditedFormattedValue +".jpg");
                }
            }
        }
    }
}
