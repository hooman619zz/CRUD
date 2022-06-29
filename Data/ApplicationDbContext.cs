using Microsoft.EntityFrameworkCore;
using CrudTest.Models;
namespace CrudTest.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Dbset
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<LibraryModel> Libraries { get; set; }
        #endregion


        #region Save Change Override
        public override int SaveChanges()
        {
            HandleBookDelete();
            HandleBookAuthor();
            return base.SaveChanges();
        }
        #endregion


        #region Handle delete
        private void HandleBookDelete()
        {
            var entities = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (entity.Entity is BookModel)
                {
                    entity.State = EntityState.Modified;
                    var book = entity.Entity as BookModel;
                    book.IsDeleted = true;
                }

            }
        }

        private void HandleBookAuthor()
        {
            var entities = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (entity.Entity is AuthorModel)
                {
                    entity.State = EntityState.Modified;
                    var author = entity.Entity as AuthorModel;
                    author.IsDeleted = true;
                }

            }
        }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookModel>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<AuthorModel>().HasQueryFilter(b => !b.IsDeleted);


            modelBuilder.Entity<BookModel>()
                        .HasOne<AuthorModel>(A => A.AuthorModel)
                        .WithMany(b => b.BookModels)
                        .HasForeignKey(A => A.AuthorId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
