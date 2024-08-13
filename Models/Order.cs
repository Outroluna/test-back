namespace test_back.Models
{
    /// Заказ: дата/время, инфо пользователя, общая сумма, лист заказанных позиций 
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public decimal TotalAmount { get; set; } // Суммарная стоимость заказа
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    }
}
