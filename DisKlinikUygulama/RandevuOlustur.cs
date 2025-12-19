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
    public partial class RandevuOlustur : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        public RandevuOlustur()
        {
            InitializeComponent();
        }

        private void RandevuOlustur_Load(object sender, EventArgs e)
        {
            HastalariDoldur();
            DoktorlariDoldur();
        }

        void HastalariDoldur()
        {
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT \"KisiId\", \"Ad\" || ' ' || \"Soyad\" as \"AdSoyad\" FROM \"Kisi\" WHERE \"KisiTuru\" = 4", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DisplayMember = "AdSoyad";
                comboBox1.ValueMember = "KisiId";
                comboBox1.DataSource = dt;
                comboBox1.SelectedIndex = -1;
            }
            catch { }
        }

        void DoktorlariDoldur()
        {
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT \"KisiId\", \"Ad\" || ' ' || \"Soyad\" as \"AdSoyad\" FROM \"Kisi\" WHERE \"KisiTuru\" = 2", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox2.DisplayMember = "AdSoyad";
                comboBox2.ValueMember = "KisiId";
                comboBox2.DataSource = dt;
                comboBox2.SelectedIndex = -1;
            }
            catch { }
        }

        // --- KRİTİK NOKTA: LİSTEYİ YENİLEME FONKSİYONU ---
        // Bu fonksiyon her işlemden sonra çalışacak ve Grid'i tazeleyecek.
        void RandevulariListele()
        {
            // Eğer hasta seçili değilse listeyi boşalt ve çık
            if (comboBox1.SelectedValue == null)
            {
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                int hastaId = (int)comboBox1.SelectedValue;

                // Hastanın randevularını çeken SQL
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaRandevulariGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", hastaId);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Grid'e bas
                dataGridView1.DataSource = dt;

                // ID sütununu gizle (Göz kirliliği yapmasın)
                if (dataGridView1.Columns.Contains("RandevuId"))
                    dataGridView1.Columns["RandevuId"].Visible = false;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
                MessageBox.Show("Listeleme Hatası: " + ex.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//hasta seç comboBox
        {

            RandevulariListele();
        }

        void RandevulariListele(int hastaId)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // SQL: fn_HastaRandevulariGetir
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaRandevulariGetir\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", hastaId);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // ID sütununu gizleyelim
                if (dataGridView1.Columns.Contains("RandevuId"))
                    dataGridView1.Columns["RandevuId"].Visible = false;

                baglanti.Close();
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)//ekle butonu
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            try
            {
                baglanti.Open();
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_RandevuOlustur\"(@p1, @p2, @p3, @p4)", baglanti);

                komut.Parameters.AddWithValue("@p1", (int)comboBox1.SelectedValue); // Hasta
                komut.Parameters.AddWithValue("@p2", (int)comboBox2.SelectedValue); // Doktor
                komut.Parameters.Add("@p3", NpgsqlTypes.NpgsqlDbType.Date).Value = dateTimePicker1.Value; // Tarih
                komut.Parameters.AddWithValue("@p4", textBox1.Text); // Saat

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Randevu Eklendi.");
                RandevulariListele(); // LİSTEYİ YENİLE (Hemen görünmesi için)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)//sil butonu
        {
            // Grid'de seçili satır yoksa uyarı ver
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek randevuyu listeden seçiniz.");
                return;
            }

            if (MessageBox.Show("Bu randevuyu silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Seçili satırın ID'sini al
                    int silinecekId = int.Parse(dataGridView1.CurrentRow.Cells["RandevuId"].Value.ToString());

                    baglanti.Open();
                    NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_RandevuSil\"(@p1)", baglanti);
                    komut.Parameters.AddWithValue("@p1", silinecekId);
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Randevu Silindi.");
                    RandevulariListele(); // LİSTEYİ YENİLE (Silinen gitsin)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    if (baglanti.State == ConnectionState.Open) baglanti.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//güncelle butonu
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen güncellenecek randevuyu listeden seçiniz.");
                return;
            }

            try
            {
                // Seçili satırın ID'sini al
                int guncellenecekId = int.Parse(dataGridView1.CurrentRow.Cells["RandevuId"].Value.ToString());

                baglanti.Open();
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_RandevuGuncelle\"(@p1, @p2, @p3, @p4)", baglanti);

                komut.Parameters.AddWithValue("@p1", guncellenecekId);
                komut.Parameters.AddWithValue("@p2", (int)comboBox2.SelectedValue); // Yeni seçilen doktor
                komut.Parameters.Add("@p3", NpgsqlTypes.NpgsqlDbType.Date).Value = dateTimePicker1.Value; // Yeni tarih
                komut.Parameters.AddWithValue("@p4", textBox1.Text); // Yeni saat

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Randevu Güncellendi.");
                RandevulariListele(); // LİSTEYİ YENİLE (Değişiklik görünsün)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Tıklanan satırdaki Tarih ve Saati kutulara geri dolduruyoruz
                try
                {
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Tarih"].Value);
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Zaman"].Value.ToString();
                }
                catch { }
            }
        }
    }
}
