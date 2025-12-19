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

        int SecilenHastaId = 0; 
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
            

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                
                TumListeyiGetir();
                return;
            }

            try
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                
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
            

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen silinecek hastanın TC Kimlik Numarasını giriniz.");
                return;
            }

            
            DialogResult cevap = MessageBox.Show(textBox1.Text + " TC numaralı hastayı silmek istediğinize emin misiniz?", "SİLME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    
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

                    
                    NpgsqlCommand silKomutu = new NpgsqlCommand("CALL \"sp_HastaSil\"(@p1)", baglanti);
                    silKomutu.Parameters.AddWithValue("@p1", silinecekId);

                    silKomutu.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Hasta başarıyla silindi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    TumListeyiGetir();
                    textBox1.Clear(); 
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
            
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["TCNo"].Value.ToString();
            }
        }

    }
}
