using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;//point komutunu kullanabilmek için yazdım.
using System.IO;
namespace DÖNEM_PROJESİ
{
    class Yilan //yılanın gövdesi,ilerlemesi,yönü,büyümesi için kullandıgım sınıf.
    {
        beden[] parca;//yılanın gövdesi için oluşturdugum dizi.
        int yilan_buyuklugu;
        Yon yonumuz;
        public Yilan()
        {
            
            parca = new beden[3];//yilanın boyutunun 3 parca olacak şekilde kodladım. 
            yilan_buyuklugu = 3;
            parca[0] = new beden(150, 150);// gövdenin panel üzerinde başlayacaı kısmı konumlandırdım .
            parca[1] = new beden(160, 150);//bu kısımlarada 1. kısma ek devam etmektedir.
            parca[2] = new beden(170, 150);
        }
        public void İlerle(Yon yon)//yılanı yon sınıfı sayesinde ilrlettim.
        {
            yonumuz = yon;
            if (yon.k == 0 && yon.t == 0) //yonlerin akıbeti bu sekilde ise birsey yapmadım.
            {

            }
            else//yönsüz kalmamışsak :)
            {
                for (int i = parca.Length - 1; i > 0; i--)//arkadaki parca öndekini takip edeceği için yılan parcasının sonuncusundan başladım.
                {
                    parca[i] = new beden(parca[i - 1].a, parca[i - 1].b);//gerideki parçaların ödekilerini takip etmesini sağladım.

                }
                parca[0] = new beden(parca[0].a + yon.k, parca[0].b + yon.t);//yılanımızın baş kısmının belirlenecek yonde gitmesini sağladım.Bu sayaede hepsi birbirini taki edecektir.


            }
        }
        public void Buyu()
        {
            Array.Resize(ref parca, parca.Length + 1);//yeniden boyutlandırmak için kullandım.Çünkü yılanımız büyüyecek.:)parca dizimizin boyutunu 1 arttırdım.
            parca[parca.Length - 1] = new beden(parca[parca.Length - 2].a - yonumuz.k, parca[parca.Length - 2].b- yonumuz.t);//yeni parcamız için bi öncekinin konumunu eksilterek yeni parcayı ekledim.
            yilan_buyuklugu++;
        }
        public Point GetPos(int numara)// hangi parça isteniyorsa yılan sınıfıdan onu dondürmek için kullandım.
        {
            return new Point (parca[numara].a, parca[numara].b);//yılan parçalarının konumunu döndürdüm.
        }
        public int Yilan_buyuklugu //yılanın kendisine çarpma durumuna bakmak için kullandım.
        {
            get
            {
                return yilan_buyuklugu;
            }
        }
    }
    class beden 
    {
        public int a;
        public int b;
        public readonly int size_x;//readonly komutlarıyla sadece okunur yaptım . Artık sonradan değistirilemezler.
        public readonly int size_y;
        public beden(int x, int y)
        {
            a = x;
            b = y;
            size_x = 10;
            size_y = 10;
        }
    }
    class Yon //yılanın yönünü belirleyecek olan  sınıf.
    {
        public readonly int k;
        public readonly int t;
        public Yon(int x, int y)
        {
            k= x;
            t = y;
        }
    }

   

}

