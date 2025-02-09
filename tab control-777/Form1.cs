﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace tab_control_777
{
    public partial class Form1 : Form
    {
        string baglanti = "Server=localhost;Database=film_arsiv;Uid=root;Pwd=;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DgwDoldur();
        }

        private void DgwDoldur()
        {
            using (MySqlConnection baglan = new MySqlConnection(baglanti))
            {
                baglan.Open();
                string sorgu = "SELECT * FROM filmler;";
                MySqlCommand cmd = new MySqlCommand(sorgu, baglan);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgwFilmler.DataSource = dt;
                dgwFilmler.Columns["yonetmen"].Visible = false;
                dgwFilmler.Columns["yil"].Visible = false;
                dgwFilmler.Columns["poster"].Visible = false;
                dgwFilmler.Columns["film_odul"].Visible = false;
                dgwFilmler.Columns["sure"].Visible = false;
            }
        }

        private void dgwFilmler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgwFilmler.SelectedRows.Count > 0)
            {
                lblFilmAd.Text = dgwFilmler.SelectedRows[0].Cells["film_ad"].Value.ToString();
                lblYonetmen.Text = dgwFilmler.SelectedRows[0].Cells["yonetmen"].Value.ToString();
                lblTur.Text = dgwFilmler.SelectedRows[0].Cells["tur"].Value.ToString();
                lblYil.Text = dgwFilmler.SelectedRows[0].Cells["yil"].Value.ToString();
                lblSure.Text = dgwFilmler.SelectedRows[0].Cells["sure"].Value.ToString();
                lblPuan.Text = dgwFilmler.SelectedRows[0].Cells["imdb_puan"].Value.ToString();
                lblOdul.Text = "Ödül Yok";

                if (Convert.ToBoolean(dgwFilmler.SelectedRows[0].Cells["film_odul"].Value))
                {
                    lblOdul.Text = "Ödüllü Film";
                }
                // poster işine bakalım
                string resimYol = Path.Combine(Environment.CurrentDirectory, "poster", dgwFilmler.SelectedRows[0].Cells["poster"].Value.ToString());

                if (File.Exists(resimYol))
                {
                    pbPoster.ImageLocation = resimYol;
                    pbPoster.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
    }
}
