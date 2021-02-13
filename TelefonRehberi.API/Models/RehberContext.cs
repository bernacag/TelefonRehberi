using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.Models
{
    public class RehberContext : DbContext
    {
        public DbSet<Kisi> Kisi { get; set; }

        public DbSet<Iletisim> Iletisim { get; set; }
    }
}
