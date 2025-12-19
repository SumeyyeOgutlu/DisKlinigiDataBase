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

        // 2. GLOBAL DEĞİŞKEN (Hangi hastayı seçtiğimizi burada tutacağız)
        int SecilenRandevuId = 0;
        public Doktor()
        {
            InitializeComponent();
        }

        private void Doktor_Load(object sender, EventArgs e)
        {
            RandevuListesiGetir(); // Listeyi doldur
            TedaviListesiGetir();  // ComboBox'ı doldur (Dolgu, Kanal vs.)
            IlacListesiGetir();

            // Başlangıçta alt kısmı kilitleyelim (Hasta seçmeden işlem yapılmasın)
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
        }

        // --- FONKSİYON 1: RANDEVU LİSTESİNİ GETİR ---
        void RandevuListesiGetir()
        {
            try
            {
                baglanti.Open();
                // SQL'deki fonksiyonumuzu çağırıyoruz (Doktor ID'sine göre)
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_DoktorRandevulariGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", Form1.GirenKullaniciId);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Tablo makyajı
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["RandevuId"].Visible = false; // ID gizli kalsın
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

        // --- FONKSİYON 2: TEDAVİLERİ COMBOBOX'A DOLDUR ---
        void TedaviListesiGetir()
        {
            try
            {
                baglanti.Open();
                // Sadece Aktif olan tedavileri getir
                NpgsqlCommand komut = new NpgsqlCommand("SELECT \"TedaviId\", \"IslemAdi\" FROM \"Tedaviler\" WHERE \"AktifMi\" = TRUE", baglanti);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // ComboBox Ayarları
                comboBox1.DisplayMember = "IslemAdi"; // Ekranda görünen isim (Örn: Dolgu)
                comboBox1.ValueMember = "TedaviId";   // Arka plandaki ID (Örn: 5)
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
                // NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_IlaclariGetir\"()", baglanti);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // cmbIlaclar senin formuna eklediğin yeni ComboBox'ın adı olmalı
                comboBox2.DisplayMember = "IlacAdi";
                comboBox2.ValueMember = "IlacId";
                comboBox2.DataSource = dt;
                comboBox2.SelectedIndex = -1; // Başlangıçta boş gelsin

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
                // Seçili satırdaki Gizli ID'yi al
                SecilenRandevuId = int.Parse(dataGridView1.SelectedRows[0].Cells["RandevuId"].Value.ToString());
                string hastaAdi = dataGridView1.SelectedRows[0].Cells["HastaAd"].Value.ToString();

                MessageBox.Show(hastaAdi + " isimli hasta seçildi. İşlem girebilirsiniz.", "Hasta Seçildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kutucukları Aktif Et
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
            // Önce güvenlik: Hasta seçilmiş mi?
            if (SecilenRandevuId == 0)
            {
                MessageBox.Show("Lütfen önce yukarıdan bir hasta seçiniz!");
                return;
            }

            try
            {
                // Bağlantı açık değilse açalım
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                // SQL Prosedürünü Çağırıyoruz: sp_IslemEkle
                // Bu prosedürü daha önce Valentina'da oluşturmuştuk
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_IslemEkle\"(@p1, @p2, @p3, @p4)", baglanti);

                // Parametre 1: Randevu ID (Seç butonundan geldi)
                komut.Parameters.AddWithValue("@p1", SecilenRandevuId);

                // Parametre 2: Diş Numarası (textBox1'den geliyor)
                int disNo = 0;
                int.TryParse(textBox1.Text, out disNo); // Sayı girilmezse 0 kabul et, hata verme
                komut.Parameters.AddWithValue("@p2", disNo);

                // Parametre 3: Tedavi ID (comboBox1'den geliyor)
                // SelectedValue, arka plandaki ID'yi verir
                komut.Parameters.AddWithValue("@p3", (int)comboBox1.SelectedValue);

                // Parametre 4: Doktor Notu (textBox2'den geliyor)
                komut.Parameters.AddWithValue("@p4", textBox2.Text);

                // Komutu Çalıştır (Veritabanına Kaydet)
                komut.ExecuteNonQuery();

                MessageBox.Show("İşlem başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Temizlik yapalım ki yeni işleme hazır olsun
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
            // 1. Kontroller
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

                // SQL Prosedürünü Çağır: sp_ReceteIlacEkle
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_ReceteIlacEkle\"(@p1, @p2, @p3, @p4)", baglanti);

                komut.Parameters.AddWithValue("@p1", SecilenRandevuId); // Randevu ID
                komut.Parameters.AddWithValue("@p2", (int)comboBox2.SelectedValue); // İlaç ID

                // Adet kutusunu sayıya çevir
                int adet = 1;
                int.TryParse(textBox3.Text, out adet);
                komut.Parameters.AddWithValue("@p3", adet);

                // Kullanım şekli (Örn: "Tok karna")
                komut.Parameters.AddWithValue("@p4", textBox4.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("İlaç reçeteye eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Temizlik
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
