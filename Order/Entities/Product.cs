using System;

namespace Order
{
    public class Product : EntityBase
    {
        public string Description { get; private set; }
        public decimal UnitaryValue { get; private set; }
        public int AvailableQuantity { get; private set; }

        public Product(string description, decimal unitaryValue, int availableQuantity)
        {
            Description = description;
            UnitaryValue = unitaryValue;
            AvailableQuantity = availableQuantity;
            base.DateHourRegister = DateTime.Now;
        }

        public void AddAvailableQuantity(int quantity)
        {
            AvailableQuantity += quantity;
        }

        public void RemoveAvailableQuantity(int quantity)
        {
            if (quantity > AvailableQuantity)
                throw new OrderException("Quantidade disponível insuficiente");

            AvailableQuantity -= quantity;
        }
    }
}
