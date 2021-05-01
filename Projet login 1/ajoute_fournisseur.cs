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

    public partial class ajoute_fournisseur : Form
    {
      
        commerciaEntities db = new commerciaEntities();

        bool vr;
        public ajoute_fournisseur(bool mth, string ID, string nom_ent, string tel, string email, string ville, string adresse)
        {


            InitializeComponent();
            vr = mth;
            if (vr == true)
            {
                btn.ButtonText = "AJOUTER";
                comboID.Visible = false;
                Bureau.Visible = false;
            }
            else
            {
                comboID.Text = ID;
                textNOM.Text = nom_ent;
              
                textTEL.Text = tel;
                textEML.Text = email;
                textVL.Text = ville;
                richTextADS.Text = adresse;
                btn.ButtonText = "MODIFER";
            }

        }

        private void ajoute_fournisseur_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool verfier_combo()
        {
            if (textNOM.Text != ""  && textTEL.Text != "" && textVL.Text != "" && textEML.Text != "" && richTextADS.Text != "")
            {
                return true;
            }

            else return false;
        }
        private void btn_Click(object sender, EventArgs e)
        {
            if (vr == true)
            {
                fournisseur cl = new fournisseur();
                cl.Nom_Entreprise = textNOM.Text;
              
                cl.Telephone_F = textTEL.Text;
                cl.Ville_F = textVL.Text;
                cl.Email_F = textEML.Text;
                cl.Adresse_F = richTextADS.Text;
                if (verfier_combo() == true)
                {
                    db.fournisseur.Add(cl);
                    db.SaveChanges();

                    MessageBox.Show("Bien ajoute !!", "ajouter un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();


                }
                else MessageBox.Show("completez les donnees SVP !!", "ajouter un client", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                int IDD = int.Parse(comboID.Text);

                if (verfier_combo() == true)
                {
                    fournisseur cl = db.fournisseur.Where(w => w.Num_F == IDD).FirstOrDefault();
                    cl.Nom_Entreprise= textNOM.Text;
                    
                    cl.Telephone_F = textTEL.Text;
                    cl.Ville_F = textVL.Text;
                    cl.Email_F = textEML.Text;
                    cl.Adresse_F = richTextADS.Text;
                    db.SaveChanges();
                    MessageBox.Show("Bien modifier !!", "modifier un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                else MessageBox.Show("completez les donnees SVP !!", "modifier un client", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    } 
}
