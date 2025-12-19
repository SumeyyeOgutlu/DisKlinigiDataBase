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
    public partial class Odemeler : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");
        public Odemeler()
        {
            InitializeComponent();
        }

        private void Odemeler_Load(object sender, EventArgs e)
        {
            HastalariDoldur();
        }
        // --- HASTA LİSTESİNİ DOLDUR ---
        void HastalariDoldur()
        {
            try
            {
                // Sadece Hastaları (KisiTuru=4) getir
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT \"KisiId\", \"Ad\" || ' ' || \"Soyad\" as \"AdSoyad\" FROM \"Kisi\" WHERE \"KisiTuru\" = 4", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DisplayMember = "AdSoyad";
                comboBox1.ValueMember = "KisiId";
                comboBox1.DataSource = dt;

                comboBox1.SelectedIndex = -1; // Açılışta boş gelsin
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Eğer geçerli bir seçim yapıldıysa fonksiyonu çağır
            if (comboBox1.SelectedValue != null && comboBox1.SelectedValue is int)
            {
                int secilenId = (int)comboBox1.SelectedValue;
                GecmisOdemeleriListele(secilenId);
            }
        }
        void GecmisOdemeleriListele(int hastaId)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // SQL: fn_HastaOdemeleriGetir (Randevu üzerinden bağlantılı sorgu)
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaOdemeleriGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", hastaId);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // ID sütununu gizleyelim
                if (dataGridView1.Columns.Contains("OdemeId"))
                    dataGridView1.Columns["OdemeId"].Visible = false;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
                // İlk açılışta hata verirse kullanıcıyı rahatsız etmemek için boş bırakabiliriz
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kontroller
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen hasta seçiniz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen ödeme tipini giriniz (Nakit/Kart).");
                return;
            }

            try
            {
                baglanti.Open();

                // Seçilen hastanın ID'sini alıyoruz
                int hastaId = (int)comboBox1.SelectedValue;

                // ADIM 1: ÖNCE HASTANIN EN SON RANDEVUSUNU BULALIM
                // (Çünkü veritabanına RandevuId kaydetmemiz lazım)
                string randevuBulSql = "SELECT \"RandevuId\" FROM \"Randevu\" WHERE \"HastaId\" = @hId ORDER BY \"Tarih\" DESC, \"Zaman\" DESC LIMIT 1";

                NpgsqlCommand bulKomut = new NpgsqlCommand(randevuBulSql, baglanti);
                bulKomut.Parameters.AddWithValue("@hId", hastaId);

                object sonuc = bulKomut.ExecuteScalar();

                if (sonuc == null)
                {
                    MessageBox.Show("Bu hastanın sistemde kayıtlı hiç randevusu yok!\nÖdeme almak için önce randevu oluşturmalısınız.");
                    baglanti.Close();
                    return;
                }

                int sonRandevuId = Convert.ToInt32(sonuc);

                // ADIM 2: BULDUĞUMUZ RANDEVU ID İLE ÖDEMEYİ KAYDEDELİM
                // SQL: sp_OdemeYap (RandevuId, OdemeTipi)
                NpgsqlCommand odemeKomut = new NpgsqlCommand("CALL \"sp_OdemeYap\"(@p1, @p2)", baglanti);
                odemeKomut.Parameters.AddWithValue("@p1", sonRandevuId); // Randevu ID
                odemeKomut.Parameters.AddWithValue("@p2", textBox1.Text.Trim()); // Ödeme Tipi (Nakit/Kart)

                odemeKomut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Ödeme alındı ve hastanın son randevusuna işlendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Listeyi Yenile
                GecmisOdemeleriListele(hastaId);

                // Kutuyu Temizle
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

    }
}
