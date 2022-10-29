using Microsoft.EntityFrameworkCore;
using CrudTest.Models;
namespace CrudTest.Data
{
    public class ApplicationDbContext : DbContext
    {




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

            modelBuilder.Entity<AuthorModel>().Property(a => a.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<BookModel>().Property(b => b.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<BookModel>().Property(b => b.ISBN).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<BookModel>().Property(b => b.Publisher).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<BookModel>().HasOne(b => b.AuthorModel).WithMany(a => a.BookModels).HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<LibraryModel>().Property(l => l.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<UserModel>().Property(u => u.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserModel>().Property(u => u.Password).IsRequired().HasMaxLength(50);


            modelBuilder.Entity<BookModel>()
                        .HasOne<AuthorModel>(A => A.AuthorModel)
                        .WithMany(b => b.BookModels)
                        .HasForeignKey(A => A.AuthorId);

            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
