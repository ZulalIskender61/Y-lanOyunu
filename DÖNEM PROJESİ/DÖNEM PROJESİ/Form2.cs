using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DÖNEM_PROJESİ
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 evet= new Form1();
            evet.evet();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
    }
}
