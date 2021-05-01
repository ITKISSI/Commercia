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
    public partial class Acceuil : Form
    {
        commerciaEntities db = new commerciaEntities();
        int ID_tilisater;
        
        public Acceuil(string utilisateur,int ID_utl)
        {
            InitializeComponent();
            ID_tilisater = ID_utl;
            if (utilisateur == "vendeur")
            {
                btnPro.Enabled = false;
                MessageBox.Show(utilisateur);
            }
            else if (utilisateur == "magasinier")
            {
                btnClient.Enabled = false;
                btnFourni.Enabled = false;
                MessageBox.Show(utilisateur);
            }
            else 
            {
                MessageBox.Show(utilisateur);
            }
            //materiel m = new materiel();



            //label7.Text = db.detail_materiel.Count(c => c.id_type_materiel == 4).ToString();
            //label8.Text = db.detail_materiel.Count(c => c.id_type_materiel == 5).ToString();
            //label9.Text = db.detail_materiel.Count(c => c.id_type_materiel == 6).ToString();
            //label10.Text = db.detail_materiel.Count(c => c.id_type_materiel == 1).ToString();
            //label11.Text = db.detail_materiel.Count(c => c.id_type_materiel == 2).ToString();
            //label12.Text = db.detail_materiel.Count(c => c.id_type_materiel == 3).ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // katal3ab 3la surface dyal panel (width)
            if (panel1.Width == 200)
            {
                panel1.Width =60;
            }
            else
            {
                panel1.Width = 200;
            }
        }

      
        private void btnClient_Click(object sender, EventArgs e)
        {
            // lpanel librit t afficha fiha form
            panMove.Top = btnClient.Top;
            if (panel3.Visible == true) { panel3.Visible = false; }
            Gestion_Client f = new Gestion_Client();
            
            f.MdiParent = this;
            f.Show();
            f.Dock = DockStyle.Fill;
            //ClientFormInPanel(new Client_0());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // fin ma clikit 3la button wa7ed panel kat7arak m3ak
            panMove.Top = btnFourni.Top;
            if (panel3.Visible == true) { panel3.Visible = false; }
            Gestion_des_fournisseurs f1 = new Gestion_des_fournisseurs();
           
            f1.MdiParent = this;
            f1.Show();
            f1.Dock = DockStyle.Fill;

        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            panMove.Top = btnStats.Top;
            panel3.Visible = true;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (panel3.Visible == true) { panel3.Visible = false; }
            panMove.Top = btnOption.Top;
            gastion_utilisateur cl = new gastion_utilisateur(ID_tilisater);
            cl.MdiParent = this;
            cl.Dock = DockStyle.Fill;
            cl.Show();
        }
       
        private void butpro_Click(object sender, EventArgs e)
        {
            panMove.Top = btnPro.Top;
            if (panel3.Visible == true) { panel3.Visible = false; }
            Gestion_des_produits f4 = new Gestion_des_produits();
            f4.MdiParent = this;
            f4.Show();
            f4.Dock = DockStyle.Fill;
            //ClientFormInPanel(new F_Produit());


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            propre l = new propre();
            l.Show(); // bach kan afichiw page inscreption
            this.Hide();//login matab9ach
            
        }

       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            
            if(this.WindowState == FormWindowState.Normal)
            {

                this.WindowState = FormWindowState.Maximized;
            }
            else this.WindowState = FormWindowState.Normal;
        }

        private void Acceuil_Load(object sender, EventArgs e)
        {
            labelNca.Text = db.commande_achat.Select(s => s.Num_C_A).Count().ToString();
            labelNcv.Text = db.commande_vente.Select(s => s.Num_C_V).Count().ToString();
            labelNc.Text = db.client.Select(s => s.Num_C).Count().ToString();
            labelNf.Text = db.fournisseur.Select(s => s.Num_F).Count().ToString();
            labelNp.Text = db.produit.Select(s => s.Num_P).Count().ToString();



            labelCjours.Text = db.ligne_commande_vente.Where(s => s.commande_vente.Date_C.Value.Day == DateTime.Now.Day && s.commande_vente.Date_C.Value.Month == DateTime.Now.Month && s.commande_vente.Date_C.Value.Year == DateTime.Now.Year).Sum(s => s.Quantite * s.produit1.Prix).ToString()+"DH";
            labelCmois.Text =db.ligne_commande_vente.Where(s => s.commande_vente.Date_C.Value.Month == DateTime.Now.Month && s.commande_vente.Date_C.Value.Year == DateTime.Now.Year).Sum(s => s.Quantite * s.produit1.Prix).ToString() + "DH";
            labelCannee.Text = db.ligne_commande_vente.Where(s => s.commande_vente.Date_C.Value.Year == DateTime.Now.Year).Sum(s => s.Quantite * s.produit1.Prix).ToString() + "DH";
            labelCtotale.Text = db.ligne_commande_vente.Sum(s => s.Quantite * s.produit1.Prix).ToString() + "DH";





        }

        private void labelCjours_Click(object sender, EventArgs e)
        {

        }
    }
}
