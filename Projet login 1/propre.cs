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
    public partial class propre : Form
    {
        public propre()
        {
            InitializeComponent();
        }

        private void propre_Load(object sender, EventArgs e)
        {
           
            
        }
        int i=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (i == 4)
            {
                timer1.Enabled = false;
                Login lg = new Login();
                lg.Show(); this.Hide();
            }
            else
            {
                i++;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            label4.Text = "Copyright " + DateTime.Now.Year;
        }
    }
}
