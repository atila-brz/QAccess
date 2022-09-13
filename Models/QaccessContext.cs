using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QAccess.Models
{
    public class QaccessContext: DbContext
    {
        public QaccessContext(DbContextOptions<QaccessContext> options) : base(options){}

        
    }
}