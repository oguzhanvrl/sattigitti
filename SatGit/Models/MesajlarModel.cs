using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatGit.Models
{
    public class MesajlarModel
    {
        public int MesajID { get; set; }
        public int GondericiID { get; set; }
        public int AliciID { get; set; }
        public DateTime MesajTarihi { get; set; }
        public int UrunID { get; set; }
        public string MesajIcerik { get; set; }
    }
}