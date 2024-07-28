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
    public partial class aracyakitfisi : DevExpress.XtraEditors.XtraForm
    {
        string baglanticümlecigi, aracid;
        public aracyakitfisi(string baglanticümlecigim, string aracidim)
        {
            InitializeComponent();
            baglanticümlecigi = baglanticümlecigim;
            aracid = aracidim;
        }
        OleDbConnection baglanti = new OleDbConnection();
        private void aracyakitfisi_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = baglanticümlecigi.ToString();
            simpleButton2.Enabled = false;
            vericek();
        }
        public void aracyakıtekle()
        {
            try
            {
                if (textEdit1.Text == "" || dateEdit1.Text == "")
                {
                    XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez \n  Lütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO aracyakit (aracid,fisno,tarih,litre,tutar,alıcı,acıklama) VALUES (@aracid,@fisno,@tarih,@litre,@tutar,@alıcı,@acıklama) ", baglanti);
                    komut.Parameters.Add("aracid", OleDbType.VarChar).Value = aracid.ToString();
                    komut.Parameters.Add("fisno", OleDbType.VarChar).Value = textEdit1.Text;
                    komut.Parameters.Add("tarih", OleDbType.VarChar).Value = dateEdit1.Text;
                    komut.Parameters.Add("litre", OleDbType.VarChar).Value = textEdit3.Text;
                    komut.Parameters.Add("tutar", OleDbType.VarChar).Value = textEdit4.Text;
                    komut.Parameters.Add("alıcı", OleDbType.VarChar).Value = textEdit2.Text;
                    komut.Parameters.Add("acıklama", OleDbType.VarChar).Value = memoEdit1.Text;


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
        public void vericek()
        {
            try
            {
                string sorgu = "SELECT * FROM aracyakit WHERE aracid like'" + aracid.ToString() + "'";
                baglanti.Open();
                OleDbCommand veri = new OleDbCommand(sorgu, baglanti);
                OleDbDataReader oku = veri.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Fiş Numarası", Type.GetType("System.String"));               
                dt.Columns.Add("Tarih", Type.GetType("System.String"));
                dt.Columns.Add("Litre", Type.GetType("System.String"));
                dt.Columns.Add("Tutar", Type.GetType("System.String"));
                dt.Columns.Add("Alıcı", Type.GetType("System.String"));
                dt.Columns.Add("Açıklama", Type.GetType("System.String"));
                dt.Columns.Add("Araç id", Type.GetType("System.String"));
                dt.Columns.Add("id", Type.GetType("System.String"));
           
                while (oku.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = oku["fisno"].ToString();                    
                    dr[1] = oku["tarih"].ToString();
                    dr[2] = oku["litre"].ToString();
                    dr[3] = oku["tutar"].ToString();
                    dr[4] = oku["alıcı"].ToString();
                    dr[5] = oku["acıklama"].ToString();
                    dr[6] = oku["aracid"].ToString();
                    dr[7] = oku["id"].ToString();
                    
                    dt.Rows.Add(dr);
                }
                gridControl1.DataSource = dt;              
                baglanti.Close();
               

            }
            catch
            {
                baglanti.Close();

            }
        }
        public void aracyakitguncelle()
        {

            if (textEdit1.Text == "" || dateEdit1.Text == "")
            {
                XtraMessageBox.Show("Yıldız ile gösterilen alanlar boş geçilemez \n  Lütfen yıldızlı alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {

                try
                {
                    baglanti.Open();
                    OleDbCommand sorgu = new OleDbCommand("UPDATE aracyakit SET fisno=@fisno,tarih=@tarih,litre=@litre,tutar=@tutar, " +
                        "alıcı=@alıcı,acıklama=@acıklama " +
                        "WHERE id like'" + yakitid.ToString() + "'", baglanti);
                    sorgu.Parameters.AddWithValue("fisno", textEdit1.Text);                   
                    sorgu.Parameters.AddWithValue("tarih", dateEdit1.Text);
                    sorgu.Parameters.AddWithValue("litre", textEdit3.Text);
                    sorgu.Parameters.AddWithValue("tutar", textEdit4.Text);
                    sorgu.Parameters.AddWithValue("alıcı", textEdit2.Text);
                    sorgu.Parameters.AddWithValue("acıklama", memoEdit1.Text);
                    
                    if (sorgu.ExecuteNonQuery() == 1)
                    {
                        XtraMessageBox.Show("Güncelleme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        textEdit1.Text = "";                        
                        dateEdit1.Text = "";
                        textEdit3.Text = "";
                        textEdit4.Text = "";
                        textEdit2.Text = "";
                        memoEdit1.Text = "";

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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            aracyakıtekle();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            aracyakitguncelle();
            vericek();
            simpleButton2.Enabled = false;
            simpleButton1.Enabled = true;
        }


        string yakitid;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
 
            simpleButton2.Enabled = true;
            simpleButton1.Enabled = false;
            textEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Fiş Numarası"));           
            dateEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tarih"));
            textEdit3.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Litre"));
            textEdit4.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tutar"));
            textEdit2.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Alıcı"));
            memoEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Açıklama"));
            yakitid = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id"));
           

            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("DELETE from aracyakit where id like '"+Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id"))+ "'", baglanti);
               

                if (sorgu.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Silme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    baglanti.Close();
                    vericek();

                }
                else
                {
                    XtraMessageBox.Show("Silme işlemi başarısız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                baglanti.Close();

            }
            catch
            {

            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && gridView1.SelectedRowsCount == 1)
            {
               
                Menü.ShowPopup(MousePosition);
            }

        }
    }
}