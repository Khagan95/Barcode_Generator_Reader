using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using System.IO;

namespace BarcodeUygulamasi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Barcode Generator/Reader Uygulamasına Hoş Geldiniz!");

            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Barcode Üret");
                Console.WriteLine("2. Barcode Oku");
                Console.WriteLine("3. Çıkış");

                int secim = Convert.ToInt32(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        BarcodeUret();
                        break;
                    case 2:
                        BarcodeOku();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }

        static void BarcodeUret()
        {
            Console.WriteLine("Üretilecek barcode verisini girin:");
            string veri = Console.ReadLine();

            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE; // Barcode formatını QR Code olarak ayarla
            EncodingOptions encodingOptions = new EncodingOptions();
            encodingOptions.Width = 300; // Genişlik
            encodingOptions.Height = 300; // Yükseklik
            barcodeWriter.Options = encodingOptions;

            Bitmap barcodeBitmap = barcodeWriter.Write(veri); // Veri ile barcode'u üret

            string dosyaAdi = $"barcode_{DateTime.Now:yyyyMMddHHmmss}.png"; // Dosya adı oluştur

            barcodeBitmap.Save(dosyaAdi); // Barcode'u resim olarak kaydet
            Console.WriteLine($"Barcode üretildi ve {dosyaAdi} olarak kaydedildi.");
        }

        static void BarcodeOku()
        {
            Console.WriteLine("Okunacak barcode dosyasının yolunu girin:");
            string dosyaYolu = Console.ReadLine();

            if (File.Exists(dosyaYolu))
            {
                Bitmap barcodeBitmap = new Bitmap(dosyaYolu);

                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode(barcodeBitmap); // Barcode'u oku

                if (result != null)
                {
                    Console.WriteLine($"Okunan barcode verisi: {result.Text}");
                }
                else
                {
                    Console.WriteLine("Barcode okunamadı.");
                }
            }
            else
            {
                Console.WriteLine("Belirtilen dosya bulunamadı.");
            }
        }
    }
}
