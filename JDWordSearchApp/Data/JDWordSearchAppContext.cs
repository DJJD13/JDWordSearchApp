using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JDWordSearchApp.Models;

namespace JDWordSearchApp.Data
{
    public class JDWordSearchAppContext : DbContext
    {
        public JDWordSearchAppContext (DbContextOptions<JDWordSearchAppContext> options)
            : base(options)
        {
        }

        public DbSet<JDWordSearchApp.Models.WordSearch> WordSearch { get; set; } = default!;
    }
}
