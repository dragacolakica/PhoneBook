using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Models;

namespace UserApp.Data
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options) : base(options) { }
        public DbSet<Email> Email { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ContactTag> ContactTag { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactTag>()
           .HasKey(t => new { t.ContactId, t.TagId });

            modelBuilder.Entity<ContactTag>()
                .HasOne(pt => pt.Contact)
                .WithMany(p => p.ContactTag)
                .HasForeignKey(pt => pt.ContactId);
            modelBuilder.Entity<ContactTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(p => p.ContactTag)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
