using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Data.Model
{
    public class GDPRiSDBContext : DbContext
    {
        public GDPRiSDBContext() : base("name=GDPRiSDBConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Phones> Phones { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Cars> Cars { get; set; }
    }
}
