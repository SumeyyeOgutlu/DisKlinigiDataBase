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
    public partial class Yonetici : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");
        public Yonetici()
        {
            InitializeComponent();
        }

        private void Yonetici_Load(object sender, EventArgs e)
        {
            // ComboBox içini dolduruyoruz
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Doktor");
            comboBox1.Items.Add("Asistan");
            comboBox1.SelectedIndex = 0; // İlk başta Doktor seçili gelsin
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Doktor")
            {
                label7.Text = "Diploma No:";
                label8.Text = "Uzmanlık Alanı:";
            }
            else // Asistan
            {
                label7.Text = "Görev:";
                label8.Text = "Sertifika No:";
            }

            // Kutuları temizle ki karışıklık olmasın
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)//ekle butonu
        {
            // Basit Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Lütfen temel bilgileri (TC, Ad, Soyad, Şifre) doldurunuz.");
                return;
            }

            try
            {
                baglanti.Open();

                if (comboBox1.Text == "Doktor")
                {
                    // --- DOKTOR EKLEME ---
                    NpgsqlCommand komut = new NpgsqlCommand("CALL sp_DoktorEkle(@p1, @p2, @p3, @p4, @p5, @p6, @p7)", baglanti);
                    komut.Parameters.AddWithValue("@p1", textBox3.Text);
                    komut.Parameters.AddWithValue("@p2", textBox1.Text);
                    komut.Parameters.AddWithValue("@p3", textBox2.Text);
                    komut.Parameters.AddWithValue("@p4", textBox5.Text);     // SQL'deki p_Sifre
                    komut.Parameters.AddWithValue("@p5", textBox4.Text);
                    komut.Parameters.AddWithValue("@p6", textBox6.Text);   // Diploma No
                    komut.Parameters.AddWithValue("@p7", textBox7.Text);   // Uzmanlık

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Doktor başarıyla eklendi.");
                }
                else
                {
                    // --- ASİSTAN EKLEME ---
                    NpgsqlCommand komut = new NpgsqlCommand("CALL sp_AsistanEkle(@p1, @p2, @p3, @p4, @p5, @p6, @p7)", baglanti);
                    komut.Parameters.AddWithValue("@p1", textBox3.Text);
                    komut.Parameters.AddWithValue("@p2", textBox1.Text);
                    komut.Parameters.AddWithValue("@p3", textBox2.Text);
                    komut.Parameters.AddWithValue("@p4", textBox5.Text);     // SQL'deki p_Sifre
                    komut.Parameters.AddWithValue("@p5", textBox4.Text);
                    komut.Parameters.AddWithValue("@p6", textBox6.Text);   // Görev
                    komut.Parameters.AddWithValue("@p7", textBox7.Text);   // Sertifika No

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Asistan başarıyla eklendi.");
                }

                baglanti.Close();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        void Temizle()
        {
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();

            // Ekstra kutuları da temizle (Diploma/Görev vb.)
            textBox6.Clear();
            textBox7.Clear();

            // İmleci tekrar en başa (TC kutusuna) odakla
            textBox3.Focus();
        }

        private void button2_Click(object sender, EventArgs e)//sil butonu
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Silmek istediğiniz kişinin TC Numarasını giriniz.");
                return;
            }

            if (MessageBox.Show("Bu personeli silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();

                    // Kişiyi TC'sine göre bulup ID'sini almamız lazım, sonra sileceğiz.
                    // Veya direkt TC ile silen bir prosedür yazabiliriz ama şu an ID ile silmeyi kullanıyoruz.
                    // Pratik olması için buraya küçük bir SQL sorgusu yazıyorum:

                    NpgsqlCommand komutIdBul = new NpgsqlCommand("SELECT \"KisiId\" FROM \"Kisi\" WHERE \"TCNo\" = @tc", baglanti);
                    komutIdBul.Parameters.AddWithValue("@tc", textBox3.Text);
                    object sonuc = komutIdBul.ExecuteScalar();

                    if (sonuc != null)
                    {
                        int silinecekId = Convert.ToInt32(sonuc);

                        // Daha önce yazdığımız sp_PersonelSil prosedürünü çağırıyoruz
                        NpgsqlCommand komutSil = new NpgsqlCommand("CALL \"sp_PersonelSil\"(@p1)", baglanti);
                        komutSil.Parameters.AddWithValue("@p1", silinecekId);
                        komutSil.ExecuteNonQuery();

                        MessageBox.Show("Personel silindi.");
                        Temizle();
                    }
                    else
                    {
                        MessageBox.Show("Bu TC numarasına ait personel bulunamadı.");
                    }

                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme Hatası: " + ex.Message);
                    if (baglanti.State == ConnectionState.Open) baglanti.Close();
                }
            }
        }
    }
}
