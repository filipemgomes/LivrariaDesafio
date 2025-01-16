using Domain.Entities;
using Infrastructure.Persistence.Views;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<BookAuthorView> BookAuthorViews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            modelBuilder.Ignore<FluentValidation.Results.ValidationFailure>();
            modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();
                        
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(b => b.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(b => b.PurchaseMode)
                    .HasConversion<int>() 
                    .IsRequired();

                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Subject)
                    .WithMany(s => s.Books)
                    .HasForeignKey(b => b.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
                        
            modelBuilder.Entity<Author>().ToTable("Author");
                        
            modelBuilder.Entity<Subject>().ToTable("Subject");
                        
            modelBuilder.Entity<BookAuthorView>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView("BookAuthorView"); 

                entity.Property(b => b.BookPrice)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
