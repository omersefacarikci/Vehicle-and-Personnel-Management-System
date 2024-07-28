using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Ini;

namespace Staj1
{
    public partial class Giriş : DevExpress.XtraEditors.XtraForm
    {
        public string baglanticümlesi;
        public Giriş()
        {
            InitializeComponent();
        }
        static IniFile iniayar = new IniFile(Application.StartupPath.ToString()+"\\Hatırla.ini");
        public void baglan()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Data.mdb;Jet OLEDB:Database Password=");
            baglanti.Open();
            baglanticümlesi = baglanti.ConnectionString.ToString();
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM kullanicilar WHERE kuladi='" + textEdit1.Text + "' AND kulsifre='"+textEdit2.Text+"'",baglanti);
            OleDbDataReader oku = sorgu.ExecuteReader();
            if (oku.Read())
            {
                Anaform aç = new Anaform(baglanticümlesi,oku["kulid"].ToString());
                Anaform.ActiveForm.Visible = false;
                aç.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı! Lütfen tekrar deneyiniz.");
                textEdit1.Text = "";
                textEdit2.Text = "";
            }
            oku.Close();
            baglanti.Close();
        }        

        private void Giriş_Load(object sender, EventArgs e)
        {
            hatırlayükle();
        }
        public void hatırlayükle()
        {
            if (iniayar.IniReadValue("Hatırla", "Kullanıcı Adı") == "")
            {
                checkEdit1.Checked = false;
            }
            else 
            {
               checkEdit1.Checked =true;
              textEdit1.Text= iniayar.IniReadValue("Hatırla", "Kullanıcı Adı");
              textEdit2.Text=iniayar.IniReadValue("Hatırla", "Kullanıcı Şifresi");
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                iniayar.IniWriteValue("Hatırla", "Kullanıcı Adı", textEdit1.Text);
                iniayar.IniWriteValue("Hatırla", "Kullanıcı Şifresi", textEdit2.Text);
            }
            else
            {
                iniayar.IniWriteValue("Hatırla", "Kullanıcı Adı", "");
                iniayar.IniWriteValue("Hatırla", "Kullanıcı Şifresi", "");
            }
            hatırlayükle();
            baglan();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "";
            textEdit2.Text = "";

        }
        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
