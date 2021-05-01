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
    public partial class Nouvelle_commande : Form
    {
        commerciaEntities db = new commerciaEntities();
        string tp;
        int num;

        public Nouvelle_commande(string type, int ID)
        {
            InitializeComponent();
            tp = type;
            num = ID;
            rempli_form();

            if (combofourn.Text != "")
            {
                int CMA = int.Parse(combofourn.Text);
                rempli(CMA);
            }


        }
        public void rempli_form()
        {

            if (tp == "fr")
            {
                combofourn.DataSource = db.commande_achat.Where(w => w.fournisseur1.Num_F == num).Select(s => s.Num_C_A).ToList();
            }
            else if (tp == "cl")
            {
                combofourn.DataSource = db.commande_vente.Where(w => w.client1.Num_C == num).Select(s => s.Num_C_V).ToList();
            }
        }
        public void rempli(int IDCMA)
        {


            dataGridView2.DataSource = db.ligne_commande_achat.Where(w => w.Commande_A == IDCMA).Select(s => new { s.produit1.Libelle, s.produit1.Prix, s.produit1.Quantite, Soustotal = s.produit1.Quantite * s.produit1.Prix }).ToList();


            label2.Text = db.ligne_commande_achat.Where(w => w.Commande_A == IDCMA).Select(s => s.produit1.Quantite * s.produit1.Prix).Sum().ToString();

        }
        private void Nouvelle_commande_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {


            if (combofourn.Text != "")
            {
                short CMA = Convert.ToInt16(combofourn.Text);
                Ajoute_Produits_a_commande aj = new Ajoute_Produits_a_commande(tp, CMA, num);

                aj.ShowDialog();
            }



        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btn_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (combofourn.Text != "")
            {

                if (MessageBox.Show("voulez vous supprimer?", "supprimer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dataGridView2.Rows.Count != 0)
                    {
                        string pro = dataGridView2.CurrentRow.Cells[0].Value.ToString();


                        short id = Convert.ToInt16(combofourn.Text);
                        if (tp == "fr")
                        {

                            ligne_commande_achat count = db.ligne_commande_achat.Where(w => w.Commande_A == id && w.produit1.Libelle == pro).FirstOrDefault();
                            if (count != null)
                            {
                                db.ligne_commande_achat.Remove(count);
                                db.SaveChanges();
                                MessageBox.Show("bien supprime", "", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            }
                            else
                            {
                                MessageBox.Show("le produit n'existe pas dans cette commande", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }


                        }
                        else if (tp == "cl")
                        {

                            ligne_commande_vente count = db.ligne_commande_vente.Where(w => w.Commande_V == id && w.produit1.Libelle == pro).FirstOrDefault();
                            if (count != null)
                            {
                                db.ligne_commande_vente.Remove(count);
                                db.SaveChanges();
                                MessageBox.Show("bien supprime", "", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            }
                            else
                            {
                                MessageBox.Show("le produit n'existe pas dans cette commande ");
                            }

                        }

                    }
                }
            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (combofourn.Text != "")
            {
            }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {

            if (combofourn.Text != "")
            {
                short id = Convert.ToInt16(combofourn.Text);
                if (tp == "fr")
                {

                    commande_achat ca = db.commande_achat.Where(w => w.Num_C_A == id).FirstOrDefault();
                    if (ca != null)
                    {
                        db.commande_achat.Remove(ca);
                        db.SaveChanges();
                        MessageBox.Show("bien supprime", "supprimer commande", MessageBoxButtons.OK);
                    }


                }
                else if (tp == "cl")
                {

                    commande_vente cv = db.commande_vente.Where(w => w.Num_C_V == id).FirstOrDefault();
                    if (cv != null)
                    {
                        db.commande_vente.Remove(cv);
                        db.SaveChanges();
                        MessageBox.Show("bien supprime", "supprimer commande", MessageBoxButtons.OK);
                    }

                }
                rempli_form();
            }
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            short id = Convert.ToInt16(num);
            if (tp == "fr")
            {

                commande_achat ca = new commande_achat();
                ca.Date_C = DateTime.Now;
                ca.Fournisseur = id;
                db.commande_achat.Add(ca);
                db.SaveChanges();

                MessageBox.Show("bien ajoute", "ajouter commande", MessageBoxButtons.OK);
            }
            else if (tp == "cl")
            {

                commande_vente ca = new commande_vente();
                ca.Date_C = DateTime.Now;
                ca.Client = id;
                db.commande_vente.Add(ca);
                db.SaveChanges();
                MessageBox.Show("bien ajoute", "ajouter commande", MessageBoxButtons.OK);
            }
            rempli_form();
        }

        private void comboPRD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tp == "fr")
            {
                int CMA = int.Parse(combofourn.Text);
                commande_achat f = db.commande_achat.Where(w => w.Num_C_A == CMA).Single();
                if (f != null)
                {
                    dateTimePicker1.Value = f.Date_C.Value;
                }
                rempli(CMA);
            }
            else if (tp == "cl")
            {
                int CMA = int.Parse(combofourn.Text);
                commande_vente f = db.commande_vente.Where(w => w.Num_C_V == CMA).Single();
                if (f != null)
                {
                    dateTimePicker1.Value = f.Date_C.Value;
                }
                rempli(CMA);
            }
        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            if (combofourn.Text != "")
            {
                short id = Convert.ToInt16(combofourn.Text);
                if (tp == "fr")
                {

                    commande_achat ca = db.commande_achat.Where(w => w.Num_C_A == id).FirstOrDefault();
                    if (ca != null)
                    {
                        ca.Date_C = dateTimePicker1.Value;
                        db.SaveChanges();
                        MessageBox.Show("bien modife", "modifer commande", MessageBoxButtons.OK);
                    }


                }
                else if (tp == "cl")
                {

                    commande_vente cv = db.commande_vente.Where(w => w.Num_C_V == id).FirstOrDefault();
                    if (cv != null)
                    {
                        cv.Date_C = dateTimePicker1.Value;
                        db.SaveChanges();
                        MessageBox.Show("bien modife", "modifer commande", MessageBoxButtons.OK);
                    }

                }
                rempli_form();
            }
        }
    }
}
