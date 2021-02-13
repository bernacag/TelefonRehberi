using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    public class Kisi : BaseModel
    {

        public int UUID { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string Firma { get; set; }

        public ICollection<Iletisim> Iletisimler { get; set; }
    }
}
