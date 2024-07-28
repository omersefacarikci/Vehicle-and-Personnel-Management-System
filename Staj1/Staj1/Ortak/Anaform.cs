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
using DevExpress.XtraGrid;

namespace Staj1
{
    public partial class Anaform : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi, kulid;

        public Anaform(string sqlSorgu, string kulidm)
        {
            InitializeComponent();
            baglanticümlecigi = sqlSorgu;
            kulid = kulidm;

        }
        public OleDbConnection baglanti = new OleDbConnection();
        string kullaniciAdiniz;
        public void kulBilgisi()
        {
            try
            {
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'" + kulid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    kullaniciAdiniz = oku["kuladi"].ToString() + " " + oku["kulsoyisim"].ToString();
                    tematool.LookAndFeel.SkinName = oku["kultema"].ToString();
                }
                oku.Close();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();

            }

        }

        private void Anaform_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            kulBilgisi();
            this.Text = "Anaform(" + kullaniciAdiniz + ")";
        }
        public void yedekleme()
        {
            try
            {
                string yedekdurumu, dizimdurumu;
                string tarih = DateTime.Now.ToString("dd.MM.yyyy");
                string sorgu = "SELECT * FROM kullanicilar WHERE kulid like'" + kulid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                while (oku.Read())
                {
                    yedekdurumu = oku["yedekdurumu"].ToString();
                    dizimdurumu = oku["yedekyolu"].ToString();
                    if (yedekdurumu == "Alınsın")
                    {
                        string mevcutdatayeri = Environment.CurrentDirectory + @"/Data.mdb";
                        File.Copy(mevcutdatayeri, dizimdurumu + @"/" + tarih + " " + "backup.mdb", true);
                        XtraMessageBox.Show("Veri tabanı yedeği alındı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Dispose();
                        Application.Exit();
                    }
                    else
                    {
                        Dispose();
                        Application.Exit();
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
        public void tema()
        {
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update kullanicilar set kultema=@kultema where kulid like  '" + kulid.ToString() + "'", baglanti);
                komut.Parameters.AddWithValue("kultema", tematool.LookAndFeel.SkinName);
                komut.ExecuteNonQuery();
                baglanti.Close();

            }
            catch
            {
                baglanti.Close();
            }
        }
        public void aktifaraclistesi()
        {
            string sorgu = "SELECT* FROM araclar WHERE aktiflik like '1' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Plaka", Type.GetType("System.String"));
            dt.Columns.Add("Araç Markası", Type.GetType("System.String"));
            dt.Columns.Add("Araç Modeli", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yılı", Type.GetType("System.String"));
            dt.Columns.Add("Araç Rengi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yakıt Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Vites Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Şase No", Type.GetType("System.String"));
            dt.Columns.Add("Açıklama", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["aracplaka"].ToString();
                dr[1] = oku["aracmarka"].ToString();
                dr[2] = oku["aracmodel"].ToString();
                dr[3] = oku["aracyılı"].ToString();
                dr[4] = oku["aracrengi"].ToString();
                dr[5] = oku["aractipi"].ToString();
                dr[6] = oku["kayıttarihi"].ToString();
                dr[7] = oku["yakıt"].ToString();
                dr[8] = oku["vites"].ToString();
                dr[9] = oku["şaseno"].ToString();
                dr[10] = oku["açıklama"].ToString();
                dr[11] = oku["id"].ToString();
                dr[12] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView1.Columns["İD"].Visible = false;
            gridView1.Columns["aktiflik"].Visible = false;



        }
        public void pasifaraclistesi()
        {
            string sorgu = "SELECT* FROM araclar WHERE aktiflik like '0' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Plaka", Type.GetType("System.String"));
            dt.Columns.Add("Araç Markası", Type.GetType("System.String"));
            dt.Columns.Add("Araç Modeli", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yılı", Type.GetType("System.String"));
            dt.Columns.Add("Araç Rengi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Yakıt Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Vites Tipi", Type.GetType("System.String"));
            dt.Columns.Add("Araç Şase No", Type.GetType("System.String"));
            dt.Columns.Add("Açıklama", Type.GetType("System.String"));
            dt.Columns.Add("İD", Type.GetType("System.String"));
            dt.Columns.Add("aktiflik", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["aracplaka"].ToString();
                dr[1] = oku["aracmarka"].ToString();
                dr[2] = oku["aracmodel"].ToString();
                dr[3] = oku["aracyılı"].ToString();
                dr[4] = oku["aracrengi"].ToString();
                dr[5] = oku["aractipi"].ToString();
                dr[6] = oku["kayıttarihi"].ToString();
                dr[7] = oku["yakıt"].ToString();
                dr[8] = oku["vites"].ToString();
                dr[9] = oku["şaseno"].ToString();
                dr[10] = oku["açıklama"].ToString();
                dr[11] = oku["id"].ToString();
                dr[12] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView1.Columns["İD"].Visible = false;
            gridView1.Columns["aktiflik"].Visible = false;
        }
        public void calısanpersonellistesi()
        {
            string sorgu = "SELECT* FROM personel WHERE aktiflik like '1' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Personel Adı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Soyadı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Tc Kimlik Numarası", Type.GetType("System.String"));
            dt.Columns.Add("Personel Cinsiyeti", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tutarı", Type.GetType("System.String"));
            dt.Columns.Add("id", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Çalışma Durumu", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["PersonelAdi"].ToString();
                dr[1] = oku["PersonelSoyadi"].ToString();
                dr[2] = oku["DogumTarihi"].ToString();
                dr[3] = oku["Tcno"].ToString();
                dr[4] = oku["PersonelCinsiyet"].ToString();
                dr[5] = oku["PersonelKayıtTarihi"].ToString();
                dr[6] = oku["Maas_Tarihi"].ToString();
                dr[7] = oku["Maas_Tutari"].ToString();
                dr[8] = oku["id"].ToString();
                dr[9] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl2.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView2.Columns["id"].Visible = false;
            gridView2.Columns["Personelin Çalışma Durumu"].Visible = false;

        }
        public void izinlipersonellistesi()
        {
            string sorgu = "SELECT* FROM personel WHERE aktiflik like '0' ORDER BY id desc";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Personel Adı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Soyadı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Tc Kimlik Numarası", Type.GetType("System.String"));
            dt.Columns.Add("Personel Cinsiyeti", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tutarı", Type.GetType("System.String"));
            dt.Columns.Add("id", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Çalışma Durumu", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["PersonelAdi"].ToString();
                dr[1] = oku["PersonelSoyadi"].ToString();
                dr[2] = oku["DogumTarihi"].ToString();
                dr[3] = oku["Tcno"].ToString();
                dr[4] = oku["PersonelCinsiyet"].ToString();
                dr[5] = oku["PersonelKayıtTarihi"].ToString();
                dr[6] = oku["Maas_Tarihi"].ToString();
                dr[7] = oku["Maas_Tutari"].ToString();
                dr[8] = oku["id"].ToString();
                dr[9] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl2.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView2.Columns["id"].Visible = false;
            gridView2.Columns["Personelin Çalışma Durumu"].Visible = false;
        }
        public void personelmaasbilgileri()
        {
            string sorgu = "SELECT* FROM personel";
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            OleDbDataReader oku = null;
            oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Personel Adı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Soyadı", Type.GetType("System.String"));
            dt.Columns.Add("Personel Doğum Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Tc Kimlik Numarası", Type.GetType("System.String"));
            dt.Columns.Add("Personel Cinsiyeti", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Kayıt Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tarihi", Type.GetType("System.String"));
            dt.Columns.Add("Maaş Tutarı", Type.GetType("System.String"));
            dt.Columns.Add("id", Type.GetType("System.String"));
            dt.Columns.Add("Personelin Çalışma Durumu", Type.GetType("System.String"));

            while (oku.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = oku["PersonelAdi"].ToString();
                dr[1] = oku["PersonelSoyadi"].ToString();
                dr[2] = oku["DogumTarihi"].ToString();
                dr[3] = oku["Tcno"].ToString();
                dr[4] = oku["PersonelCinsiyet"].ToString();
                dr[5] = oku["PersonelKayıtTarihi"].ToString();
                dr[6] = oku["Maas_Tarihi"].ToString();
                dr[7] = oku["Maas_Tutari"].ToString();
                dr[8] = oku["id"].ToString();
                dr[9] = oku["aktiflik"].ToString();
                dt.Rows.Add(dr);
            }
            gridControl2.DataSource = dt;
            oku.Close();
            baglanti.Close();
            gridView2.Columns["id"].Visible = false;
            gridView2.Columns["Personelin Çalışma Durumu"].Visible = false;
        }
        private void Anaform_FormClosing(object sender, FormClosingEventArgs e)
        {

            yedekleme();
            tema();


        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kullaniciprofili aç = new kullaniciprofili(baglanticümlecigi, kulid);
            aç.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            profilguncelleme aç = new profilguncelleme(baglanticümlecigi, kulid);
            aç.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult sonuc = new DialogResult();
            sonuc = XtraMessageBox.Show("Program kapatılacaktır çıkmak istediğinizden emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (sonuc == DialogResult.No)
            {

            }
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string datapath = Environment.CurrentDirectory + @"/Data.mdb";
                string dosyaadı = DateTime.Now.ToString("dd.MM.yyyy");
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string backuppath = fbd.SelectedPath.ToString();
                    File.Copy(datapath, backuppath + @"/" + dosyaadı + " " + "backup.mdb", true);
                    XtraMessageBox.Show("Veri tabanı yedeği alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch
            {

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
        private void navButton3_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            arackayıt ac = new arackayıt(baglanticümlecigi, kulid, "1", null);
            ac.ShowDialog();

        }
        private void navButton1_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            personelkayıt ac = new personelkayıt(baglanticümlecigi, kulid, "1", null);
            ac.ShowDialog();
        }
        private void backstageViewTabItem1_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            calısanpersonellistesi();
        }

        private void navButton2_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            aktifaraclistesi();
        }

        private void navButton4_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            pasifaraclistesi();
        }
        private void backstageViewTabItem2_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            izinlipersonellistesi();
        }
        private void navButton6_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            calısanpersonellistesi();
        }
        private void navButton7_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            izinlipersonellistesi();
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && gridView1.SelectedRowsCount == 1)
            {
                if (Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "aktiflik")) == "1")
                {
                    barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else if (Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "aktiflik")) == "0")
                {
                    barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                AracMenu.ShowPopup(MousePosition);
            }

        }
        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && gridView2.SelectedRowsCount == 1)
            {
                if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "aktiflik")) == "1")
                {
                    barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "aktiflik")) == "0")
                {
                    barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                PersonelMenu.ShowPopup(MousePosition);
            }

        }
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            arackayıt ac = new arackayıt(baglanticümlecigi, kulid, "2", Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();
        }
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            personelkayıt ac = new personelkayıt(baglanticümlecigi, kulid, "2", Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "İD")));
            ac.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE araclar SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "1");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Araç kayıtı aktif edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Araç kayıtı aktif edilemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                pasifaraclistesi();
            }
            catch
            {

            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE araclar SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "0");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Araç kayıtı pasif edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Araç kayıtı pasif edilemedi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                aktifaraclistesi();
            }
            catch
            {

            }
        }
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "id")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "1");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Personel çalışan olarak ayarlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Personel çalışan olarak ayarlanamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                izinlipersonellistesi();
            }
            catch
            {

            }
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("UPDATE personel SET aktiflik=@aktiflik WHERE id like'" + Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "id")) + "'", baglanti);
                sorgu.Parameters.AddWithValue("aktiflik", "0");
                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Personel izinli olarak ayarlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else
                {
                    XtraMessageBox.Show("Personel izinli olarak ayarlanamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();
                calısanpersonellistesi();
            }
            catch
            {

            }
        }

        private void navButton5_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            raporform aç = new raporform("1", "1");
            aç.ShowDialog();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            aracyakitfisi ac = new aracyakitfisi(baglanticümlecigi, Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            ac.ShowDialog();

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            raporform aç = new raporform("2", Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "İD")));
            aç.ShowDialog();
        }

        private void navButton6_ElementClick_1(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            calısanpersonellistesi();
        }

        private void navButton7_ElementClick_1(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            izinlipersonellistesi();
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {

        }

        private void gridControl2_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && gridView2.SelectedRowsCount == 1)
            {
                if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Personelin Çalışma Durumu")) == "1")
                {
                    barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else if (Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Personelin Çalışma Durumu")) == "0")
                {
                    barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                PersonelMenu.ShowPopup(MousePosition);
            }
        }

        private void navButton10_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            personelmaasbilgileri();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            personelsil ps = new personelsil();
            ps.ShowDialog();
        }

    } 
    
}