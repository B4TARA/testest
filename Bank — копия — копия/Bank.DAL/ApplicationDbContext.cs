using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated(); //создать автоматически, если БД не существует
        }
        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(builder =>
            {
                //builder.HasData(new UserInfo //добавить дефолтного
                //{
                //    Id = 1,
                //    Name = "ITHomester",
                //    Password = HashPasswordHelper.HashPassowrd("123456"),
                //    Role = Role.Admin
                //});

                builder.ToTable("USER_INFO", schema: "public").HasKey(x => x.service_number); //установить ключ

                //builder.Property(x => x.Id).ValueGeneratedOnAdd(); //автогенерируемое

                //builder.Property(x => x.Password).IsRequired(); //необходимое
                //builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                //builder.HasOne(x => x.Profile) 
                //    .WithOne(x => x.User)
                //    .HasPrincipalKey<User>(x => x.Id)
                //    .OnDelete(DeleteBehavior.Cascade);

                //builder.HasOne(x => x.Basket)
                //    .WithOne(x => x.User)
                //    .HasPrincipalKey<User>(x => x.Id)
                //    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
