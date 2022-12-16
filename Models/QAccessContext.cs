using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QAccess.Models
{
    public class QAccessContext: DbContext
    {
        public QAccessContext(DbContextOptions<QAccessContext> options) : base(options){}

        public DbSet<Unit> Units { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<Occurrence> Occurrences { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Condominium> Codominiums { get; set; }
        public DbSet<Publication> Publications { get; set; }
        
    }
}