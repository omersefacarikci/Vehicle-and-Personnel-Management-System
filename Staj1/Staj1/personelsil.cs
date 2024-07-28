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
    public partial class personelsil : DevExpress.XtraEditors.XtraForm
    {
        OleDbDataReader rr;
        public personelsil()
        {
            InitializeComponent();
            
        }

        private void txttc_TextChanged(object sender, EventArgs e)
        {
            if (txttc.Text.Length==11)
            {
                    
                OleDbConnection tcc = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Data.mdb;Jet OLEDB:Database Password=");
                tcc.Open();
                OleDbCommand cmd = new OleDbCommand("Select PersonelAdi,PersonelSoyadi from personel where Tcno=@p1",tcc);
                cmd.Parameters.AddWithValue("@p1", txttc.Text);
                rr=cmd.ExecuteReader();
                if(rr.Read())
                {
                    textBox2.Text = rr["PersonelAdi"].ToString();
                    textBox3.Text = rr["PersonelSoyadi"].ToString();
                    //textBox2.Text = rr.IsDBNull(0)?null:rr.GetString(0); 2.yol

                }
                rr.Close();
                tcc.Close();


            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult db =  MessageBox.Show("Silmek istediğinize emin misiniz? ", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (db == System.Windows.Forms.DialogResult.Yes)
            {
                OleDbConnection tcc = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Data.mdb;Jet OLEDB:Database Password=");
                tcc.Open();
                OleDbCommand cmd = new OleDbCommand("delete from personel where Tcno=@p1", tcc);
                cmd.Parameters.AddWithValue("@p1", txttc.Text);
                cmd.ExecuteNonQuery();
                tcc.Close();
                MessageBox.Show("Personel silindi. ", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}