using Bank.Domain.Models;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }
        //public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(builder =>
            {
                //builder.HasData(new UserInfo
                //{
                //    Id = 1,
                //    Name = "ITHomester",
                //    Password = HashPasswordHelper.HashPassowrd("123456"),
                //    Role = Role.Admin
                //});

                builder.ToTable("USER_INFO", schema: "public").HasKey(x => x.service_number);

                //builder.Property(x => x.Id).ValueGeneratedOnAdd();

                //builder.Property(x => x.Password).IsRequired();
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
