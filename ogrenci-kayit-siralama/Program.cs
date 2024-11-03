using System;

public class Ogrenci
{
    public int Numara { get; set; }       // Öğrenci numarası
    public string Ad { get; set; }        // Öğrencinin adı
    public string Soyad { get; set; }     // Öğrencinin soyadı
    public Ogrenci SonrakiKisi { get; set; }  // Bağlı listede sonraki kişi için kullanacağız aşağıdaki nesneden
                                              // bilgileri alacak

    // Öğrenci bilgilerini alacağımız öğrenci nesnesini oluşturuyoruz
    public Ogrenci(int numara, string ad, string soyad)
    {
        Numara = numara;
        Ad = ad;
        Soyad = soyad;
        SonrakiKisi = null;  // Önce sonraki öğrenci kısmını boş ayarlıyoruz yani null
    }
}

public class OgrenciListesi
{
    private Ogrenci baslangic; // listenin başındaki öğrenciyi gösterecek yani headNode oluşturuyoruz   

    // Yeni öğrenci eklemek için metot oluşturuyoruz
    public void Ekle(int numara, string ad, string soyad)
    {
        var yeniOgrenci = new Ogrenci(numara, ad, soyad); // Yeni öğrenci için de nesne oluşturuyoruz

        // Eğer liste boşsa ya da yeni öğrencinin numarası listenin başındaki öğrenciden küçükse
        if (baslangic == null || baslangic.Numara > numara)
        {
            yeniOgrenci.SonrakiKisi = baslangic; // Yeni öğrenciyi en başa alıyoruz
            baslangic = yeniOgrenci; // Baştaki öğrenciyi yeni öğrencinin yerine alıyoruz
            return;
        }

        var mevcut = baslangic; // Listenin en başından başlatarak doğru yere eklemesi için veriyi oluşturuyoruz

        // Buradaki mevcut verimizin listedeki en uygun yere gelmesi için gerekli kodları yazıyoruz
        while (mevcut.SonrakiKisi != null && mevcut.SonrakiKisi.Numara < numara)
            mevcut = mevcut.SonrakiKisi;

        // Yeni öğrenci olması gereken yere ekleniyor
        yeniOgrenci.SonrakiKisi = mevcut.SonrakiKisi;
        mevcut.SonrakiKisi = yeniOgrenci;
    }

    // Öğrenci silmek için metot oluşturuyoruz
    public void Sil(int numara)
    {
        // Baştaki öğrencinin numarası silinecek numaraya eşitse buradaki if yapısı devreye girecek
        if (baslangic != null && baslangic.Numara == numara)
        {
            baslangic = baslangic.SonrakiKisi; // Ardından başlangıçtaki öğrenci listeden çıkarılacak
            return;
        }

        var mevcut = baslangic; // Listenin başlangıcından itibaren öğrenciyi bulmak için var mevcut'u tekrar 
                                // başlangıçtan başlatıyoruz

        // Öğrenci bulunana kadar listeyi kontrol ediyor
        while (mevcut != null && mevcut.SonrakiKisi != null && mevcut.SonrakiKisi.Numara != numara)
            mevcut = mevcut.SonrakiKisi;

        // Eğer öğrenci bulunduysa onu listeden çıkarıyoruz
        if (mevcut != null && mevcut.SonrakiKisi != null)
            mevcut.SonrakiKisi = mevcut.SonrakiKisi.SonrakiKisi;
    }

    // Öğrencileri listelemek için metot oluşturuyoruz
    public void Listele()
    {
        var mevcut = baslangic; // Listenin başlangıcından başlaması için mevcut verimizi tekrar başlangıca alıyoruz
        Console.WriteLine("Numara\tAd\tSoyad");
        while (mevcut != null)
        {
            // Tüm öğrencilerin bilgisini yazdırıyoruz
            Console.WriteLine($"{mevcut.Numara}\t{mevcut.Ad}\t{mevcut.Soyad}");
            mevcut = mevcut.SonrakiKisi; // Sonraki öğrenciye geçiyor
        }
    }
}

class Program
{
    static void Main()
    {
        var ogrenciListesi = new OgrenciListesi(); // Öğrenci listesi sınıfından yeni bir nesne oluşturuyoruz

        while (true) // Sürekli olarak kullanıcıdan seçim alacak döngü
        {
            // Kullanıcının yapacağı işlemi seçmesi için seçenekleri oluşturuyoruz
            Console.WriteLine("1- Öğrenci Ekle\n2- Öğrenci Sil\n3- Öğrencileri Listele\n4- Çıkış\nSeçiminiz: ");
            int secim = int.Parse(Console.ReadLine());

            if (secim == 1) // Eğer kullanıcı 1 numarayı seçerse öğrenci ekleme işlemi yapacak
            {
                Console.Write("Numara: ");
                int numara = int.Parse(Console.ReadLine());
                Console.Write("Ad: ");
                string ad = Console.ReadLine();
                Console.Write("Soyad: ");
                string soyad = Console.ReadLine();
                ogrenciListesi.Ekle(numara, ad, soyad);
            }
            else if (secim == 2) // Eğer kullanıcı 2 numarayı seçerse öğrenci silme işlemi yapacak
            {
                Console.Write("Silinecek numara: ");
                int numara = int.Parse(Console.ReadLine());
                ogrenciListesi.Sil(numara);
            }
            else if (secim == 3) // Eğer kullanıcı 3 numarayı seçerse öğrencileri listeleyecek
            {
                ogrenciListesi.Listele();
            }
            else if (secim == 4) // Çıkış seçeneği
            {
                break; // Döngüyü sonlandırmak için break
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
        }
    }
}