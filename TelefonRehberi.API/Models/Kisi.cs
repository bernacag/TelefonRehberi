using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    public class Kisi
    {
        public int Id { get; set; }

        public int UUID { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string  Firma { get; set; }

        public bool SilindiMi { get; set; }


    }
}
