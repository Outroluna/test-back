using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace test_back.Models
{
    public class OrderItem
    {
        ///Инфо о каждом элементе заказа
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; } ///Количество заказанного товара
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }//// Цена за единицу товара

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }///Идентификатор товара который был заказан

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
