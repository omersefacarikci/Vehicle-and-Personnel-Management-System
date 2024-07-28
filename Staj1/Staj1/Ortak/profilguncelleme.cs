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
using System.IO;
namespace Staj1
{
    public partial class profilguncelleme : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi, kulid;
        public profilguncelleme(string baglanticümlecigim, string kulidimiz)
        {
            InitializeComponent();
            kulid = kulidimiz;
            baglanticümlecigi = baglanticümlecigim;
        }
        OleDbConnection baglanti = new OleDbConnection();
        private void profilguncelleme_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            vericek();
            comboBoxEdit1.Properties.Items.Add("Adana");
            comboBoxEdit1.Properties.Items.Add("Adıyaman");
            comboBoxEdit1.Properties.Items.Add("Afyonkarahisar");
            comboBoxEdit1.Properties.Items.Add("Ağrı");
            comboBoxEdit1.Properties.Items.Add("Aksaray");
            comboBoxEdit1.Properties.Items.Add("Amasya");
            comboBoxEdit1.Properties.Items.Add("Ankara");
            comboBoxEdit1.Properties.Items.Add("Antalya");
            comboBoxEdit1.Properties.Items.Add("Ardahan");
            comboBoxEdit1.Properties.Items.Add("Artvin");
            comboBoxEdit1.Properties.Items.Add("Aydın");
            comboBoxEdit1.Properties.Items.Add("Balıkesir");
            comboBoxEdit1.Properties.Items.Add("Bartın");
            comboBoxEdit1.Properties.Items.Add("Batman");
            comboBoxEdit1.Properties.Items.Add("Bayburt");
            comboBoxEdit1.Properties.Items.Add("Bilecik");
            comboBoxEdit1.Properties.Items.Add("Bingöl");
            comboBoxEdit1.Properties.Items.Add("Bitlis");
            comboBoxEdit1.Properties.Items.Add("Bolu");
            comboBoxEdit1.Properties.Items.Add("Burdur");
            comboBoxEdit1.Properties.Items.Add("Bursa");
            comboBoxEdit1.Properties.Items.Add("Çanakkale");
            comboBoxEdit1.Properties.Items.Add("Çankırı");
            comboBoxEdit1.Properties.Items.Add("Çorum");
            comboBoxEdit1.Properties.Items.Add("Denizli");
            comboBoxEdit1.Properties.Items.Add("Diyarbakır");
            comboBoxEdit1.Properties.Items.Add("Düzce");
            comboBoxEdit1.Properties.Items.Add("Edirne");
            comboBoxEdit1.Properties.Items.Add("Elazığ");
            comboBoxEdit1.Properties.Items.Add("Erzincan");
            comboBoxEdit1.Properties.Items.Add("Erzurum");
            comboBoxEdit1.Properties.Items.Add("Eskişehir");
            comboBoxEdit1.Properties.Items.Add("Gaziantep");
            comboBoxEdit1.Properties.Items.Add("Giresun");
            comboBoxEdit1.Properties.Items.Add("Gümüşhane");
            comboBoxEdit1.Properties.Items.Add("Hakkâri");
            comboBoxEdit1.Properties.Items.Add("Hatay");
            comboBoxEdit1.Properties.Items.Add("Iğdır");
            comboBoxEdit1.Properties.Items.Add("Isparta");
            comboBoxEdit1.Properties.Items.Add("İstanbul");
            comboBoxEdit1.Properties.Items.Add("İzmir");
            comboBoxEdit1.Properties.Items.Add("Kahramanmaraş");
            comboBoxEdit1.Properties.Items.Add("Karabük");
            comboBoxEdit1.Properties.Items.Add("Karaman");
            comboBoxEdit1.Properties.Items.Add("Kars");
            comboBoxEdit1.Properties.Items.Add("Kastamonu");
            comboBoxEdit1.Properties.Items.Add("Kayseri");
            comboBoxEdit1.Properties.Items.Add("Kilis");
            comboBoxEdit1.Properties.Items.Add("Kırıkkale");
            comboBoxEdit1.Properties.Items.Add("Kırklareli");
            comboBoxEdit1.Properties.Items.Add("Kırşehir");
            comboBoxEdit1.Properties.Items.Add("Kocaeli");
            comboBoxEdit1.Properties.Items.Add("Konya");
            comboBoxEdit1.Properties.Items.Add("Kütahya");
            comboBoxEdit1.Properties.Items.Add("Malatya");
            comboBoxEdit1.Properties.Items.Add("Manisa");
            comboBoxEdit1.Properties.Items.Add("Mardin");
            comboBoxEdit1.Properties.Items.Add("Mersin");
            comboBoxEdit1.Properties.Items.Add("Muğla");
            comboBoxEdit1.Properties.Items.Add("Muş");
            comboBoxEdit1.Properties.Items.Add("Nevşehir");
            comboBoxEdit1.Properties.Items.Add("Niğde");
            comboBoxEdit1.Properties.Items.Add("Ordu");
            comboBoxEdit1.Properties.Items.Add("Osmaniye");
            comboBoxEdit1.Properties.Items.Add("Rize");
            comboBoxEdit1.Properties.Items.Add("Sakarya");
            comboBoxEdit1.Properties.Items.Add("Samsun");
            comboBoxEdit1.Properties.Items.Add("Şanlıurfa");
            comboBoxEdit1.Properties.Items.Add("Siirt");
            comboBoxEdit1.Properties.Items.Add("Sinop");
            comboBoxEdit1.Properties.Items.Add("Sivas");
            comboBoxEdit1.Properties.Items.Add("Şırnak");
            comboBoxEdit1.Properties.Items.Add("Tekirdağ");
            comboBoxEdit1.Properties.Items.Add("Tokat");
            comboBoxEdit1.Properties.Items.Add("Trabzon");
            comboBoxEdit1.Properties.Items.Add("Tunceli");
            comboBoxEdit1.Properties.Items.Add("Uşak");
            comboBoxEdit1.Properties.Items.Add("Van");
            comboBoxEdit1.Properties.Items.Add("Yalova");
            comboBoxEdit1.Properties.Items.Add("Yozgat");
            comboBoxEdit1.Properties.Items.Add("Zonguldak");
        }
        string resimyolumuz, resimadimiz;
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
                    textEdit6.Text = oku["kuladi"].ToString();
                    textEdit1.Text = oku["kulisim"].ToString();
                    textEdit2.Text = oku["kulsoyisim"].ToString();
                    textEdit3.Text = oku["kule_mail"].ToString();
                    textEdit4.Text= oku["kulgsm"].ToString();
                    comboBoxEdit1.Text = oku["kulil"].ToString();
                    textEdit5.Text = oku["kulilce"].ToString();
                    memoEdit1.Text = oku["kuladres"].ToString();
                    pictureEdit1.Image = GetCopyImage(Application.StartupPath + "\\profil\\"+oku["kulresim"].ToString()+"");
                    resimyolumuz = Application.StartupPath + "\\profil\\" + oku["kulresim"].ToString() + "";
                    resimadimiz = oku["kulresim"].ToString();
                    if (oku["cinsiyet"].ToString()=="Kadın")
                    {
                        radioGroup1.SelectedIndex = 0;
                    }
                    else if (oku["cinsiyet"].ToString() == "Erkek")
                    {
                        radioGroup1.SelectedIndex = 1;
                    }
                    if(oku["yedekdurumu"].ToString()=="Alınsın")
                    {
                        checkEdit1.Checked = true;
                        labelControl14.Text = oku["yedekyolu"].ToString();
                    }
                    else if (oku["yedekdurumu"].ToString() == "Alınmasın")
                    {
                        checkEdit1.Checked = false;
                    }
                    else 
                    {
                        checkEdit1.Checked = false;
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
        public Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        OpenFileDialog dialog =new OpenFileDialog();
        string profilresim;
        public void guncelle()
        {
            DateTime dt= DateTime.Now;
            DateTime dateString = DateTime.Now;
            string saat = dateString.Hour.ToString();
            string dakika = dateString.Minute.ToString();
            string saniye = dateString.Second.ToString();
            string sn = saat + "." + dakika + "." + saniye + ".";
            string tarih = dt.ToString("dd.MM.yyyy");

            if (textEdit6.Text == "" || textEdit1.Text == "" || textEdit2.Text == "")
            {
                XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez \n  Lütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                try
                {
                    if (dialog.SafeFileName == "")
                    {
                        profilresim = resimadimiz.ToString();
                    }
                    else
                    {
                        try
                        {
                            File.Delete(resimyolumuz.ToString());
                        }
                        catch
                        {

                        }
                        string resimyolu = Application.StartupPath + "\\profil\\" + tarih + " " + sn + "." + dialog.SafeFileName;
                        FileInfo fi = new FileInfo(dialog.FileName);
                        fi.CopyTo(resimyolu);
                        profilresim = tarih + " " + sn + "." + dialog.SafeFileName;



                    }
                    try
                    {
                        baglanti.Open();
                        OleDbCommand sorgu = new OleDbCommand("UPDATE kullanicilar SET kuladi=@kuladi,kulisim=@kulisim,kulsoyisim=@kulsoyisim,  " +
                            "kule_mail=@kule_mail,kulgsm=@kulgsm,kulil=@kulil,kulilce=@kulilce,kuladres=@kuladres,cinsiyet=@cinsiyet,yedekdurumu=@yedekdurumu,yedekyolu=@yedekyolu,kulresim=@kulresim  " +
                            "WHERE kulid like'" + kulid.ToString() + "'", baglanti);
                        sorgu.Parameters.AddWithValue("kuladi", textEdit6.Text);
                        sorgu.Parameters.AddWithValue("kulisim", textEdit1.Text);
                        sorgu.Parameters.AddWithValue("kulsoyisim", textEdit2.Text);
                        sorgu.Parameters.AddWithValue("kule_mail", textEdit3.Text);
                        sorgu.Parameters.AddWithValue("kulgsm", textEdit4.Text);
                        sorgu.Parameters.AddWithValue("kulil", comboBoxEdit1.Text);
                        sorgu.Parameters.AddWithValue("kulilce", textEdit5.Text);
                        sorgu.Parameters.AddWithValue("kuladres", memoEdit1.Text);
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            sorgu.Parameters.AddWithValue("cinsiyet", "Kadın");
                        }
                        else if (radioGroup1.SelectedIndex == 1)
                        {
                            sorgu.Parameters.AddWithValue("cinsiyet", "Erkek");
                        }
                        if (checkEdit1.Checked == true)
                        {
                            sorgu.Parameters.AddWithValue("yedekdurumu", "Alınsın");
                            sorgu.Parameters.AddWithValue("yedekyolu", labelControl14.Text);
                        }
                        else if (checkEdit1.Checked == false)
                        {
                            sorgu.Parameters.AddWithValue("yedekdurumu", "Alınmasın");
                            sorgu.Parameters.AddWithValue("yedekyolu", "");
                        }
                        sorgu.Parameters.AddWithValue("kulresim", profilresim.ToString());
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
                catch
                {

                }
            }


        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            guncelle();
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void pictureEdit1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DialogResult dr = new DialogResult();
                dialog.Filter="(*jpg)|*.jpg|(*png)|*.png";
                dr = dialog.ShowDialog();
                pictureEdit1.Image = Image.FromFile(dialog.FileName);

            }
            catch
            { 

            }

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
       
        private void checkEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            if(checkEdit1.Checked==false)
            {
                FolderBrowserDialog fdb = new FolderBrowserDialog();
                if(fdb.ShowDialog()==DialogResult.OK)
                {
                    labelControl14.Text = fdb.SelectedPath.ToString();
                    checkEdit1.Checked = true;
                }
            }
        }
 
    }
}