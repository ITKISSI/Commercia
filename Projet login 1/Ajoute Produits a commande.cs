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
    public partial class Ajoute_Produits_a_commande : Form
    {
        short numCOM;
        string tp;
        int IDpersonne;
        commerciaEntities db = new commerciaEntities();
        public Ajoute_Produits_a_commande(string typ,short numcommand,int IDP)
        {
            InitializeComponent();
            numCOM = numcommand;
            tp = typ;
            IDpersonne = IDP;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Nouvelle_commande nv = new Nouvelle_commande(tp, IDpersonne);
            nv.Show();
            this.Close();
        }

        private void Ajoute_Produits_a_commande_Load(object sender, EventArgs e)
        {
            comboTYP.DataSource = db.type_produit.Select(s=>s.Libelle).ToList();
            
        }

        private void comboTYP_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboPRD.DataSource = db.produit.Where(w => w.Type_P == comboTYP.Text).ToList();
            comboPRD.DisplayMember = "Libelle";
            comboPRD.ValueMember = "Num_P";
        }

        private void comboPRD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pr = Convert.ToInt32(comboPRD.SelectedValue);
            var p = db.produit.Where(w => w.Num_P == pr).Select(s => s.Prix).Single();
            label2.Text = (int.Parse(p.Value.ToString()) * int.Parse(Qt.Value.ToString())).ToString()+"DH";
            }
            catch (Exception)
            {

                
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int pr = Convert.ToInt32(comboPRD.SelectedValue);
                var p = db.produit.Where(w => w.Num_P == pr).Select(s => s.Prix).Single();
                label2.Text = (int.Parse(p.Value.ToString()) * int.Parse(Qt.Value.ToString())).ToString()+"DH";
            }
            catch (Exception)
            {

                
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                string type = comboTYP.Text;
                short pr = Convert.ToInt16(comboPRD.SelectedValue);
                short nc = short.Parse(numCOM.ToString());
                if (tp == "fr")
                {
                    ligne_commande_achat ca = new ligne_commande_achat();
                    ca.Produit = pr;
                    ca.Quantite = int.Parse(Qt.Value.ToString());
                    ca.Commande_A = numCOM;
                    db.ligne_commande_achat.Add(ca);
                    db.SaveChanges();
                    
                    this.Close();
                }
                else if (tp == "cl")
                {
                    ligne_commande_vente ca = new ligne_commande_vente();
                    ca.Produit = pr;
                    ca.Quantite = int.Parse(Qt.Value.ToString());
                    ca.Commande_V = numCOM;
                    db.ligne_commande_vente.Add(ca);
                    db.SaveChanges();
                   
                    this.Close();
                }
            }
            catch (Exception)
            {

               
            }
            

        }

        private void gunaNumeric1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int pr = Convert.ToInt32(comboPRD.SelectedValue);
                var p = db.produit.Where(w => w.Num_P == pr).Select(s => s.Prix).Single();
                label2.Text = (int.Parse(p.Value.ToString()) * int.Parse(Qt.Value.ToString())).ToString() + "DH";
            }
            catch (Exception)
            {


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboPRD.DataSource = db.produit.Where(w => w.Type_P == comboTYP.Text).ToList();
            comboPRD.DisplayMember = "Libelle";
            comboPRD.ValueMember = "Num_P";
        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pr = Convert.ToInt32(comboPRD.SelectedValue);
                var p = db.produit.Where(w => w.Num_P == pr).Select(s => s.Prix).Single();
                label2.Text = (int.Parse(p.Value.ToString()) * int.Parse(Qt.Value.ToString())).ToString() + "DH";
            }
            catch (Exception)
            {


            }
        }
    }
}
