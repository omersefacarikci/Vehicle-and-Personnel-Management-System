using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;

namespace Staj1
{
    public partial class personelkayıt : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi,kulid,deger,personelid;

        public personelkayıt(string baglanticümlecigim, string kulidm, string degerim, string personelidim)
        {
            InitializeComponent();
            baglanticümlecigi = baglanticümlecigim;
            kulid = kulidm;
            deger = degerim;
            personelid = personelidim;
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Data.mdb;Jet OLEDB:Database Password=");
        private void personelkayıt_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
           
        }
        public void vericek()
        {
            try
            {
                string sorgu = "SELECT * FROM personel WHERE id like'" + personelid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    textBox1.Text = oku["PersonelAdi"].ToString();
                    textBox2.Text = oku["PersonelSoyadi"].ToString();
                    textBox5.Text = oku["DogumTarihi"].ToString();
                    textBox6.Text = oku["Tcno"].ToString();
                    textBox3.Text = oku["PersonelCinsiyet"].ToString();
                    textBox4.Text = oku["PersonelKayıtTarihi"].ToString();
                    textBox7.Text = oku["Maas_Tarihi"].ToString();
                    textBox8.Text = oku["Maas_Tutari"].ToString();
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }
        }
        public void personelekle()
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez. \nLütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO personel (PersonelAdi,PersonelSoyadi,DogumTarihi,Tcno,PersonelCinsiyet,PersonelKayıtTarihi,Maas_Tarihi,Maas_Tutari,aktiflik) VALUES (@PersonelAdi,@PersonelSoyadi,@DogumTarihi,@Tcno,@PersonelCinsiyet,@PersonelKayıtTarihi,@Maas_Tarihi,@Maas_Tutari,@aktiflik) ", baglanti);
                    komut.Parameters.Add("PersonelAdi", OleDbType.VarChar).Value = textBox1.Text;
                    komut.Parameters.Add("PersonelSoyadi", OleDbType.VarChar).Value = textBox2.Text;
                    komut.Parameters.Add("DogumTarihi", OleDbType.VarChar).Value = textBox5.Text;
                    komut.Parameters.Add("Tcno", OleDbType.VarChar).Value = textBox6.Text;
                    komut.Parameters.Add("PersonelCinsiyet", OleDbType.VarChar).Value = textBox3.Text;
                    komut.Parameters.Add("PersonelKayıtTarihi", OleDbType.VarChar).Value = textBox4.Text;
                    komut.Parameters.Add("Maas_Tarihi", OleDbType.VarChar).Value = textBox7.Text;
                    komut.Parameters.Add("Maas_Tutari", OleDbType.VarChar).Value = textBox8.Text;
                    komut.Parameters.Add("aktiflik", OleDbType.VarChar).Value = "1";
                    if (komut.ExecuteNonQuery() == 1)
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                    else
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarısız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }


                }
            }
            catch
            {
                baglanti.Close();
            }
        }

        public void personelguncelle()
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez. \nLütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {

                try
                {
                    baglanti.Open();
                    OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET PersonelAdi=@PersonelAdi,PersonelSoyadi=@PersonelSoyadi,DogumTarihi=@DogumTarihi,Tcno=@Tcno,PersonelCinsiyet=@PersonelCinsiyet,  " +
                         "PersonelKayıtTarihi=@PersonelKayıtTarihi,Maas_Tarihi=@Maas_Tarihi,Maas_Tutari=@Maas_Tutari " +
                         "WHERE id like'" + personelid.ToString() + "'", baglanti);
                    sorgu.Parameters.AddWithValue("PersonelAdi", textBox1.Text);
                    sorgu.Parameters.AddWithValue("PersonelSoyadi", textBox2.Text);
                    sorgu.Parameters.AddWithValue("DogumTarihi", textBox5.Text);
                    sorgu.Parameters.AddWithValue("Tcno", textBox6.Text);
                    sorgu.Parameters.AddWithValue("PersonelCinsiyet", textBox3.Text);
                    sorgu.Parameters.AddWithValue("PersonelKayıtTarihi", textBox4.Text);
                    sorgu.Parameters.AddWithValue("Maas_Tarihi", textBox7.Text);
                    sorgu.Parameters.AddWithValue("Maas_Tutari", textBox8.Text);
                    baglanti.Close();
                    if (sorgu.ExecuteNonQuery() == 1)
                    {
                        XtraMessageBox.Show("Güncelleme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Güncelleme işlemi başarısız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    baglanti.Close();
                }
                catch
                {

                }


            }

        }
       
        
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            personelekle();
        }

       
    }
}