using System;
using System.IO;
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
    public partial class inscription : Form
    {
        commerciaEntities db = new commerciaEntities();
          public inscription()
        {
            InitializeComponent();

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


         void Clear() // bach kindiro inscreption dok les cases ywaliw vide
        {
            textNom.Text = textPrenem.Text = textEmail.Text  = Pass.Text = pass2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            this.Close();// rir dik la page likatbala3
            lg.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

           private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           
              if (textNom.Text == "" || textPrenem.Text == "" || textEmail.Text == "" || Pass.Text == "" || pass2.Text == "" || typeutl.Text=="")
            {
                MessageBox.Show("completez les donnees SVP !!", "inscrire", MessageBoxButtons.OK, MessageBoxIcon.Question);//ila l9alna chi case khawya 3ad kayt2afiha had lmsg
            }
            else if (Pass.Text != pass2.Text)
            {
                MessageBox.Show("Le mot de passe est incorrecte", "inscrire", MessageBoxButtons.OK, MessageBoxIcon.Question); //kayt2akad lina man mot de pass wach sa7i7
            }
              else
            {
                try
                    {
                    DialogResult res=DialogResult.Yes;
                    if (img == null)
                    {
                        res=MessageBox.Show("Vous n'avez pas sélectionné d'image. Voulez-vous vous inscrire?","inscrire", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         }  
                         if (res==DialogResult.Yes)
                           {
                                utilisateur utl = new utilisateur();
                                utl.Nom = textNom.Text;
                                utl.Prenom = textPrenem.Text;
                                utl.Email = textEmail.Text;
                                utl.Type_U = typeutl.Text;
                                utl.Mot_De_Passe = pass2.Text;
                                byte[] pic = null;
                                FileStream stream = new FileStream(img, FileMode.Open, FileAccess.Read);
                                BinaryReader brs = new BinaryReader(stream);
                                pic = brs.ReadBytes((int)stream.Length);
                                utl.photo = pic;
                                db.utilisateur.Add(utl);
                                db.SaveChanges();
                                MessageBox.Show("félicitations", "inscrire", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                Clear();
                           }
                         
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Il y a des erreurs  " + ex.Message, "inscrire", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }

               
            }
        }
        string img;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog odg = new OpenFileDialog();
            odg.Filter = "Image Files | *.JPG;*.PNG;*.GIF";
            odg.InitialDirectory = "D:\\";
            if (odg.ShowDialog() == DialogResult.OK)
            { img = odg.FileName;}
                
                if (img == null)
                {
                    button1.FlatAppearance.BorderColor = Color.Red;
                }
                else
                {
                    button1.FlatAppearance.BorderColor = Color.Lime;
                }
            }
    }
}
