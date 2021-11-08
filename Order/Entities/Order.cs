using System;
using System.Collections.Generic;

namespace Order
{
    public enum OrderStatus
    {
        InProgress,
        PendingPayment,
        Processing,
        Shipped,
        Delivered
    }

    public class Order : EntityBase
    {
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public Customer Customer { get; private set; }

        public Order(Customer customer)
        {
            Status = OrderStatus.InProgress;
            Customer = customer;
            Items = new List<OrderItem>();
            base.DateHourRegister = DateTime.Now;
        }

        public void Summary()
        {
            Console.WriteLine($"Cliente: {Customer.Person.FullName}");
            Console.WriteLine($"Status: {Status}");

            foreach(var item in Items)
            {
                Console.WriteLine($"Produto: {item.Product.Description}");
                Console.WriteLine($"Quantidade: {item.Quantity}");
                Console.WriteLine($"Valor total: R$ {item.TotalValue:c}");
            }
        }

        public void AddItem(OrderItem orderItem)
        {
            if (Status != OrderStatus.InProgress)
                throw new OrderException("Só é possível adicionar itens em pedidos que estão em progresso!");

            Items.Add(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if(Status != OrderStatus.InProgress)
                throw new OrderException("Só é possível remover itens em pedidos que estão em progresso!");

            Items.Remove(orderItem);
        }

        public void FinalizeOrder()
        {
            if(Status != OrderStatus.InProgress)
                throw new OrderException("Só é possível finalizar pedido que esteja com o status em progresso!");

            Status = OrderStatus.PendingPayment;
        }

        public void PaymentMade()
        {
            if (Status != OrderStatus.PendingPayment)
                throw new OrderException("Só é possível finalizar pedido que esteja com o status aguardando pagamento!");

            Status = OrderStatus.Processing;
        }

        public void OrderShipped()
        {
            if (Status != OrderStatus.Processing)
                throw new OrderException("Só é possível enviar pedido que esteja com o status processando!");

            Status = OrderStatus.Shipped;
        }

        public void OrderDelivered()
        {
            if (Status != OrderStatus.Shipped)
                throw new OrderException("Só é possível entregar pedidos que estejam com o status enviado!");

            Status = OrderStatus.Delivered;
        }
    }
}
