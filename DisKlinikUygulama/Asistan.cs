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
    public partial class Asistan : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        int SecilenHastaId = 0;
        public Asistan()
        {
            InitializeComponent();
        }

        private void Asistan_Load(object sender, EventArgs e)
        {
            ListeyiYenile();
        }

        void ListeyiYenile()
        {
            try
            {
                baglanti.Open();

                
                string sorgu = @"
            SELECT 
                k.""KisiId"", 
                k.""TCNo"", 
                k.""Ad"", 
                k.""Soyad"", 
                k.""Telefon"", 
                k.""Sifre"",             -- Şifre eklendi
                h.""DogumTarihi"",       -- Hasta detayları eklendi
                h.""KanGrubu"", 
                h.""Cinsiyet"", 
                h.""KronikHastalik""
            FROM ""public"".""Kisi"" k
            JOIN ""public"".""Hasta"" h ON k.""KisiId"" = h.""KisiId""
            WHERE k.""KisiTuru"" = 4
            ORDER BY k.""KisiId"" DESC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["KisiId"].Visible = false;
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste yüklenirken hata oluştu: " + ex.Message);
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
            }
        }

       
        private void dgvHastalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (e.RowIndex >= 0) //başlığa tıklanmazsa
            {
                //satırdaki tüm bilgileri al
                DataGridViewRow satir = dataGridView1.Rows[e.RowIndex];

                SecilenHastaId = int.Parse(satir.Cells["KisiId"].Value.ToString());
 
                textBox3.Text = satir.Cells["TCNo"].Value.ToString();  
                textBox1.Text = satir.Cells["Ad"].Value.ToString();    
                textBox2.Text = satir.Cells["Soyad"].Value.ToString(); 
                textBox4.Text = satir.Cells["Telefon"].Value.ToString(); 
                textBox6.Text = satir.Cells["Sifre"].Value.ToString();   

                textBox5.Text = satir.Cells["KanGrubu"].Value != DBNull.Value ? satir.Cells["KanGrubu"].Value.ToString() : "";
                textBox7.Text = satir.Cells["KronikHastalik"].Value != DBNull.Value ? satir.Cells["KronikHastalik"].Value.ToString() : "";
                textBox8.Text = satir.Cells["Cinsiyet"].Value != DBNull.Value ? satir.Cells["Cinsiyet"].Value.ToString() : "";

                
                if (satir.Cells["DogumTarihi"].Value != DBNull.Value)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(satir.Cells["DogumTarihi"].Value);
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//ekle butonu
        {
            
            try
            {
                baglanti.Open();

         
                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_HastaEkle\"(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)", baglanti);

                komut.Parameters.AddWithValue("@p1", textBox3.Text);
                komut.Parameters.AddWithValue("@p2", textBox1.Text);
                komut.Parameters.AddWithValue("@p3", textBox2.Text);
                komut.Parameters.AddWithValue("@p4", textBox6.Text); 
                komut.Parameters.AddWithValue("@p5", textBox4.Text);
                komut.Parameters.AddWithValue("@p6", NpgsqlTypes.NpgsqlDbType.Date, dateTimePicker1.Value); 
                komut.Parameters.AddWithValue("@p7", textBox5.Text);

                string cinsiyet = textBox8.Text.Trim();
                char cinsiyetHarf = (string.IsNullOrEmpty(cinsiyet)) ? 'E' : cinsiyet[0];
                komut.Parameters.AddWithValue("@p8", cinsiyetHarf);

                komut.Parameters.AddWithValue("@p9", textBox7.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Hasta başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme Hatası: " + ex.Message);
                if (baglanti.State == System.Data.ConnectionState.Open) baglanti.Close();
            }
        }


        private void button3_Click(object sender, EventArgs e)//güncelle butonu
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Güncelleme yapmak için hastanın TC Kimlik Numarasını girmelisiniz.");
                return;
            }

            try
            {
                baglanti.Open();

                NpgsqlCommand komut = new NpgsqlCommand("CALL \"sp_HastaGuncelle\"(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)", baglanti);

                
                komut.Parameters.AddWithValue("@p1", textBox3.Text); 
                komut.Parameters.AddWithValue("@p2", textBox1.Text);
                komut.Parameters.AddWithValue("@p3", textBox2.Text);
                komut.Parameters.AddWithValue("@p4", textBox6.Text);
                komut.Parameters.AddWithValue("@p5", textBox4.Text);
                komut.Parameters.AddWithValue("@p6", NpgsqlTypes.NpgsqlDbType.Date, dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@p7", textBox5.Text);

                string cinsiyet = textBox8.Text.Trim();
                char cinsiyetHarf = (string.IsNullOrEmpty(cinsiyet)) ? 'E' : cinsiyet[0];
                komut.Parameters.AddWithValue("@p8", cinsiyetHarf);

                komut.Parameters.AddWithValue("@p9", textBox7.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Hasta bilgileri güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
                if (baglanti.State == System.Data.ConnectionState.Open) baglanti.Close();
            }

        }

        void Temizle()
        {
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox6.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox8.Clear();
            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
