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
    public partial class Gestion_des_fournisseurs : Form
    {
        commerciaEntities db = new commerciaEntities();

        public Gestion_des_fournisseurs()
        {
            InitializeComponent();
            rempli();
        }
        public void rempli()
        {
          
                listBox1.DataSource = db.fournisseur.Select(s => s.Nom_Entreprise).ToList();
                
            



            var req = db.ligne_commande_achat.Select(s => new { s.commande_achat.Num_C_A, s.commande_achat.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
            dataGridView1.DataSource = req.ToList();
            var req1 = db.ligne_commande_achat.Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
            dataGridView2.DataSource = req1.ToList();
        }
        public bool verfier_combo()
        {
            if (textNOM_ET.Text != "" && textTEL.Text != "" && textVL.Text != "" && textEML.Text != "" && richTextADS.Text != "")
            {
                return true;
            }

            else return false;
        }
        bool mth;

      
       
       
        private void comboID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboID.Text);
            fournisseur fr = db.fournisseur.Where(w => w.Num_F == ID).FirstOrDefault();
            textNOM_ET.Text = fr.Nom_Entreprise;
           
            textTEL.Text = fr.Telephone_F;
            textVL.Text = fr.Ville_F;
            textEML.Text = fr.Email_F;
            richTextADS.Text = fr.Adresse_F;
        }
        public void updat()
        {
            String idEmp = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String nom = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            String prenom = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            String tele = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            String email = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            String ideq = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }
       

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    comboID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                }

            }
            catch (Exception)
            {

               
            }
        }

        

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void bunifuThinButton212_Click(object sender, EventArgs e)
        {
           
        }

       
        private void bButton1_Click(object sender, EventArgs e)
        {
            mth = true;
            string nom_ent = textNOM_ET.Text;
            string tel = textTEL.Text;
            string villle = textVL.Text;
            string email = textEML.Text;
            string adresse = richTextADS.Text;
            string ID = comboID.Text;


            ajoute_fournisseur aj = new ajoute_fournisseur(mth, ID, nom_ent, tel, email, villle, adresse);
            aj.ShowDialog();
        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            mth = false;
            string nom_ent = textNOM_ET.Text;
            string tel = textTEL.Text;
            string villle = textVL.Text;
            string email = textEML.Text;
            string adresse = richTextADS.Text;
            string ID = comboID.Text;

            ajoute_fournisseur aj = new ajoute_fournisseur(mth, ID, nom_ent, tel, email, villle, adresse);
            aj.ShowDialog();
        }

        private void bunifuThinButton26_Click_1(object sender, EventArgs e)
        {
            rempli();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboID.Text);

            if (comboID.Text != "")
            {
                DialogResult dialogresult = MessageBox.Show("voulez vous vraiment supprimer ce fournissuer ?", "voulez vous vraiment supprimer ce fournissuer ?", MessageBoxButtons.YesNo);
                if (dialogresult == DialogResult.Yes)
                {
                    fournisseur fr = db.fournisseur.Where(w => w.Num_F == ID).FirstOrDefault();
                    db.fournisseur.Remove(fr);
                    db.SaveChanges();
                    rempli();
                    MessageBox.Show("Bien supprime !!", "supprimer un fournisseur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogresult == DialogResult.No)
                {

                }
            }
            else MessageBox.Show("choissisez un numero de fournisseur SVP !!", "supprimer un fournisseur", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            fournisseur cl = db.fournisseur.Where(w => w.Nom_Entreprise == textrecherch.Text).FirstOrDefault();
          
            if (cl != null)
            {
                textrecherch.Clear();
                comboID.Text = cl.Num_F.ToString();
                textNOM_ET.Text = cl.Nom_Entreprise;
               
                textTEL.Text = cl.Telephone_F;
                textVL.Text = cl.Ville_F;
                textEML.Text = cl.Email_F;
                richTextADS.Text = cl.Adresse_F;
                var req = db.ligne_commande_achat.Where(w => w.commande_achat.fournisseur1.Num_F == cl.Num_F).Select(s => new { s.commande_achat.Num_C_A, s.commande_achat.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
                dataGridView1.DataSource = req.ToList();
                var req1 = db.ligne_commande_achat.Where(w => w.commande_achat.fournisseur1.Num_F == cl.Num_F).Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
                dataGridView2.DataSource = req1.ToList();
            }
            else { MessageBox.Show("le fournisseur n'existe pas !!", "", MessageBoxButtons.OK, MessageBoxIcon.Question); textrecherch.Clear(); }

        }

        private void Gestion_des_fournisseurs_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fournisseur cl = db.fournisseur.ToList().ElementAt(listBox1.SelectedIndex);
            if (cl != null)
            {
                comboID.Text = cl.Num_F.ToString();
                textNOM_ET.Text = cl.Nom_Entreprise;
               
                textTEL.Text = cl.Telephone_F;
                textVL.Text = cl.Ville_F;
                textEML.Text = cl.Email_F;
                richTextADS.Text = cl.Adresse_F;
                var req = db.ligne_commande_achat.Where(w => w.commande_achat.fournisseur1.Num_F == cl.Num_F).Select(s => new { s.commande_achat.Num_C_A, s.commande_achat.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
                dataGridView1.DataSource = req.ToList();
                var req1 = db.ligne_commande_achat.Where(w => w.commande_achat.fournisseur1.Num_F == cl.Num_F).Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
                dataGridView2.DataSource = req1.ToList();
            }
            else { MessageBox.Show(listBox1.SelectedItem.ToString()); }

        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(comboID.Text);
            string type = "fr";
            Nouvelle_commande n = new Nouvelle_commande(type,ID);
            n.ShowDialog();
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            Form_bon_livraison FR = new Form_bon_livraison("commande", int.Parse(comboID.Text));

            FR.Show();
        }
    }
}
