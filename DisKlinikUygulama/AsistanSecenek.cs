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
    public partial class AsistanSecenek : Form
    {
        public AsistanSecenek()
        {
            InitializeComponent();
        }

        private void AsistanSecenek_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//ekle/ güncelle butonu
        {
            Asistan asistan = new Asistan();
            asistan.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)//sil/ arama butonu
        {
            Asistan2 asistan2 = new Asistan2();
            asistan2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandevuOlustur randevuolustur = new RandevuOlustur();
            randevuolustur.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//ödemeler butonu
        {
            Odemeler odemeler=new Odemeler();
            odemeler.ShowDialog();
        }
    }
}
