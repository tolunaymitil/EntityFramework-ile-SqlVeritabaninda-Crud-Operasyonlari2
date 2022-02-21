using EfCoreExample.Dto;
using EfCoreExample.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PersonelBilgileri> personeller = GetAll();

            foreach (var personel in personeller)
            {
                string strPersonalData = $"{personel.adsoyad}";
                Console.WriteLine(strPersonalData);
            }

            Console.WriteLine("Personel Ekleme İşlemine Hoşgeldiniz");
            Console.Write("Adsoyad Giriniz:");
            string adSoyadInput = Console.ReadLine();
            Console.Write("Adres Giriniz:");
            string adresInput = Console.ReadLine();

            Console.Write("Tarih Giriniz:");
            string tarihInput = Console.ReadLine();

            Console.Write("Saat Giriniz:");
            string saatInput = Console.ReadLine();



            Insert(new PersonelBilgileriInsertInput { adsoyad = adSoyadInput, adres = adresInput, telefon = "", email = "", tarih = tarihInput, saat = saatInput });
        }
        static List<PersonelBilgileri> GetAll()
        {
            List<PersonelBilgileri> personelBilgileris = new();
            using (var db = new ETrade2DbContext())
            {
                personelBilgileris = db.personelbilgileri.ToList();


            }
            return personelBilgileris;
        }
        static ResultModel Insert(PersonelBilgileriInsertInput personel)
        {
            using (var db = new ETrade2DbContext())
            {

                DateTime tarihSaat = DateTime.Parse(personel.tarih + "T" + personel.saat);
                PersonelBilgileri personelEntity = new PersonelBilgileri
                {
                    adsoyad = personel.adsoyad,
                    adres = personel.adres,
                    telefon = personel.telefon,
                    email = personel.email,
                    tarihsaat = tarihSaat


                };

                db.Add(personelEntity);
                db.SaveChanges();
            }

            return new ResultModel
            {
                ReasonMessage = "Başarılı",
                ReasonCode = 0
            };
        }

        static ResultModel Delete(int mno)
        {

            using (var db = new ETrade2DbContext())
            {
                PersonelBilgileri silinecekPersonel =
                    db.personelbilgileri.FirstOrDefault(p => p.mno == mno);
                db.personelbilgileri.Remove(silinecekPersonel);
                db.SaveChanges();

            }
            return new ResultModel
            { ReasonCode = 0, ReasonMessage = "Silindi" };
        }



        public class ResultModel
        {
            public string ReasonMessage { get; set; }
            public int ReasonCode { get; set; }
        }
    }
}
