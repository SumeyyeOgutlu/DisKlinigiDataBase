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
    public partial class Asistan2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        int SecilenHastaId = 0; // Silinecek kişinin ID'si burada tutulacak
        public Asistan2()
        {
            InitializeComponent();
        }

        private void Asistan2_Load(object sender, EventArgs e)
        {
            TumListeyiGetir();
        }
        void TumListeyiGetir()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // Tüm hastaları detaylarıyla çekiyoruz
                string sorgu = @"
                    SELECT 
                        k.""KisiId"", 
                        k.""TCNo"", 
                        k.""Ad"", 
                        k.""Soyad"", 
                        k.""Telefon"", 
                        h.""KanGrubu"", 
                        h.""DogumTarihi""
                    FROM ""public"".""Kisi"" k
                    JOIN ""public"".""Hasta"" h ON k.""KisiId"" = h.""KisiId""
                    WHERE k.""KisiTuru"" = 4
                    ORDER BY k.""Ad"" ASC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // ID sütununu gizleyelim
                if (dataGridView1.Columns.Contains("KisiId"))
                {
                    dataGridView1.Columns["KisiId"].Visible = false;
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste yüklenirken hata: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)//arama butonu
        {
            /* if (string.IsNullOrWhiteSpace(textBox1.Text))
             {
                 MessageBox.Show("Lütfen aranacak TC numarasını giriniz.");
                 return;
             }

             try
             {
                 baglanti.Open();

                 // SQL Fonksiyonu: fn_HastaAraTC
                 // LIKE kullandığımız için tam TC yazmasa bile içinde geçenleri bulur
                 NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaAraTC\"(@p1)", baglanti);
                 komut.Parameters.AddWithValue("@p1", textBox1.Text.Trim());

                 NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 dataGridView1.DataSource = dt;

                 // ID sütununu gizleyelim (Kullanıcı görmesin ama biz silebileslim)
                 if (dataGridView1.Columns.Contains("KisiId"))
                 {
                     dataGridView1.Columns["KisiId"].Visible = false;
                 }

                 baglanti.Close();

                 if (dt.Rows.Count == 0)
                     MessageBox.Show("Bu TC numarasına ait kayıt bulunamadı.");
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Arama Hatası: " + ex.Message);
                 if (baglanti.State == ConnectionState.Open) baglanti.Close();
             }
         }

         // --- 2. LİSTEDEN SEÇME (Tıklayınca ID'yi al) ---
         // DataGridView'in CellClick olayına bu kodu bağlamayı unutma!
         private void dgvSonuclar_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex >= 0)
             {
                 // Tıklanan satırdaki Gizli ID'yi al
                 SecilenHastaId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["KisiId"].Value.ToString());

                 // Seçilen kişinin adını alıp kullanıcıya gösterelim ki emin olsun
                 string ad = dataGridView1.Rows[e.RowIndex].Cells["Ad"].Value.ToString();
                 string soyad = dataGridView1.Rows[e.RowIndex].Cells["Soyad"].Value.ToString();

                 // İstersen burada Label'a yazdırabilirsin: "Seçilen: Ahmet Yılmaz" gibi
             }*/

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Kutu boşsa her şeyi göster
                TumListeyiGetir();
                return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                // Sadece girilen TC'ye benzeyenleri getir (Filtrele)
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_HastaAraTC\"(@p1)", baglanti);
                komut.Parameters.AddWithValue("@p1", textBox1.Text.Trim());

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                baglanti.Close();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Bu TC numarasına ait kayıt bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama Hatası: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)//sil butonu
        {
            // Önce Güvenlik: Kimse seçildi mi?
            /* if (SecilenHastaId == 0)
             {
                 MessageBox.Show("Lütfen önce listeden silinecek kişiye tıklayınız!");
                 return;
             }

             // Son Karar: Emin misin?
             DialogResult cevap = MessageBox.Show("Seçilen hastayı kalıcı olarak silmek istiyor musunuz?", "SİLME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

             if (cevap == DialogResult.Yes)
             {
                 try
                 {
                     baglanti.Open();

                     // SQL Prosedürü: sp_HastaSil
                     NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_HastaSil\"(@p1)", baglanti);
                     komut.Parameters.AddWithValue("@p1", SecilenHastaId);

                     komut.ExecuteNonQuery();
                     baglanti.Close();

                     MessageBox.Show("Hasta başarıyla silindi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     // Listeyi temizle veya tekrar ara
                     dataGridView1.DataSource = null;
                     SecilenHastaId = 0; // Seçimi sıfırla
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Silme Hatası: " + ex.Message);
                     if (baglanti.State == ConnectionState.Open) baglanti.Close();
                 }
             }
            */

            // 1. Kutu boş mu?
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen silinecek hastanın TC Kimlik Numarasını giriniz.");
                return;
            }

            // 2. Emin misin?
            DialogResult cevap = MessageBox.Show(textBox1.Text + " TC numaralı hastayı silmek istediğinize emin misiniz?", "SİLME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    // ADIM 1: Önce bu TC'ye ait kişinin ID'sini bulmamız lazım
                    // Çünkü sp_HastaSil bizden ID istiyor.
                    NpgsqlCommand idBulKomutu = new NpgsqlCommand("SELECT \"KisiId\" FROM \"public\".\"Kisi\" WHERE \"TCNo\" = @tc AND \"KisiTuru\" = 4", baglanti);
                    idBulKomutu.Parameters.AddWithValue("@tc", textBox1.Text.Trim());

                    object sonuc = idBulKomutu.ExecuteScalar();

                    if (sonuc == null)
                    {
                        MessageBox.Show("Bu TC numarasına ait bir hasta bulunamadı!");
                        baglanti.Close();
                        return;
                    }

                    int silinecekId = Convert.ToInt32(sonuc);

                    // ADIM 2: ID'yi bulduk, şimdi silebiliriz
                    NpgsqlCommand silKomutu = new NpgsqlCommand("CALL \"sp_HastaSil\"(@p1)", baglanti);
                    silKomutu.Parameters.AddWithValue("@p1", silinecekId);

                    silKomutu.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Hasta başarıyla silindi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Listeyi yenile ki silinen kişi ekrandan gitsin
                    TumListeyiGetir();
                    textBox1.Clear(); // Kutuyu temizle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme Hatası: " + ex.Message);
                    if (baglanti.State == ConnectionState.Open) baglanti.Close();
                }
            }
        }
        private void dgvSonuclar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burası boş kalabilir veya istersen tıklanan kişinin TC'sini kutuya yazdırabilirsin:
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["TCNo"].Value.ToString();
            }
        }

    }
}
