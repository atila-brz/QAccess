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

        public DbSet<Unit> units { get; set; }
        public DbSet<Correspondence> correspondences { get; set; }
        public DbSet<Ocurrence> ocurrences { get; set; }
        public DbSet<UserEmployee> userEmployees { get; set; }
        public DbSet<UserOwner> userOwners { get; set; }
    }
}