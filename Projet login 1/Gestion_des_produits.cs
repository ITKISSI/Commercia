using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_login_1
{
    public partial class Gestion_des_produits : Form
    {
        commerciaEntities db = new commerciaEntities();

        public Gestion_des_produits()
        {
            InitializeComponent();
            rempli();
        }
        public void rempli()
        {

            comboID.DataSource = db.produit.Select(s => s.Num_P).ToList();
            comboTYP.DataSource = db.produit.Select(s => s.Type_P).ToList();
            dataGridView1.DataSource = db.produit.Select(s => new { s.Num_P, s.Libelle, s.Type_P, s.Prix, s.Quantite, s.Description_P }).ToList();

            //dataGridView1.Columns[8].Visible = false;
            //dataGridView1.Columns[9].Visible = false;
            //dataGridView1.Columns[10].Visible = false;
        }
        public bool verfier_combo()
        {
            if ( textLBL.Text != "" && comboTYP.Text != "" && textPRX.Text != "" && textQT.Text != "" /*&& textEMG.Text != "" */ && richTextADS.Text != "")
            {
                return true;
            }

            else return false;
        }
        private void Controle_et_contentien_Load(object sender, EventArgs e)
        {
        }

        private void comboTYP_mtr_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
       

        private void bButton1_Click(object sender, EventArgs e)
        {
            try
            {
                produit pr = new produit();
                pr.Libelle = textLBL.Text;
                pr.Type_P = comboTYP.Text;
                pr.Prix = int.Parse(textPRX.Text);
                pr.Quantite = int.Parse(textQT.Text);
                pr.Image_Nom = textEMG.Text;
                pr.Description_P = richTextADS.Text;
                byte[] pic = null;
                FileStream stream = new FileStream(img, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                pic = brs.ReadBytes((int)stream.Length);
                pr.photo = pic;
                if (verfier_combo() == true)
                {
                    db.produit.Add(pr);
                    db.SaveChanges();
                    rempli();
                    MessageBox.Show("Bien ajoute !!", "ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("completez les donnees SVP !!", "ajouter un produit", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    openFileDialog1.Filter = "all Image | *.pnj; *.JPG; *.BMP";
        //   // openFileDialog1.ShowDialog = DialogResult.OK;
            
        //        pictureBox1.Image =Image.FromFile( openFileDialog1.FileName);
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void _TextChanged(object sender, EventArgs e)
        {

        }

        private void comboID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboID_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int id = int.Parse(comboID.Text);
            produit p = db.produit.Where(w => w.Num_P ==id).FirstOrDefault();
            comboTYP.Text = p.type_produit.Libelle;
            textLBL.Text = p.Libelle;
            textPRX.Text = p.Prix.ToString();
            textQT.Text = p.Quantite.ToString();
            richTextADS.Text = p.Description_P;
            textEMG.Text = p.Image_Nom;
            byte[] pic = (byte[])(p.photo);
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

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
           
                if (recherch.Text != "")
                {
                    produit p = db.produit.Where(w => w.Libelle == recherch.Text).FirstOrDefault();
                    if (p != null)
                    {
                        comboTYP.Text = p.type_produit.Libelle;
                        textLBL.Text = p.Libelle;
                        textPRX.Text = p.Prix.ToString();
                        textQT.Text = p.Quantite.ToString();
                        richTextADS.Text = p.Description_P;
                        textEMG.Text = p.Image_Nom;
                    dataGridView1.DataSource = db.produit.Where(w => w.Libelle == recherch.Text).Select(s => new { s.Num_P, s.Libelle, s.Type_P, s.Prix, s.Quantite, s.Description_P }).ToList();

                    recherch.Clear();
                    }
                }
               
          
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboID.Text);
            produit p = db.produit.Where(w => w.Num_P == id).FirstOrDefault();
            if(MessageBox.Show("êtes-vous sûr", "supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                db.produit.Remove(p);
                db.SaveChanges();
                rempli();
                MessageBox.Show("bien supprime", "supprimer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            rempli();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboID.Text);
            produit p = db.produit.Where(w => w.Num_P == id).FirstOrDefault();
            if (MessageBox.Show("êtes-vous sûr", "supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                p.Libelle = textLBL.Text;
                p.Type_P = comboTYP.Text;
                p.Prix = int.Parse(textPRX.Text);
                p.Quantite = int.Parse(textQT.Text);
                p.Image_Nom = textEMG.Text;
                p.Description_P = richTextADS.Text;
                byte[] pic = null;
                try
                {
                    FileStream stream = new FileStream(img, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    pic = brs.ReadBytes((int)stream.Length);
                    p.photo = pic;
                }
                catch (Exception)
                {


                }
                if (verfier_combo() == true)
                {
                   
                    db.SaveChanges();
                    rempli();
                    MessageBox.Show("Bien modifie !!", "modifer un prduit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("completez les donnees SVP !!", "modifer un produit", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           comboID.Text=dataGridView1.CurrentRow.Cells[0].Value.ToString();
           
        }
        string img="";
        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog odg = new OpenFileDialog();
            odg.Filter = "Image Files | *.JPG;*.PNG;*.GIF";
            odg.InitialDirectory = "D:\\";
            if (odg.ShowDialog() == DialogResult.OK)
            {
                photo.BackgroundImage = Image.FromFile(odg.FileName);
                photo.BackgroundImageLayout = ImageLayout.Stretch;

                img = odg.FileName; }

            if (img == null)
            {
                imgbtn.IdleLineColor = Color.Red;
            }
            else
            {
                imgbtn.IdleLineColor = Color.Lime;
            }
        }
    }
}
