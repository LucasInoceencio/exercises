using System;

namespace Order
{
    public class OrderItem : EntityBase
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue => Quantity * Product.UnitaryValue;

        public OrderItem(Product product, int quantity)
        {
            Product = product;

            if (quantity <= 0)
                throw new OrderException("Quantidade inválida. Informe uma quantidade igual ou superior a um.");

            if (quantity > product.AvailableQuantity)
                throw new OrderException("Quantidade inválida. Informe uma quantidade menor ou igual a quantidade máxima disponível.");

            Quantity = quantity;
            base.DateHourRegister = DateTime.Now;
            product.RemoveAvailableQuantity(quantity);
        }
    }
}
