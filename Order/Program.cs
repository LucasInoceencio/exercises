using System;
using System.Collections.Generic;

namespace Order
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> _customers = new List<Customer>();
            List<Product> _products = new List<Product>();
            List<Order> _orders = new List<Order>();

            Console.WriteLine("Seja bem-vindo!");

            var chosenOption = -1;

            while(chosenOption != 0)
            {
                chosenOption = Utils.ChooseMenuOption();

                switch (chosenOption)
                {
                    case 1:
                        _customers.Add(Utils.RegisterCustomer());
                        break;
                    case 2:
                        _products.Add(Utils.RegisterProduct());
                        break;
                    case 3:
                        _orders.Add(Utils.RegisterOrder(_customers, _products));
                        break;
                    case 4:
                        Utils.ListCustomers(_customers);
                        break;
                    case 5:
                        Utils.ListProducts(_products);
                        break;
                    case 6:
                        Utils.ListOrders(_orders);
                        break;
                    default:
                        Message.InvalidOption();
                        break;
                }
            }

            Environment.Exit(0);
        }
    }
}
