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
    public partial class kullaniciprofili : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi,kulid;
        public kullaniciprofili(string baglanticümlecigim,string kulidimiz)
        {
            InitializeComponent();
            kulid = kulidimiz;
            baglanticümlecigi = baglanticümlecigim;
        }
        OleDbConnection baglanti = new OleDbConnection();
        public void vericek()
        {
            try
            {
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'" + kulid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    labelControl4.Text = oku["kulisim"].ToString() + " " + oku["kulsoyisim"].ToString();
                    labelControl5.Text = oku["kule_mail"].ToString();
                    labelControl6.Text = oku["kulil"].ToString();
                    labelControl8.Text = oku["kulilce"].ToString();
                    labelControl10.Text = oku["kulgsm"].ToString();
                    labelControl12.Text = oku["kuladres"].ToString();
                    if (oku["kulresim"].ToString() == "" && oku["cinsiyet"].ToString() == "kadın")
                    {
                        pictureEdit1.Image = Image.FromFile(Application.StartupPath + "\\profil\\kadın.png");
                    }
                    else if (oku["kulresim"].ToString() == "" && oku["cinsiyet"].ToString() == "erkek")
                    {
                        pictureEdit1.Image = Image.FromFile(Application.StartupPath + "\\profil\\bay.jpeg");
                    }
                    else
                    {
                        pictureEdit1.Image = Image.FromFile(Application.StartupPath + "\\profil\\" + oku["kulresim"].ToString());
                    }
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }
        }
        private void kullaniciprofili_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            vericek();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sifredegis ac = new sifredegis(baglanticümlecigi,kulid);
            ac.ShowDialog();

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}