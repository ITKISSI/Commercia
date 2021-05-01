using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_login_1
{
    public partial class Form_bon_livraison : Form
    {
        public Form_bon_livraison(string nom,int ID)
        {
            InitializeComponent();
            if (nom == "client")
            {
                livrition bl = new livrition();
                bl.SetParameterValue("Num_C", ID);
                crystalReportViewer1.ReportSource = bl;
                crystalReportViewer1.Refresh();
            }
            else if(nom== "client_factur")
            {
                facteur bl = new facteur();
                bl.SetParameterValue("Num_C", ID);
                crystalReportViewer1.ReportSource = bl;
                crystalReportViewer1.Refresh();
            }
            else if(nom== "commande")
            {
                commande bl = new commande();
                bl.SetParameterValue("Num_F", ID);
                crystalReportViewer1.ReportSource = bl;
                crystalReportViewer1.Refresh();
            }
        }

        private void Form_bon_livraison_Load(object sender, EventArgs e)
        {
            //CrystalReport1 bl = new CrystalReport1();
            //bl.SetParameterValue("Num_C", 4);
            //crystalReportViewer1.ReportSource = bl;
            //crystalReportViewer1.Refresh();
        }
    }
}
