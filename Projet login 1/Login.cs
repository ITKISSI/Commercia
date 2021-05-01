using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Projet_login_1
{
    public partial class Login : Form
    {
        commerciaEntities db = new commerciaEntities();
        public Login()
        {
            InitializeComponent();
            bButton1.Focus();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);





       
        int j = 0, s= 0;
     private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit(); //program kaytbala3
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bTextbox1_Enter(object sender, EventArgs e)
        {
            //vider le txtbox
            if (bTextbox1.Text == "Utilisateur")
            {
                bTextbox1.Text = "";
                bTextbox1.ForeColor = Color.WhiteSmoke;//changer le couleur de txt
                bLabel1.Visible = true;
            }
        }

        private void bTextbox1_Leave(object sender, EventArgs e)
        {
            if (bTextbox1.Text == "")
            {
                bTextbox1.Text = "Utilisateur";
                bTextbox1.ForeColor = Color.Silver;
                bLabel1.Visible = false;
            }
        }

        
        private void bTextbox2_Enter(object sender, EventArgs e)
        {
            //vider le txtbox
            if (bTextbox2.Text == "Mot De Passe")
            {
                bTextbox2.Text = "";
                bTextbox2.ForeColor = Color.WhiteSmoke;//changer le couleur de txt
                bLabel2.Visible = true;
            }
        }

        private void bTextbox2_Leave(object sender, EventArgs e)
        {
            if (bTextbox2.Text == "")
            {
                bTextbox2.Text = "Mot De Passe";
                bTextbox2.ForeColor = Color.Silver;
                bLabel2.Visible = false;
            }
        }
        private void bCheckbox1_OnChange(object sender, EventArgs e)
        {
            // Show password 
            if (bCheckbox1.Checked == true)
            {
                bTextbox2.isPassword = false;
                bTextbox2.Focus();
                closeye.Visible = false;
                openeye.Visible = true;
            }
            else
            { 

                 bTextbox2.isPassword = true;
                 bTextbox2.Focus();
                 openeye.Visible = false;
                 closeye.Visible = true;
            }
        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bButton2_Click(object sender, EventArgs e)
        {
            inscription f = new inscription();
            f.Show(); // bach kan afichiw page inscreption
            this.Hide();//login matab9ach
        }

        private void bButton1_Click(object sender, EventArgs e)
        {
            if (bTextbox1.Text != "" && bTextbox2.Text != "")
            {
                try
                {
                    var utl = db.utilisateur.Where(w => w.Email == bTextbox1.Text && w.Mot_De_Passe == bTextbox2.Text).Select(s => new { s.num_utl, s.Type_U }).FirstOrDefault();
                    if (bTextbox1.Text == "admin" && bTextbox2.Text == "admin")
                    {

                        Acceuil st = new Acceuil("gérant", 0);
                        bTextbox1.Text = "";
                        bTextbox2.Text = "";
                        bTextbox2.isPassword = true;
                        bCheckbox1.Checked = false;
                        this.Hide();
                        st.Show();

                    }
                    else if (utl != null)
                    {
                        Acceuil st = new Acceuil(utl.Type_U, utl.num_utl);
                        bTextbox1.Text = "";
                        bTextbox2.Text = "";
                        bTextbox2.isPassword = true;
                        bCheckbox1.Checked = false;
                        this.Hide();
                        st.Show();
                    }
                    else
                    {
                        j += 1;
                        if (j > 2)
                        {
                            MessageBox.Show("attendez 30 seconde", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            timer1.Enabled = true;
                            bPanel1.Visible = true;
                            bLabel3.Visible = true;
                            bButton1.Enabled = false;
                            bCheckbox1.Enabled = false;
                            bTextbox1.Enabled = false;
                            bTextbox2.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("donnees incorect", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bTextbox1.Select();
                            bTextbox1.Text = "";
                            bTextbox2.Text = "";
                            bTextbox1.Focus();

                        }

                    }
                }
                catch
                {
                    MessageBox.Show(" entrez email et le mot de passe", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bTextbox1.Focus();
                    //bTextbox1.SelectAll();
                }

            }
            else MessageBox.Show("Veuillez compléter les informations", "login", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            s += 1;

            if (s >= 30)
            {
                s = 0;
                timer1.Enabled = false;

                bLabel3.Visible = false;
                bLabel3.Text = "30";
                bPanel1.Visible = false;
                bButton1.Enabled = true;
                bCheckbox1.Enabled = true;
                bTextbox1.Enabled = true;
                bTextbox2.Enabled = true;
                bTextbox1.Focus();
                j = 0;
            }
            else if (s < 30) { bLabel3.Text = (int.Parse(bLabel3.Text) - 1).ToString(); }

        }

        

    }
              
                
  }

