using LibraryInventory.Models;
using System.Data.Entity;

namespace LibraryInventory.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public LibraryContext() : base("name=LibraryDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasRequired(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}