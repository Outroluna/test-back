using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using test_back.Models;
namespace test_back.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///Установка отношений
            base.OnModelCreating(modelBuilder);

            ///Один ко многим: один category может содержать много product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId); ///product -> ключ CategoryId

            ///Один ко многим: один order может содержать много orderitem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId); ///orderitem -> ключ orderid

            ///Многие к одному: один product может быть частью многих orderitem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId); ///orderitem -> ключ productid

            //стартовые данные
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Футболки, топы и поло" },
                new Category { Id = 2, Name = "Блузы и рубашк " },
                new Category { Id = 3, Name = "Платья и сарафаны" },
                new Category { Id = 4, Name = "Джинсы и джеггинсы" },
                new Category { Id = 5, Name = "Брюки, бриджи и капри" }
            );
        }


    }

    }
