using LibraryInventory.Models;
using System.Data.Entity;

namespace LibraryInventory.Data
{
    /// <summary>
    /// Represents the database context for the library system.
    /// Manages entity sets and configures relationships between entities.
    /// </summary>
    public class LibraryContext : DbContext
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

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