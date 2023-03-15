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

namespace DÖNEM_PROJESİ
{
    public partial class Skorlar : Form
    {
        public Skorlar()
        {
            InitializeComponent();
        }

       
        private void Skorlar_Load(object sender, EventArgs e)
        {


             
            string yol = @"c:\yilan_oyunu\oyuncu_bilgileri.txt";
            FileInfo dosya1 = new FileInfo(yol);//dosya işlemlerini kullanabilmek için fileInfo dan yararlandım.
            if (dosya1.Exists)//dosyanın varlığını kontrol ediyor.
            {
                StreamReader oku = new StreamReader(yol);//dtreamRider ile belirtilen yoldaki metin belgesinden satıır satır okuma işlemi yaptım.
                string satir;//metin belgesindeki her bir satırı temsil ediyor.
                while ((satir = oku.ReadLine()) != null)//dosyada okunacak satır kalmayana kadar ilerlemesi için kulandım.
                {
                    listBox1.Items.Add(satir);//listboxa teker teker tüm satırları yazdırdım.
                }
            }
            


        }

      
    }
}
