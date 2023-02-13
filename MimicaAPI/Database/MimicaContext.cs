using Microsoft.EntityFrameworkCore;
using MimicaAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicaAPI.Database
{
    public class MimicaContext : DbContext
    {
        public MimicaContext(DbContextOptions<MimicaContext> options) : base(options)
        {

        }
        public DbSet<Palavra> Palavras { get; set; }
    }
}
