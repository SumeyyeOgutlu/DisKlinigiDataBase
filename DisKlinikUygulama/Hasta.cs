using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinikUygulama
{
    public partial class Hasta : Form
    {
        // Veritabanı bağlantı
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        public string gelenTC = "";
        public Hasta()
        {
            InitializeComponent();
            
        }

        
        public Hasta(string tc)
        {
            InitializeComponent();
            gelenTC = tc;
        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            // Eğer TC boşsa  işlem yapma
            if (string.IsNullOrEmpty(gelenTC)) return;

            
            label4.Text = "Randevularım";
            label5.Text = "Ödemelerim";

            
            ProfilBilgileriniGetir();
            RandevulariGetir();
            OdemeleriGetir();
        }

        void ProfilBilgileriniGetir()
        {
            try
            {
                baglanti.Open();

                
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaProfilGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", gelenTC);

                NpgsqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    label1.Text = "Sayın " + dr["AdSoyad"].ToString();
                    label2.Text = "TC: " + gelenTC;
                    label3.Text = "Kan Grubu: " + dr["KanGrubu"].ToString() + "\n" +
                                  "Kronik Hastalık: " + dr["KronikHastalik"].ToString();
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Profil Yükleme Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        void RandevulariGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaRandevulari\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", gelenTC);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                
                if (dataGridView1.Columns.Contains("Doktor"))
                    dataGridView1.Columns["Doktor"].HeaderText = "Doktor Adı";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevu Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        
        void OdemeleriGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaOdemeleri\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", gelenTC);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;

                
                if (dataGridView2.Columns.Contains("Tutar"))
                    dataGridView2.Columns["Tutar"].DefaultCellStyle.Format = "C2";

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
    }
}
