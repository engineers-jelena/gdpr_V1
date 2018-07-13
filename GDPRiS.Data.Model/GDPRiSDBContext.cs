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

        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
