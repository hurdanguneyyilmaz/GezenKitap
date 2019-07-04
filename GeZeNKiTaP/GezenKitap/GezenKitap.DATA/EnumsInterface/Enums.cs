using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.EnumsInterface
{
    public enum OrderState
    {
        Iptal = -1,
        Istek = 0,
        Kargolandi = 1,
        Tamamlandi = 2
    }

    //public enum Cinsiyet
    //{
    //    Kiz = 0,
    //    Erkek = 1
    //}

    //public enum Gozluk
    //{
    //    Gozluksuz = 0,
    //    Gozluklu = 1
    //}

    //public class Ogrenci
    //{
    //    public string Adi { get; set; }
    //    public string Soyadi { get; set; }
    //    public int NO { get; set; }
    //    public Cinsiyet cinsiyet { get; set; }
    //    public Gozluk gozluk { get; set; }
    //}

    //public class Sinif
    //{
    //    public List<Ogrenci> ogrenciler { get; set; }

    //    public IEnumerable<Ogrenci> ErkekOgrenciler()//entityframework kullanmadığımız için IEnumerable kullandık.
    //    {
    //        return ogrenciler.Where(x => x.cinsiyet == Cinsiyet.Erkek);
    //    }


    //}
}
