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
    public partial class arackayıt : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi,kulid,deger,aracid;

        public arackayıt(string baglanticümlecigim, string kulidm, string degerim, string aracidim)
        {
            InitializeComponent();
            baglanticümlecigi = baglanticümlecigim;
            kulid = kulidm;
            deger = degerim;
            aracid = aracidim;
        }
        OleDbConnection baglanti = new OleDbConnection();    
        private void arackayıt_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            if(deger.ToString()=="1")
            {
                simpleButton1.Text = "Kaydet";
            }
            else if (deger.ToString() == "2")
            {
                simpleButton1.Text = "Güncelle";
                vericek();
            }
        }
        public void vericek()
        {
            try
            {
                string sorgu = "SELECT * FROM araclar WHERE id like'" + aracid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    textEdit1.Text = oku["aracplaka"].ToString();
                    textEdit2.Text = oku["aracmarka"].ToString();
                    textEdit3.Text = oku["aracmodel"].ToString();
                    textEdit4.Text = oku["aracyılı"].ToString();
                    textEdit5.Text = oku["aracrengi"].ToString();
                    textEdit6.Text = oku["aractipi"].ToString();
                    textEdit7.Text = oku["kayıttarihi"].ToString();

                    if(oku["yakıt"].ToString()=="Benzin")
                    {
                        radioGroup1.SelectedIndex = 0;
                    }
                    else if (oku["yakıt"].ToString() == "Dizel")
                    {
                        radioGroup1.SelectedIndex = 1;
                    }
                    else if (oku["yakıt"].ToString() == "Elektrik")
                    {
                        radioGroup1.SelectedIndex = 2;
                    }
                    else if (oku["yakıt"].ToString() == "Benzin & LPG")
                    {
                        radioGroup1.SelectedIndex = 3;
                    }

                    if (oku["vites"].ToString() == "Manuel")
                    {
                        radioGroup1.SelectedIndex = 0;
                    }
                    else if (oku["vites"].ToString() == "Otomatik")
                    {
                        radioGroup1.SelectedIndex = 1;
                    }
                    else if (oku["vites"].ToString() == "Yarı Otomatik")
                    {
                        radioGroup1.SelectedIndex = 2;
                    }
                    textEdit8.Text = oku["şaseno"].ToString();
                    memoEdit1.Text = oku["açıklama"].ToString();
                    
                    
                    
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }
        }
        public void aracekle()
        {
            try
            {
                if (textEdit1.Text == "" || textEdit2.Text == "" || textEdit3.Text == "")
                {
                    XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez. \nLütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO araclar (aracplaka,aracmarka,aracmodel,aracyılı,aracrengi,aractipi,kayıttarihi,yakıt,vites,şaseno,açıklama,aktiflik) VALUES (@aracplaka,@aracmarka,@aracmodel,@aracyılı,@aracrengi,@aractipi,@kayıttarihi,@yakıt,@vites,@şaseno,@açıklama,@aktiflik) ", baglanti);
                    komut.Parameters.Add("aracplaka", OleDbType.VarChar).Value = textEdit1.Text;
                    komut.Parameters.Add("aracmarka", OleDbType.VarChar).Value = textEdit2.Text;
                    komut.Parameters.Add("aracmodel", OleDbType.VarChar).Value = textEdit3.Text;
                    komut.Parameters.Add("aracyılı", OleDbType.VarChar).Value = textEdit4.Text;
                    komut.Parameters.Add("aracrengi", OleDbType.VarChar).Value = textEdit5.Text;
                    komut.Parameters.Add("aractipi", OleDbType.VarChar).Value = textEdit6.Text;
                    komut.Parameters.Add("kayıttarihi", OleDbType.VarChar).Value = textEdit7.Text;


                    if (radioGroup1.SelectedIndex.ToString() == "0")
                    {
                        komut.Parameters.Add("yakıt", OleDbType.VarChar).Value = "Benzin";
                    }
                    else if (radioGroup1.SelectedIndex.ToString() == "1")
                    {
                        komut.Parameters.Add("yakıt", OleDbType.VarChar).Value = "Dizel";
                    }
                    else if (radioGroup1.SelectedIndex.ToString() == "2")
                    {
                        komut.Parameters.Add("yakıt", OleDbType.VarChar).Value = "Elektrik";
                    }
                    else if (radioGroup1.SelectedIndex.ToString() == "3")
                    {
                        komut.Parameters.Add("yakıt", OleDbType.VarChar).Value = "Benzin & LPG";
                    }
                    if (radioGroup2.SelectedIndex.ToString() == "0")
                    {
                        komut.Parameters.Add("vites", OleDbType.VarChar).Value = "Manuel";
                    }
                    else if (radioGroup2.SelectedIndex.ToString() == "1")
                    {
                        komut.Parameters.Add("vites", OleDbType.VarChar).Value = "Otomatik";
                    }
                    else if (radioGroup2.SelectedIndex.ToString() == "2")
                    {
                        komut.Parameters.Add("vites", OleDbType.VarChar).Value = "Yarı Otomatik";
                    }
                    komut.Parameters.Add("şaseno", OleDbType.VarChar).Value = textEdit8.Text;
                    komut.Parameters.Add("açıklama", OleDbType.VarChar).Value = memoEdit1.Text;
                    komut.Parameters.Add("aktiflik", OleDbType.VarChar).Value = "1";


                    if (komut.ExecuteNonQuery() == 1)
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        textEdit1.Text="";
                        textEdit2.Text="";
                        textEdit3.Text="";
                        textEdit4.Text="";
                        textEdit5.Text="";
                        textEdit6.Text="";
                        textEdit7.Text="";
                        textEdit8.Text="";
                        memoEdit1.Text="";

                    }
                    else
                    {
                        baglanti.Close();
                        XtraMessageBox.Show("Kayıt başarısız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        textEdit1.Text = "";
                        textEdit2.Text = "";
                        textEdit3.Text = "";
                        textEdit4.Text = "";
                        textEdit5.Text = "";
                        textEdit6.Text = "";
                        textEdit7.Text = "";
                        textEdit8.Text = "";
                        memoEdit1.Text = "";
                    }


                }
            }
            catch
            {
                baglanti.Close();
            }        
        }
        public void aracguncelle()
        {
       
            if (textEdit6.Text == "" || textEdit1.Text == "" || textEdit2.Text == "")
            {
                XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez. \nLütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
               
                    try
                    {
                        baglanti.Open();
                        OleDbCommand sorgu = new OleDbCommand("UPDATE araclar SET aracplaka=@aracplaka,aracmarka=@aracmarka,aracmodel=@aracmodel,  " +
                            "aracyılı=@aracyılı,aracrengi=@aracrengi,aractipi=@aractipi,kayıttarihi=@kayıttarihi,yakıt=@yakıt,vites=@vites,şaseno=@şaseno,açıklama=@açıklama " +
                            "WHERE id like'" + aracid.ToString() + "'", baglanti);
                        sorgu.Parameters.AddWithValue("aracplaka", textEdit1.Text);
                        sorgu.Parameters.AddWithValue("aracmarka", textEdit2.Text);
                        sorgu.Parameters.AddWithValue("aracmodel", textEdit3.Text);
                        sorgu.Parameters.AddWithValue("aracyılı", textEdit4.Text);
                        sorgu.Parameters.AddWithValue("aracrengi", textEdit5.Text);
                        sorgu.Parameters.AddWithValue("aractipi", textEdit6.Text);
                        sorgu.Parameters.AddWithValue("kayıttarihi", textEdit7.Text);
                        
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            sorgu.Parameters.AddWithValue("yakıt", "Benzin");
                        }
                        else if (radioGroup1.SelectedIndex == 1)
                        {
                            sorgu.Parameters.AddWithValue("yakıt", "Dizel");
                        }
                        else if (radioGroup1.SelectedIndex == 2)
                        {
                            sorgu.Parameters.AddWithValue("yakıt", "Elektrik");
                        }
                        else if (radioGroup1.SelectedIndex == 3)
                        {
                            sorgu.Parameters.AddWithValue("yakıt", "Benzin & LPG");
                        }

                        if (radioGroup2.SelectedIndex == 0)
                        {
                            sorgu.Parameters.AddWithValue("vites", "Manuel");
                        }
                        else if (radioGroup2.SelectedIndex == 1)
                        {
                            sorgu.Parameters.AddWithValue("vites", "Otomatik");
                        }
                        else if (radioGroup2.SelectedIndex == 2)
                        {
                            sorgu.Parameters.AddWithValue("vites", "Yarı Otomatik");
                        }
                        sorgu.Parameters.AddWithValue("şaseno", textEdit8.Text);
                        sorgu.Parameters.AddWithValue("açıklama", memoEdit1.Text);
                       
                        if (sorgu.ExecuteNonQuery() == 1)
                        {
                            XtraMessageBox.Show("Güncelleme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            textEdit1.Text = "";
                            textEdit2.Text = "";
                            textEdit3.Text = "";
                            textEdit4.Text = "";
                            textEdit5.Text = "";
                            textEdit6.Text = "";
                            textEdit7.Text = "";
                            textEdit8.Text = "";
                            memoEdit1.Text = "";
                            Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("Güncelleme işlemi başarısız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            textEdit1.Text = "";
                            textEdit2.Text = "";
                            textEdit3.Text = "";
                            textEdit4.Text = "";
                            textEdit5.Text = "";
                            textEdit6.Text = "";
                            textEdit7.Text = "";
                            textEdit8.Text = "";
                            memoEdit1.Text = "";
                        }
                        baglanti.Close();
                    }
                    catch
                    {

                    }

               
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (deger.ToString() == "1")
            {
                aracekle();
            }
            else if (deger.ToString() == "2")
            {
                aracguncelle();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }
       
    }
}