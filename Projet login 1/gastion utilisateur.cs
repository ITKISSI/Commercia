using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Projet_login_1
{
    public partial class gastion_utilisateur : Form
    {
        commerciaEntities db = new commerciaEntities();
        int IDUTL;
        public gastion_utilisateur(int ID)
        {
            InitializeComponent();
            IDUTL = ID;
            rempli();
        }
        public void rempli()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < db.utilisateur.Count(); i++)
            {
                var bb = db.utilisateur.Select(s => new { s.Nom, s.Prenom }).ToList().ElementAt(i);
                listBox1.Items.Add(bb.Nom + " " + bb.Prenom);
            }
            if (IDUTL != 0)
            {
                label2.Visible = false;
                comboBox1.Visible = false;
                listBox1.Visible = false;
                BTN_ajout.Visible = false;
                BTN_supp.Visible = false;
                comboBox1.DataSource = null;
                utilisateur utl = db.utilisateur.Where(w => w.num_utl == IDUTL).FirstOrDefault();
                NOM.Text = utl.Nom;
                PRENOM.Text = utl.Prenom;
                EMAIL.Text = utl.Email;
                PASSWORD.Text = utl.Mot_De_Passe;
                typeutl.SelectedItem = utl.Type_U;
                byte[] pic = (byte[])(utl.photo);
                if (pic == null)
                {
                    photo.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(pic);
                    photo.Image = Image.FromStream(ms);

                }
            }
            else
            {
                label1.Text = "Gestion utilisateur";
                comboBox1.DataSource = db.utilisateur.Select(s => s.num_utl).ToList();
            }
        }
        private void gastion_utilisateur_Load(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        string img = "";
        private void bButton1_Click(object sender, EventArgs e)
        {
            if (NOM.Text != "" && PRENOM.Text != "" && EMAIL.Text != "" && typeutl.Text != "" && PASSWORD.Text != "")
            {
               
                utilisateur utl = new utilisateur();

                utl.Nom = NOM.Text;
                utl.Prenom = PRENOM.Text;
                utl.Email = EMAIL.Text;
                utl.Type_U = typeutl.Text;
                utl.Mot_De_Passe = PASSWORD.Text;
                byte[] pic = null;
                FileStream stream = new FileStream(img, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                pic = brs.ReadBytes((int)stream.Length);
                utl.photo = pic;
                db.utilisateur.Add(utl);
                db.SaveChanges();
                rempli();
                MessageBox.Show("bien ajoute ", "ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("completez les donnees SVP !! ", "ajouter", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
        }
        int IDD;
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
          
            if (IDUTL == 0)
            {
                IDD = Convert.ToInt16(comboBox1.Text);
            }
            else { IDD = IDUTL; }
            if (NOM.Text != "" && PRENOM.Text != "" && EMAIL.Text != "" && typeutl.Text != "" && PASSWORD.Text != "")
            {
                utilisateur utl = db.utilisateur.Where(w => w.num_utl ==IDD).FirstOrDefault(); ;

                utl.Nom = NOM.Text;
                utl.Prenom = PRENOM.Text;
                utl.Email = EMAIL.Text;
                utl.Type_U = typeutl.Text;
                utl.Mot_De_Passe = PASSWORD.Text;
                byte[] pic = null;
                try
                {
                FileStream stream = new FileStream(img, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                    pic = brs.ReadBytes((int)stream.Length);
                    utl.photo = pic;
                }
                catch (Exception)
                {

                   
                }
                
                
                db.SaveChanges();
                rempli();
                MessageBox.Show("bien modifie ", "modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("completez les donnees SVP !! ", "modifier", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
           
                IDD = Convert.ToInt16(comboBox1.Text);
            
            if (NOM.Text != "" && PRENOM.Text != "" && EMAIL.Text != "" && typeutl.Text != "" && PASSWORD.Text != "")
            {
                utilisateur utl = db.utilisateur.Where(w => w.num_utl == IDD).FirstOrDefault(); ;

                db.utilisateur.Remove(utl);

                db.SaveChanges();
                rempli();
                MessageBox.Show("bien supprime ", "supprimer",MessageBoxButtons.OK,MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("completez les donnees SVP !! ", "supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
               
                int ID = int.Parse(comboBox1.Text);
            utilisateur utl = db.utilisateur.Where(w => w.num_utl == ID).FirstOrDefault();
            NOM.Text = utl.Nom;
            PRENOM.Text = utl.Prenom;
            EMAIL.Text = utl.Email;
            PASSWORD.Text = utl.Mot_De_Passe;
            typeutl.SelectedItem = utl.Type_U;

            byte[] pic = (byte[])(utl.photo);
            if (pic == null)
            {
                photo.Image = null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(pic);
                photo.Image = Image.FromStream(ms);

            }
            
           
        }

        private void gunaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(gunaCheckBox1.Checked == true)
            {
                PASSWORD.UseSystemPasswordChar = false;
                PASSWORD.PasswordChar = default;
            }
            else if (gunaCheckBox1.Checked == false)
            {
                PASSWORD.UseSystemPasswordChar = true;
               
            }
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = db.utilisateur.Select(s => s.num_utl).ToList().ElementAt(listBox1.SelectedIndex).ToString();
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            //bach t7adad chno ba4i wach photo wala txt...
            openFileDialog1.Filter = "Image Files | *.JPG;*.PNG;*.GIF";
            //ila b4it t7adad awal dosi yad5alo
            openFileDialog1.InitialDirectory = "D:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                photo.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                photo.BackgroundImageLayout = ImageLayout.Stretch;
               img = openFileDialog1.FileName;
                


            }
        }
    }
}
