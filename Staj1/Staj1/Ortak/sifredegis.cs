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
    public partial class sifredegis : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi, kulid;
        public sifredegis(string baglanticümlecigim, string kulidimiz)
        {
            InitializeComponent();
            kulid = kulidimiz;
            baglanticümlecigi = baglanticümlecigim;
        }
        OleDbConnection baglanti = new OleDbConnection();
        private void labelControl4_Click(object sender, EventArgs e)
        {

        }
        string eskisifre;
        public void eskisifrem()
        {
            try
            {
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'" + kulid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    eskisifre = oku["kulsifre"].ToString();                    
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }
        }

        private void sifredegis_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            eskisifrem();
            simpleButton1.Enabled = false;
            labelControl4.Visible = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (eskisifre.ToString() == textEdit1.Text)
                {
                    OleDbCommand cmd = new OleDbCommand("update kullanicilar set kulsifre=@kulsifre WHERE kulid like '" + kulid + "' ",baglanti);
                    cmd.Parameters.AddWithValue("kulsifre", textEdit2.Text);
                    baglanti.Open();
                    cmd.ExecuteNonQuery();
                    baglanti.Close();
                    XtraMessageBox.Show("Şifre değiştirme işlemi başarılı", "Bilgi");
                    Close();

                }
                else
                {
                    XtraMessageBox.Show("Eski şifreniz hatalı", "Hata");
                }

            }
            catch
            {

            }

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit2.Text == textEdit3.Text)
            {
                labelControl4.Visible = true;
                simpleButton1.Enabled = true;
                labelControl4.Text = "Şifre eşleşti.";
                labelControl4.ForeColor = Color.Green;
            }
            else
            {
                labelControl4.Visible = true;
                simpleButton1.Enabled = false;
                labelControl4.Text = "Şifre Eşleşmiyor";
                labelControl4.ForeColor = Color.Red;
            }
             
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit2.Text == textEdit3.Text)
            {
                labelControl4.Visible = true;
                simpleButton1.Enabled = true;
                labelControl4.Text = "Şifre eşleşti.";
                labelControl4.ForeColor = Color.Green;
            }
            else
            {
                labelControl4.Visible = true;
                simpleButton1.Enabled = false;
                labelControl4.Text = "Şifre Eşleşmiyor";
                labelControl4.ForeColor = Color.Red;
            }

        }
    }
}