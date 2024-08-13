namespace test_back.Models
{
    public class OrderItem
    {
        ///Отдельная позиция в заказе: 
        public int Id { get; set; }
        public int Quantity { get; set; } //Количество заказанного товара
        public decimal Price { get; set; } // Цена за единицу товара на момент заказа


        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
