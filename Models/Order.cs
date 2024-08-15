using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_back.Models
{
    /// Заказ: дата/время, инфо пользователя, общая сумма, лист заказанных позиций 
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
 

        [Required]
        [Column(TypeName = "decimal(18,2)")]///Тип данных в бд(с точностью до 2 знаков????)
        public decimal TotalAmount { get; set; } ///Общая сумма заказа

        [Required]
        [StringLength(50)]
        public string? Status { get; set; }

        [Required]
        public string UserId { get; set; }///Внешний ключ

        [ForeignKey("UserId")] ///Свойство UserId является внешним ключом для связи с таб User
        public User User { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    }
}
