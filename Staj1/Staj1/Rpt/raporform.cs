using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Staj1
{
    public partial class raporform : DevExpress.XtraEditors.XtraForm
    {
        string degisken,deger1;
        public raporform(string degiskenim,string degerim1)
        {
            InitializeComponent();
            deger1 = degerim1;
            degisken = degiskenim;

        }
        ReportDocument cryRpt = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;   
        private void raporform_Load(object sender, EventArgs e)
        {
            
            CrystalDecisions.Shared.ParameterDiscreteValue gelen = new CrystalDecisions.Shared.ParameterDiscreteValue();
            CrystalDecisions.Shared.ParameterValues gelen1=new CrystalDecisions.Shared.ParameterValues();
            //crConnectionInfo.Password = "1234";
            if (degisken == "1")
            {
                string al = Application.StartupPath + "\\Rpt\\AracListesi.rpt";
                cryRpt.Load(al);
                
            }
            else if (degisken == "2")
            {
                string al = Application.StartupPath + "\\Rpt\\aracyakit.rpt";
                cryRpt.Load(al);
                gelen.Value = deger1;
                gelen1.Add(gelen);
                cryRpt.DataDefinition.ParameterFields["id"].ApplyCurrentValues(gelen1);
                
            }
            CrTables = cryRpt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);

            }
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}