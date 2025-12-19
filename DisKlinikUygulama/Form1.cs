using Npgsql;
using System.Data;

namespace DisKlinikUygulama
{
    public partial class Form1 : Form
    {

        NpgsqlConnection baglanti = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123Sum.;Database=DisKlinigi");

        public static int GirenKullaniciId;
        public static string GirenKullaniciAdSoyad;
        public static int GirenKullaniciRol;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // SQL Fonksiyonunu çaðýrýyoruz
                NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"fn_GirisYap\"(@p1, @p2)", baglanti);

                
                komut.Parameters.AddWithValue("@p1", textBox2.Text.Trim());

                
                komut.Parameters.AddWithValue("@p2", textBox1.Text.Trim());


                NpgsqlDataReader oku = komut.ExecuteReader();

                if (oku.Read()) // Veri geldiyse giriþ baþarýlý
                {
                    GirenKullaniciId = int.Parse(oku["KullaniciId"].ToString());
                    GirenKullaniciRol = int.Parse(oku["Rol"].ToString());
                    GirenKullaniciAdSoyad = oku["AdSoyad"].ToString();

                    MessageBox.Show("Hoþgeldiniz " + GirenKullaniciAdSoyad, "Giriþ Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Role göre form açma
                    switch (GirenKullaniciRol)
                    {
                        case 1: // Yönetici
                            Yonetici yonetici = new Yonetici();
                            yonetici.Show();
                            break;
                        case 2: // Doktor
                            Doktor doktor = new Doktor();
                            doktor.Show();
                            break;
                        case 3: // Asistan
                            AsistanSecenek asistansecenek = new AsistanSecenek();
                            asistansecenek.Show();
                            break;
                        case 4:
                            Hasta hasta = new Hasta(textBox2.Text.Trim());
                            hasta.Show();
                            break;
                        default:
                            MessageBox.Show("Yetkisiz giriþ!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    this.Hide(); // Giriþ ekranýný gizle
                }
                else
                {
                    MessageBox.Show("Hatalý giriþ. Lütfen tekrar deneyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baðlantý Hatasý: " + ex.Message);
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }
    }
}
