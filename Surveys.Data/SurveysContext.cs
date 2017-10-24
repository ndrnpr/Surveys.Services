using Microsoft.EntityFrameworkCore;
using Surveys.Models.Entities;
using System;

namespace Surveys.Data
{
    public class SurveysContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Bulletin> Bulletins { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public SurveysContext()
        {
        }

        public SurveysContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SpyStore;Trusted_Connection=True;MultipleActiveResultSets=true;",
                        options => options.EnableRetryOnFailure()
                    );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bulletin>()
                .HasOne(b => b.Survey)
                .WithMany(s => s.Bulletins)
                .OnDelete(DeleteBehavior.Restrict);           
        }
    }
}
