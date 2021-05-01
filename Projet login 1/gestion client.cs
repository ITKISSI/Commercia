using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_login_1
{
    public partial class Gestion_Client : Form
    {
        commerciaEntities db = new commerciaEntities();
        public Gestion_Client()
        {
            InitializeComponent();
        }
        public void rempli()
        {
           listBox1.DataSource = db.client.Select(s =>s.Nom+" "+s.Prenom ).ToList();
            
           
           
          
            var req = db.ligne_commande_vente.Select(s => new { s.commande_vente.Num_C_V, s.commande_vente.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
            dataGridView1.DataSource = req.ToList();
            var req1 = db.ligne_commande_vente.Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
            dataGridView2.DataSource = req1.ToList();
        }
        private void Assiette_Load(object sender, EventArgs e)
        {
            
            //dataGridView1.DataSource = db.client.Local.ToBindingList();

            //dataGridView1.Columns[7].Visible = false;
            rempli();
        }

        public bool verfier_combo()
        {
            if (comboID.Text!="" && textNOM.Text!="" && textPRN.Text!="" && textTEL.Text!="" && textVL.Text!="" && textEML.Text!="" && richTextADS.Text!="")
            {
                return true;
            }

           else return false;
        }
        bool mth;
       

      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                   // comboID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
            
        }

       

       

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
           
           
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            rempli();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
           
        }

        private void bButton1_Click_1(object sender, EventArgs e)
        {
           
            mth = true;
            string nom = textNOM.Text;
            string prenom = textPRN.Text;
            string tel = textTEL.Text;
            string villle = textVL.Text;
            string email = textEML.Text;
            string adresse = richTextADS.Text;
            string ID = comboID.Text;
          
            ajoute_client aj = new ajoute_client(mth, ID, nom, prenom, tel, email, villle, adresse);
           
            
            
            aj.ShowDialog();
        }
        
        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            mth = false;
            string nom = textNOM.Text;
            string prenom = textPRN.Text;
            string tel = textTEL.Text;
            string villle = textVL.Text;
            string email = textEML.Text;
            string adresse = richTextADS.Text;
            string ID = comboID.Text;

            ajoute_client aj = new ajoute_client(mth, ID, nom, prenom, tel, email, villle, adresse);
            
            aj.ShowDialog();

        }

        private void bunifuThinButton25_Click_1(object sender, EventArgs e)
        {
           
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
                client cl = db.client.Where(w => w.Num_C == ID).FirstOrDefault();
                db.client.Remove(cl);
                db.SaveChanges();
                rempli();
                MessageBox.Show("Bien supprime !!", "supprimer un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("choissisez un numero de client SVP !!", "supprimer un client", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void bunifuThinButton25_Click_2(object sender, EventArgs e)
        {
            int id = int.Parse(comboID.Text);
            string type = "cl";
            Nouvelle_commande nv = new Nouvelle_commande(type, id);
            nv.ShowDialog();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            
            Form_bon_livraison FR = new Form_bon_livraison("client",int.Parse(comboID.Text));
            
            FR.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            client cl = db.client.ToList().ElementAt(listBox1.SelectedIndex);
            if (cl != null)
            {
                comboID.Text = cl.Num_C.ToString();
                textNOM.Text = cl.Nom;
                textPRN.Text = cl.Prenom;
                textTEL.Text = cl.Telephone;
                textVL.Text = cl.Ville;
                textEML.Text = cl.Email;
                richTextADS.Text = cl.Adresse;
                var req = db.ligne_commande_vente.Where(w => w.commande_vente.client1.Num_C == cl.Num_C).Select(s => new { s.commande_vente.Num_C_V, s.commande_vente.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
                dataGridView1.DataSource = req.ToList();
                var req1 = db.ligne_commande_vente.Where(w => w.commande_vente.client1.Num_C == cl.Num_C).Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
                dataGridView2.DataSource = req1.ToList();
            }
            else {MessageBox.Show(listBox1.SelectedItem.ToString()); }
            
        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            client cl = db.client.Where(w => w.Nom == textrecherch.Text).FirstOrDefault();
            if (cl != null)
            {
                textrecherch.Clear();
                comboID.Text = cl.Num_C.ToString();
                textNOM.Text = cl.Nom;
                textPRN.Text = cl.Prenom;
                textTEL.Text = cl.Telephone;
                textVL.Text = cl.Ville;
                textEML.Text = cl.Email;
                richTextADS.Text = cl.Adresse;
                var req = db.ligne_commande_vente.Where(w => w.commande_vente.client1.Num_C == cl.Num_C).Select(s => new { s.commande_vente.Num_C_V, s.commande_vente.Date_C, Montant = s.Quantite * s.produit1.Prix }).ToList();
                dataGridView1.DataSource = req.ToList();
                var req1 = db.ligne_commande_vente.Where(w => w.commande_vente.client1.Num_C == cl.Num_C).Select(s => new { s.produit1.Libelle, s.Quantite, s.produit1.Prix }).ToList();
                dataGridView2.DataSource = req1.ToList();
            }
            else { MessageBox.Show("le client n'existe pas !!", "", MessageBoxButtons.OK, MessageBoxIcon.Question); textrecherch.Clear(); }

        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            Form_bon_livraison FR = new Form_bon_livraison("client_factur", int.Parse(comboID.Text));

            FR.Show();
        }
    }
}
