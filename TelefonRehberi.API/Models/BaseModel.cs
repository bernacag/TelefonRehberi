using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool SilindiMi { get; set; }
    }
}
