using lesson.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace lesson.Data
{
    public class ApplicationDbContext : IdentityDbContext<lessonUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<lessonName> LessonNames { get; set; }
        public DbSet<summaryItem> Summaries { get; set; }
    }
}
