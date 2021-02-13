using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    [Table("Iletisim")]
    public class Iletisim
    {
        public BilgiTip BilgiTipi { get; set; }

        public string BilgiIcerigi { get; set; }

        [ForeignKey(nameof(Kisi))]
        public int KisiId { get; set; }

        public Kisi Kisi { get; set; }
    }
}
