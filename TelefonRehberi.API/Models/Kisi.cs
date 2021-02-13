using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    [Table("Kisi")]
    public class Kisi : BaseModel
    {
        public int UUID { get; set; }

        [Required(ErrorMessage = "Kişi adı zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string Ad { get; set; }

        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string Soyad { get; set; }

        [StringLength(50, ErrorMessage = "Firma en fazla 50 karakter olabilir")]
        public string Firma { get; set; }

        public ICollection<Iletisim> Iletisimler { get; set; }
    }
}
