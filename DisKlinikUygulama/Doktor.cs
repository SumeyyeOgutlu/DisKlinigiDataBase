using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DisKlinikUygulama
{
    public partial class Doktor : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        
        int SecilenRandevuId = 0;
        public Doktor()
        {
            InitializeComponent();
        }

        private void Doktor_Load(object sender, EventArgs e)
        {
            RandevuListesiGetir(); 
            TedaviListesiGetir();  
            IlacListesiGetir();

            
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
        }

        
        void RandevuListesiGetir()
        {
            try
            {
                baglanti.Open();
                
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_DoktorRandevulariGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", Form1.GirenKullaniciId);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["RandevuId"].Visible = false; 
                    dataGridView1.Columns["Saat"].HeaderText = "Saat";
                    dataGridView1.Columns["HastaAd"].HeaderText = "Adı";
                    dataGridView1.Columns["HastaSoyad"].HeaderText = "Soyadı";
                    dataGridView1.Columns["TCNo"].HeaderText = "TC Kimlik";
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu listesi hatası: " + ex.Message);
                baglanti.Close();
            }
        }

        
        void TedaviListesiGetir()
        {
            try
            {
                baglanti.Open();
                
                NpgsqlCommand komut = new NpgsqlCommand("SELECT \"TedaviId\", \"IslemAdi\" FROM \"Tedaviler\" WHERE \"AktifMi\" = TRUE", baglanti);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                comboBox1.DisplayMember = "IslemAdi"; 
                comboBox1.ValueMember = "TedaviId";   
                comboBox1.DataSource = dt;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tedavi listesi hatası: " + ex.Message);
                baglanti.Close();
            }

        }

        void IlacListesiGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                NpgsqlCommand komut = new NpgsqlCommand("SELECT \"IlacId\", \"IlacAdi\" FROM \"public\".\"Ilac\" ORDER BY \"IlacAdi\"", baglanti);
                
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

               
                comboBox2.DisplayMember = "IlacAdi";
                comboBox2.ValueMember = "IlacId";
                comboBox2.DataSource = dt;
                comboBox2.SelectedIndex = -1; 

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İlaç listesi hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)//seç butonu
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                SecilenRandevuId = int.Parse(dataGridView1.SelectedRows[0].Cells["RandevuId"].Value.ToString());
                string hastaAdi = dataGridView1.SelectedRows[0].Cells["HastaAd"].Value.ToString();

                MessageBox.Show(hastaAdi + " isimli hasta seçildi. İşlem girebilirsiniz.", "Hasta Seçildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                textBox2.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir hasta seçiniz!");
            }
        }

        private void button2_Click(object sender, EventArgs e)//kaydet butonu
        {
            
            if (SecilenRandevuId == 0)
            {
                MessageBox.Show("Lütfen önce yukarıdan bir hasta seçiniz!");
                return;
            }

            try
            {
                
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_IslemEkle\"(@p1, @p2, @p3, @p4)", baglanti);

                
                komut.Parameters.AddWithValue("@p1", SecilenRandevuId);

                
                int disNo = 0;
                int.TryParse(textBox1.Text, out disNo); 
                komut.Parameters.AddWithValue("@p2", disNo);

                
                komut.Parameters.AddWithValue("@p3", (int)comboBox1.SelectedValue);

                
                komut.Parameters.AddWithValue("@p4", textBox2.Text);

               
                komut.ExecuteNonQuery();

                MessageBox.Show("İşlem başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                textBox1.Clear(); // Diş nosunu temizle
                textBox2.Clear(); // Notu temizle

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)//ilaç ekle butonu
        {
            
            if (SecilenRandevuId == 0)
            {
                MessageBox.Show("Lütfen önce listeden bir hasta seçiniz.");
                return;
            }
            if (comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Lütfen ilaç seçin ve adet girin.");
                return;
            }

            try
            {
                baglanti.Open();

                
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_ReceteIlacEkle\"(@p1, @p2, @p3, @p4)", baglanti);

                komut.Parameters.AddWithValue("@p1", SecilenRandevuId); // Randevu ID
                komut.Parameters.AddWithValue("@p2", (int)comboBox2.SelectedValue); // İlaç ID

                // Adet kutusunu sayıya çevir
                int adet = 1;
                int.TryParse(textBox3.Text, out adet);
                komut.Parameters.AddWithValue("@p3", adet);

                // Kullanım şekli
                komut.Parameters.AddWithValue("@p4", textBox4.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("İlaç reçeteye eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reçete hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
    }
}
