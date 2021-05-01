﻿using System;
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
    public partial class ajoute_client : Form
    {
        commerciaEntities db = new commerciaEntities();
        bool vr;
        public ajoute_client( bool mth,string ID,string nom,string prenom,string tel,string email,string ville, string adresse)
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
                comboID.Text =ID;
                textNOM.Text = nom;
                textPRN.Text = prenom;
                textTEL.Text = tel;
                textEML.Text = email;
                textVL.Text = ville;
                richTextADS.Text = adresse;
                btn.ButtonText = "MODIFER";
            }
        }
        public bool verfier_combo()
        {
            if ( textNOM.Text != "" && textPRN.Text != "" && textTEL.Text != "" && textVL.Text != "" && textEML.Text != "" && richTextADS.Text != "")
            {
                return true;
            }

            else return false;
        }
        
        private void btn_Click(object sender, EventArgs e)
        {
           
            if (vr == true)
            {
                client cl = new client();
                cl.Nom = textNOM.Text;
                cl.Prenom = textPRN.Text;
                cl.Telephone = textTEL.Text;
                cl.Ville = textVL.Text;
                cl.Email = textEML.Text;
                cl.Adresse = richTextADS.Text;
                if (verfier_combo() == true)
                {
                    db.client.Add(cl);
                    db.SaveChanges();
                   
                    MessageBox.Show("Bien ajoute !!", "ajouter un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    
                    
                }
                else MessageBox.Show("completez les donne SVP !!", "ajouter un client", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                int IDD = int.Parse(comboID.Text);

                if (verfier_combo() == true)
                {
                    client cl = db.client.Where(w => w.Num_C == IDD).FirstOrDefault();
                    cl.Nom = textNOM.Text;
                    cl.Prenom = textPRN.Text;
                    cl.Telephone = textTEL.Text;
                    cl.Ville = textVL.Text;
                    cl.Email = textEML.Text;
                    cl.Adresse = richTextADS.Text;
                    db.SaveChanges();
                    MessageBox.Show("Bien modifie !!", "modifier un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    
                }
                else MessageBox.Show("completez les donnees SVP !!", "modifier un client", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void ajoute_client_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void ajoute_client_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void comboID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
