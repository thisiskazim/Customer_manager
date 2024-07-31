using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Customer_manager.Models
{

		public class ApplicationDbContext : IdentityDbContext<Customers, IdentityRole, string>
    {
			public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
			{
			}


			public DbSet<Customer_infos> CustomerInfos { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Hizmetler> Hizmetler { get; set; }
        public DbSet<Ozgecmis> Ozgecmis { get; set; }
        public DbSet<Portfolyo> Portfolyo { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<SSS> SSS { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
			base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Customer_infos>()
      .HasKey(ci => ci.CustomerInfoId);

            modelBuilder.Entity<Customers>()
          .HasIndex(c => c.CustomerKey)
          .IsUnique();

            modelBuilder.Entity<Customers>()
     .HasOne(c => c.CustomerInfo)
     .WithOne(ci => ci.Customer)
     .HasForeignKey<Customer_infos>(ci => ci.CustomerKey)
     .HasPrincipalKey<Customers>(c => c.CustomerKey);


            //     modelBuilder.Entity<Customers>()
            //.HasOne(c => c.CustomerInfo)
            //.WithOne(ci => ci.Customer)
            //.HasForeignKey<Customers>(c => c.CustomerInfoId);

            //         // Customer_infos için birincil anahtar tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasKey(ci => ci.CustomerInfoId);

            //         // Customers ve Customer_infos arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customers>()
            //             .HasOne(c => c.CustomerInfo)
            //             .WithOne(ci => ci.Customer)
            //             .HasForeignKey<Customer_infos>(ci => ci.CustomerKey)
            //             .HasPrincipalKey<Customers>(c => c.CustomerKey);

            //         // Customer_infos ve About arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasMany(ci => ci.About)
            //             .WithOne(a => a.CustomerInfo)
            //             .HasForeignKey(a => a.CustomerInfoId);

            //         // Customer_infos ve Contact arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasMany(ci => ci.Contact)
            //             .WithOne(c => c.CustomerInfo)
            //             .HasForeignKey(c => c.CustomerInfoId);


            //modelBuilder.Entity<Customer_infos>()
            // .HasMany(ci => ci.Hizmetler)
            // .WithOne(h => h.CustomerInfo)
            // .HasForeignKey(h => h.CustomerInfoId);

            //          modelBuilder.Entity<Hizmetler>().
            //             HasIndex(ci => ci.CustomerInfoId).
            //             IsUnique(false);





            //         // Customer_infos ve Ozgecmis arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasMany(ci => ci.Ozgecmis)
            //             .WithOne(o => o.CustomerInfo)
            //             .HasForeignKey(o => o.CustomerInfoId);

            //         // Customer_infos ve Portfolyo arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasMany(ci => ci.Portfolyo)
            //             .WithOne(p => p.CustomerInfo)
            //             .HasForeignKey(p => p.CustomerInfoId);

            //         // Customer_infos ve Skills arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //             .HasMany(ci => ci.Skills)
            //             .WithOne(s => s.CustomerInfo)
            //             .HasForeignKey(s => s.CustomerInfoId);

            //         // Customer_infos ve SSS arasında bire bir ilişki tanımlaması
            //         modelBuilder.Entity<Customer_infos>()
            //            .HasMany(ci => ci.Hizmetler)
            //            .WithOne(h => h.CustomerInfo)
            //            .HasForeignKey(h => h.CustomerInfoId);
        }
		
	}
}
