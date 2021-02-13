using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    public enum BilgiTip
    {
        Telefon,
        Email,
        Konum
    }

    public class Iletisim
    {
        public int Id { get; set; }

        public BilgiTip BilgiTipi { get; set; }

        public string BilgiIcerigi { get; set; }

    }
}
