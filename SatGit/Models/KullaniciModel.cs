using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatGit.Models
{
    public class KullaniciModel
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSifre { get; set; }
        public string KullaniciMail { get; set; }
    }
}