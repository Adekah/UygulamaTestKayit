using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UygulamaTestProjesi.Models;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.IO;

namespace UygulamaTestProjesi.Data
{
    public class Data
    {
        public static tbl_Kullanici KullaniciGiris(string _username,string _password)
        {
            using (UygulamaTestDBEntities login = new UygulamaTestDBEntities())
            {
                var _user = login.tbl_Kullanici.Where(a => a.Username == _username && a.Password == _password).SingleOrDefault();
                return _user;
            }

        }

        public static List<tbl_Kategori> getKategori()
        {
            using (UygulamaTestDBEntities db = new UygulamaTestDBEntities())
            {
                return db.tbl_Kategori.ToList();
            }
        }
        public static List<tbl_Sonuc> getSonuc()
        {
            using(UygulamaTestDBEntities db=new UygulamaTestDBEntities())
            {
                return db.tbl_Sonuc.ToList();
            }
        }
        public static string getcategoryname(int? _ID)
        {
            using (UygulamaTestDBEntities db = new UygulamaTestDBEntities())
            {
                return db.tbl_Kategori.Where(a => a.Kategori_ID == _ID).Select(a => a.Kategori_Adi).SingleOrDefault();
            }
        }
        public static string gettestsonuc(int? _ID)
        {
            using (UygulamaTestDBEntities db = new UygulamaTestDBEntities())
            {
                return db.tbl_Sonuc.Where(a => a.Sonuc_ID == _ID).Select(a => a.Sonuc).SingleOrDefault();
            }
        }
        public static string getkullanici(int? _ID)
        {
            using (UygulamaTestDBEntities db = new UygulamaTestDBEntities())
            {
                return db.tbl_Kullanici.Where(a => a.Kullanici_ID == _ID).Select(a => a.AdSoyad).SingleOrDefault();
            }
        }








    }
}