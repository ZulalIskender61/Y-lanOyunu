using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;//point komutunu kullanabilmek için yazdım.
using System.IO; //dosya işlemlerini kullanabilmek için yazdım.
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; //skorları dosyada görüntülemelk için kullandığım  kütüphane.
namespace DÖNEM_PROJESİ
{
    
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
           

        }
       

        Yilan yilanimiz;//yılan sınıfından faydalanmak için kullandım.
        Yon yonumuz;//yon sınıfından faydalanmak için kullandım.
        PictureBox[] pb_yilanparcalari;// yılanın parcalarını göstermek için kullandım.
        bool yem_varmi = false;// yemin olup olmadıgını arastırmak için kullandım.
        Random rastg = new Random(); // yemi rastgele yerlerde oluşturmak için random fonksiyonunu kullandım. 
        PictureBox pb_yem;//yem için kullandım.
        float skor = 0.00f;// skor için kullandım .float kullandım çünkü double virgülden sonrasını çok uzatıyordu.
        float puan = 0;//Skor için kullandım . (Aşağıda daha detaylı anlatımı bulunmaktadır).
        float sayac = 0;//timer ikide skor işlemlerinde kullanabilmek için kullandım.
        int boy=0, en=0; //paneldeki yemin oluşacagı yerdeki rast gele konumlarını belirlemek ve skorda kenarlara gelen kısımlarda yemin belirtilen süre içerisinde yenirse puan eklemek için kullandım.
        int sure = 0;//timer2 de zamanı hesaplamak için kullandım.
        int z = 0;//kösede olusan yemi belli etmek için kullandım.
        string yol = @"c:\yilan_oyunu";
      
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(yol) == true)//Exists komutu dosyanın varlığını araştırıyor.
            {
               //boş bıraktım çünkü bu koşul doğruysa yapacak bir işlemimiz yok.
               //bu if - else koşullarını kullanma sebebim ise oyunu her başlattığında yeni bir dosya acıp kayıtları baştan yapmasını istemediğimden.
            }
            else { //Eğer klasör ve dosyanın varlığı yok ise klasor ve dosyayı açmak için kullandım.
            Directory.CreateDirectory(@"c:\yilan_oyunu");//c hafızasında kalsor oluşturmak için bu komutları kullandım.
            FileStream fs = File.Create(@"c:\yilan_oyunu\oyuncu_bilgileri.txt");//c hafızasında oluşturdugum klasorun içinde bu dosyayı(metin belgesi *txt) açtım.
            fs.Close();//işlem yapmak istemediğimden close  u kullandım.
             }
            
            //formda bilgilendirmek amaçlı kullandığım label.
            label4.Text = "OYUNU DURDURMAK İÇİN [D TUŞUNU]" + "\n\n" +
                          "OYUNU BAŞLATMAK İÇİN [B TUŞUNU]" + "\n\n" +
                          "KULLANINIZ" + "\n\n" +
                          "BAŞARILAR...";
           
            label1.BackColor = Color.Yellow;//skorun sarı arka rengi için kullandım.
            label2.BackColor = Color.Yellow;//sürenin sarı arka rengi için kullandım.
            label3.BackColor = Color.LightPink;//yılan oyunu yazısının pembe arka rengi için kullandım.
            label4.BackColor = Color.LightPink;//panelin üserindeki bilgilendirmenin pembe arka rengi için kullandım.
        }
       

        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e) // menuStript in hakkımızda kısmı için yazdığım bölüm.
        {
           MessageBox.Show("Bu proje Zülal İskender tarafından yapılmıştır.","ZÜLAL İSKENDER",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void nasılOynanırToolStripMenuItem_Click(object sender, EventArgs e) // menuStript in nasıl oynandığına dair bilgi almak için  yazdığım bölüm.
        {
            MessageBox.Show("Öncelikle oynumuza seviye belirleme ile başlıyoruz.Daha sonra başlatmak için B/b tuşuna basıyoruz.D tuşuna basarakta oyunu durdurup skorları görüntüleyebiliyoruz.", "Oyun Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e) // menuStript in oyundan çıkış yapmak için  yazdığım bölüm.
        {
            this.Close();
        }
        
       public void yeni_oyun() // oyunu başlatmak ve yeni oyun için fonksiyonum.
        {
            
            sayac = 0;//her yeni oyun başlangıcında sayacı sıfırladım ki üzerinde olacak işlemleri doğru yapsın.
            z = 0;//kösedeki yemlerin sayisi
            sure = 0;//her yeni oyunda süreyi baştan baştatım.
            skor = 0;//her yeni oyunda skoru sıfırladım.
            yem_varmi = false;//hey yeni oyuna yemi oluşturmak için bool değiskenini false gösterdim.
        
            yilanimiz = new Yilan();
            yonumuz = new Yon(-10,0);//yılanın baktıgı yone doğru süreyle beraber başlaması için -10 yazdım.
            pb_yilanparcalari = new PictureBox[0];//yılan parccaları için pixtureBox olusturdum.
            
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);//boyutu arttırıyorum .
                pb_yilanparcalari[i] = Pb_ekle();//ve yılanın son gelen parçasını bu komutla ekliyorum.
            }

            //yeni oyunda süreçleri başlattım.
            timer1.Start();
            timer2.Start();
            
        }
        private PictureBox Pb_ekle()//yılan parçalarını pictureBox a eklemek için oluşturdum.
        {

            PictureBox pb = new PictureBox();//yeni bir picturBox oluşturdum.
            pb.Size = new Size(10, 10);//boyutları 10'a 10 ayarladım.
           
            pb.BackColor = Color.Green;//yılnanın rengini yeşil olarak ayarladım.
           
            pb.Location = yilanimiz.GetPos(pb_yilanparcalari.Length - 1);// parçanın konumunu yılan sınıfınfan istedim .
            panel1.Controls.Add(pb);  //paneldeki yerine burada ekledim.                                 
            return pb;
        }
        private void Pb_guncelle() //pictureBoxlarımızın konumlarını güncellemem gerekli.
        {
            for (int i = 0; i < pb_yilanparcalari.Length; i++)
            {
                pb_yilanparcalari[i].Location = yilanimiz.GetPos(i);//sırayla hepsinin konumlarını güncelledim.
            }
        }


        public void Yem_olustur()//yemi oluşturmak için açtım.
        {
            label6.Visible = false;
            if (!yem_varmi) 
            {
                sayac = 0;
                PictureBox pb = new PictureBox();//yem için pictureBox oluşturdum.
                pb.BackColor = Color.Red;//yemin rengini kırmızı belirledim.
                pb.Size = new Size(10, 10);//yemin boyutunuda 1 yılan parcası boyutu kadar oluşturdum.
                en = rastg.Next(panel1.Width / 10) * 10;//(Paneldeki belirlenen eni 10'a boldum)yemin konumlarını 10'un katları şeklinde konumlandırdım.
                boy = rastg.Next(panel1.Height / 10) * 10;//(Paneldeki belirlenen boyu 10'a boldum)

                //aşagıdaki if bloklarında yemin köşeye gelebilecek noktalarını belirledim ve ihtimallerin birinin tutması sonucunda degeri sıfır olan değişkenin degerini artırdım.
                if (boy == 0 && (en == 0 || en <= 690)  )
                {
                    z++;                   
                }
                if (en==0 && (boy==0 || boy<=390))
                {
                    z++;
                }
                if (boy == 390 && (en==0 || en<=690))
                {
                    z++;
                }
                if (en==690 && (boy==0|| boy<=390))
                {
                    z++;
                }
                
                pb.Location = new Point(en,boy);//en ve boy şeklinde yemin yerleşecegi konumları belirleyip yerleştirdim.
                pb_yem = pb;
                yem_varmi = true;//yemi oluşturdugum için bu deger artık true olmalı.
                panel1.Controls.Add(pb);

            }
        }
       
        public void Yem_yedi_mi()//yemi yiyip yemediğini kontrol etmek için ve skoru hesaplamak,yılanı büyütmek için kullandım.
        {
            if (yilanimiz.GetPos(0) == pb_yem.Location)//yılanın baş kısmı yemin konumuna eşit ise yem yenmiş olur.
            {
               

                if (sayac <= 100)//eğer yılanımız yemi  101 saniyeden düşük bir zamnada yediyse
                {
                    if (z == 1) //eğer yılanımız köşe noktasında yediyse
                    {

                        skor += 10;//skora 10 puan ekledim.
                       
                        label6.Visible = true;//kösedeki yemi yedikten sonra kısa bir süre belirecek olan bir işaret ekledim . kontrol amaçlı.
                    }
                  
                    puan = 100 / sayac;//yenilen süreyi 100 le bölüp puan degerine atadım.
                    sayac = 0;//sayac1 i her yemi yediğinde sıfırladım ki her yediği yemin süresini bilelim.
                    
                }
                if(sayac>100)
                {

                    puan = 0;//yemi verilen süre zarfında yemediyse skora eklenecek puanı 0 olarak belirledim.
                    sayac = 0;
                }
                skor += puan;//if -else bloklarından elde ettiğim puanları skora ekledim . Böylelikle oyuncunun skorunu elde ettim.
                puan = 0;
                z = 0;//şart kosulan surede yediyse veya yiyemediyse z nin degerini bir sonraki yem için sıfırladım.
                sayac = 0;
                yilanimiz.Buyu();//yılanımız puan alsa da almasa da buyumesi için bu fonksiyonu cağırdım.
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);//yılan parçalarını buyutuyorum.
                pb_yilanparcalari[pb_yilanparcalari.Length - 1] = Pb_ekle();
                yem_varmi = false;//yemi yedikten sonra tekrar yem oluşturmak için değerini false yaptım.
                panel1.Controls.Remove(pb_yem);//yemi bu işlemle panelden kaldırdım.(remove=kaldırmak)

                label1.Text = "SKOR: " + skor.ToString();//timer2 işlediği sürece skoru ekrana yazdırdım.
              

            }

        }
        public void Yilan_kendine_Carpti()//yenilgi durumuna bakmak için yazdım.
        { 
            //bu fonksiyon için yılan sınıfından yılanın buyuklugunu döndürdüm.
            for (int i = 1; i < yilanimiz.Yilan_buyuklugu; i++)
            {
                if (yilanimiz.GetPos(0) == yilanimiz.GetPos(i))
                {
                    Yenildi();//Kendine çarptıgı için bu fonksiyonu çağırdım.
                    puanlamalar();//yenilgi durumunda skoru yazdırması için kullandım.

                }
            }


        }
        public void Duvarlara_Carpti()//yenilgi durumuna bakmak için yazdım.
        {
            Point p = yilanimiz.GetPos(0);
            if (p.X < 0 || p.X > panel1.Width - 10 || p.Y < 0 || p.Y > panel1.Height - 10)//yılanın ba kısmı panelin dışına çıkıyor ise 
            {
                Yenildi();//Duvara çarptıgı için bu fonksiyonu çağırdım.
                puanlamalar();//yenilgi durumunda skoru yazdırması için kullandım.
            
            }
           
        }
        int dakika = 0, saniye=0;//süre için oluşturdum.
      
      private void puanlamalar()// iki durumda çağırdım bir fonksiyondur ve bu fonsiyonu skoru yazdırması için kullandım .
        {
           
            StreamWriter yaz = new StreamWriter(@"c:\yilan_oyunu\oyuncu_bilgileri.txt", true);//kaydedicegimiz dosyaya yaz sınıfını oluşturdum .
           
            yaz.WriteLine("skor: "+skor);//elde edilen skoru bu sınıf ile birlikte dosyaya yazdım.
        
            dakika = sure / 60;//süreyi salise cindinde ölçtüğünü fark ettim 
                                   //ve dakika ,saniye,salise cinsinden yazmak istedim .60salise 1 saniye,60saniye 1 sakika oldugundan bu işlemleri yaptım.
            saniye =sure % 60;
            

            //süre görsel açıdan 00:00:00 şeklinde oluşsun diye alttaki if bloklarını kurdum.
            if (dakika < 10 && saniye < 10)
            {
               yaz.WriteLine("gecen süre: "+0+dakika+":"+0+ saniye );
              
            }
            if (dakika < 10 && saniye > 10)
            {
                yaz.WriteLine("gecen süre: " + 0 + dakika + ":"  + saniye);
                
            }
            if (dakika> 10 && saniye < 10)
            {
                yaz.WriteLine("gecen süre: "  + dakika + ":" +0+ saniye);
                
            }
            if (dakika> 10 && saniye > 10)
            {
                yaz.WriteLine("gecen süre: " + dakika + ":" + saniye );
                
            }
          
           
            yaz.Close();//yazma işlemini işim bittikten sonra sonlandırdım.
        }
        private void isimler()
        {
          
            StreamWriter yaz = new StreamWriter(@"c:\yilan_oyunu\oyuncu_bilgileri.txt", true);//bilgisayarın c hafızasındaki yilan_oyunu klasorundeki Oyuncu_bilgileri metin belgesine
            yaz.WriteLine("isim:"+richTextBox1.Text);//richTexBox tan girdiğimiz isimi kayıt ettim.
            yaz.Close();//yazma işlemini işim bittikten sonra sonlandırdım.

        }
        private void tekrar()//tekrar oynamak isteyip istemediğini oyuncuya yeni bir form aracılığıyla sordum.
        {
            Form2 tkr = new Form2();
            tkr.Text = "YILAN OYUNU";//Form2 nin adını koydum.
            tkr.Show();
        }
        public void evet()//oyuncu eger tekrar oynamak istiyorsa oyunu bu şekilde tekrar başlatıyorum.
        {
            sayac = 0;//oyun bitmeden yeni oyun için bu tuşa bastıgımız zaman sayacı sıfırlanmadığı için sayacı buradada sıfırladım çünkü sonuç sodru çıksın.
            button2.Enabled = false;//oyun bitti ve durdur tuşuna bastık fakat çıkış yapmadan yeniden oyun oynamak istiyorsa oyuncu B/b tuşuna basarak oyuncu yeni oyun başlatabiliri
            panel1.Controls.Clear(); // paneldeki işlemleri temizler 
           
           

        }
        
        private void Yenildi()//iki durumda yenilgi durumu var ve bu fonksiyonu o durumlar gerçekleştiği zaman çagırıyorum.
        {
            sayac = 0;//sayacı yenildiği zamanda sıfırladım çünkü sıfırlamazsan oyuncu tekrar oynamak istediğinde sayacın değeri farklı oluyor.
           
            timer1.Stop();//oyun bittiği zaman timer1 e bağlı olan her işlevi durduruyorum.
            timer2.Stop();//oyun bittiği zaman timer2 ye bağlı olan her işlevi durduruyorum.
            MessageBox.Show("Oyun Bitti.Yenildin");
           
            
            tekrar();//oyuncu tekrar oynamak istiyorsada başta belirttiğim üzere b tuşuna basmadan başlatamayacaktır.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            yilanimiz.İlerle(yonumuz);//yılanı belirlenen yon doğrultusunda ilerlettim.
            Pb_guncelle();//yılanın parçalarının eklenen parçalarıyla birlikte konumlarını güncelledim.
            Yem_olustur();//yemi oluşturdum.
            Yem_yedi_mi();//bool degiğkenine bağlı işlemler yaptım.
            Yilan_kendine_Carpti();//yenilgi durumuna baktım.
            Duvarlara_Carpti();//yenilgi durumuna baktım.
            
        }

        private void timer2_Tick(object sender, EventArgs e)//timer2 yi timer1 normal saat akışına uygun olmadıgı için kullandım .(timer1 in interval ıni seviyeler için değiştirdim. )
        {                                                   //timer2 nin interval ını 1000 yaptım ki 1 saniyeye eşdeger olsun.
            


            sure++;//süre akisini oyunun başlangıcından bitimine hep sağladım.
            sayac++;//bu sayacı normal süre akışına uygun 
            label2.Text ="GEÇEN SÜRE: "+ sure.ToString();//timer2 işlediği sürece süreyi ekrana yazdırdım.
           
        }

       

        private void Form1_KeyDown(object sender, KeyEventArgs e) //formun üzrine tıkladıktan sonra eventslerden keyDown' a tıkladım . 
        {                                                        //bu şekilded alttaki kod satırları sayesinde  klavye yoluyla hareketi sağladım.   
            if (e.KeyCode == Keys.B)
            {
                evet();//oyunu yeniden B tuşuna basarak devam etmesini istedim.
                sayac = 0;//oyun bitmeden yeni oyun için bu tuşa bastıgımız zaman sayacı sıfırlanmadığı için sayacı buradada sıfırladım çünkü sonuç sodru çıksın.
                button2.Enabled = false;//oyun bitti ve durdur tuşuna bastık fakat çıkış yapmadan yeniden oyun oynamak istiyorsa oyuncu B/b tuşuna basarak oyuncu yeni oyun başlatabiliri
                panel1.Controls.Clear(); // paneldeki işlemleri temizler 
               
                yeni_oyun();//eger oyuncu B/b tusuna tıklar ise yeni oyun bloguna gidip oyunu başlatacak veya yeniden başlatacaktır.
               

            }

           else if (e.KeyCode == Keys.D)//eger oyuncu D/d tuşuna basar ise oyunu durduracaktır ve skorları görüntüle (button2) butonunu aktifleştirecektir.
            {
               
                timer1.Stop();//oyunu durdurmak için oyun esnasında işleyen zamanı durdurdum.
                timer2.Stop();//oyunu durdurduğum zaman skor için işleyen zamanıda durdurdum .
                button2.Enabled = true;//button2 yi oyunu durduktan sonra aktifleştirdim.

            }
            if (e.KeyCode == Keys.C)
            {
                
                this.Close();//cıkış için kolaylık olsun diye koda kendim ekledim.
            }
          


            if (e.KeyCode == Keys.Up)// klavyedeki yukarı tuşuna basarsa o yöndeki hareketi sağlayacaktır.
            {
               if (yonumuz.t != 10)//aşağı giderken yukarı gitmesini engeller.Bu if blogunu eklemeseydim oyuncu kaybedecekti.
                {
                    yonumuz = new Yon(0, -10);
                }
            }
            else if (e.KeyCode == Keys.Down)// klavyedeki aşağı tuşuna basarsa o yöndeki hareketi sağlayacaktır.
            {
                if (yonumuz.t != -10)//yukarı giderken aşağıya gitmesini engeller.Bu if blogunu eklemeseydim oyuncu kaybedecekti.
                {
                    yonumuz = new Yon(0, 10);
                }
            }
            else if (e.KeyCode == Keys.Left)// klavyedeki sol tuşuna basarsa o yöndeki hareketi sağlayacaktır.
            {
               if (yonumuz.k != 10)//sağa giderken sola gitmesini engeller.Bu if blogunu eklemeseydim oyuncu kaybedecekti.
                {
                    yonumuz = new Yon(-10, 0);
                }
            }
            else if (e.KeyCode == Keys.Right)// klavyedeki sağ tuşuna basarsa o yöndeki hareketi sağlayacaktır.
            {
                if (yonumuz.k != -10)//sola giderken sağa gitmesini engeller.Bu if blogunu eklemeseydim oyuncu kaybedecekti.
                {
                    yonumuz = new Yon(10, 0);
                }
            }
            //bunları yapabilmek için yonumuze yon değerini verdim .Yılan sınıfında 24. satırda belirttim.
        }

        private void yeniOyuncuToolStripMenuItem_Click(object sender, EventArgs e)//bu kısımda oyun esnasında farklı bir oyuncu adını kaydederek                                                                     
        {                                                                         //en başta hangi seviye seçildiyse yeni oyuncu adına oyunu başlatacaktır.
            panel1.Controls.Clear();//panelin üzerini temizledim.
            
            panel1.Enabled = true;        //paneli aktifleştirdim.
            
            richTextBox1.Enabled = true;  //Yeni isim girişi için richTextBox1 i aktifleştirdim.
            button1.Enabled = true; //Yeni oyuncunun adını kaydetmesi için button1 i aktifleştirdim.
           

        }

     
        private void button1_Click(object sender, EventArgs e)
        {

           
            isimler();//oyuncunun ismini kayıt altına almak için kullandım .

            if (radioButton2.Checked == true)//kolay seviye için
            {

                timer1.Interval = 50;//timer1 in interval ini yükselttim.
                label5.BackColor = Color.Red;
                label5.Text ="KOLAY SEVİYE";
            }
            if (radioButton1.Checked == true)//zor seviye için 
            {

                timer1.Interval = 20;//timer1 in interval ini düşürdüm.
                label5.BackColor = Color.Red;
                label5.Text = "ZOR SEVİYE";
            }

            panel1.Controls.Clear();//oyun başlamadan önce panelin üzerindeki bilgilendirmeleri ve seviye seçimini sildim.
            button1.Enabled = false;//button1 i pasifleştirdim.
            button2.Enabled = false;//button2 yi pasifleştirdim.
            richTextBox1.Enabled = false;//richTextBox ı pasifleştirdim.
           //bu aracıları pasifleştirdim çünkü aktif halde kalırlarsa oyun başlamazdı .
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Skorlar skorlar = new Skorlar();
            skorlar.Show();
         
            //Process.Start(@"c:\yilan_oyunu\oyuncu_bilgileri.txt");//button2 ye tıkladıgımız zaman yilan_oyunu klsorunden Oyuncu bilgileri ni içren text belgesi açılıyor.
            //işlem başlangıcı için kullanılan methottur . world ,exel vb. içeriklerdede kullanılır.
        }
    }
}
