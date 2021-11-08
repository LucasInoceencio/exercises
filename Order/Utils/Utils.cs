using System;
using System.Collections.Generic;
using System.Globalization;

namespace Order
{
    public static class Utils
    {
        public static Customer RegisterCustomer()
        {
            string firstName;
            string lastName;
            Cpf cpf = string.Empty;
            Email email = string.Empty;
            DateTime birthDate = DateTime.MinValue;

            Console.WriteLine("Insira o primeiro nome do cliente: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Insira o sobrenome do cliente: ");
            lastName = Console.ReadLine();
            while (!cpf.IsValid)
            {
                Console.WriteLine("Insira o cpf do cliente sem pontos e traços: ");
                cpf = Console.ReadLine();
            }
            
            while (!email.IsValid)
            {
                Console.WriteLine("Insira o e-mail do cliente: ");
                email = Console.ReadLine();
            }
            
            while (birthDate == DateTime.MinValue)
            {
                try
                {
                    Console.WriteLine("Insira a data de nascimento do cliente, no formato dd/MM/yyyy: ");
                    birthDate = Convert.ToDateTime(Console.ReadLine(), new CultureInfo("pt-BR"));
                }
                catch (Exception)
                {
                    Console.WriteLine("Data inserida no formato errado, tente novamente");
                }
            }

            try
            {
                var person = new Person(firstName, lastName, birthDate, cpf, email);
                var customer = new Customer()
                {
                    Person = person,
                    Active = true
                };

                return customer;
            }
            catch(OrderException ex)
            {
                Console.WriteLine("Erro ao cadastrar cliente:");
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro desconhecido:");
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static Product RegisterProduct()
        {
            string productDescription;
            int productAvailable = -1;
            decimal productUnitaryValue = -1M;

            Console.WriteLine("Insira a descrição do produto: ");
            productDescription = Console.ReadLine();

            while(productAvailable == -1)
            {
                try
                {
                    Console.WriteLine("Insira a quantidade disponível: ");
                    productAvailable = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }

            while(productUnitaryValue == -1M)
            {
                try
                {
                    Console.WriteLine("Insira o valor unitário do produto: ");
                    productUnitaryValue = Convert.ToDecimal(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }

            var product = new Product(productDescription, productUnitaryValue, productAvailable);
            return product;
        }
        
        public static Order RegisterOrder(List<Customer> customers, List<Product> products)
        {
            int indexCustomer = -1;
            do
            {
                try
                {
                    ListCustomers(customers);
                    Console.WriteLine("Informe o número de um cliente válido: ");
                    indexCustomer = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (indexCustomer == -1 || indexCustomer > customers.Count);

            var customer = customers[indexCustomer - 1];

            var order = new Order(customer);

            // Fazer o order item e add na order
            var chooseMoreProducts = 1;

            do
            {
                try
                {
                    Console.WriteLine("Escolha o número de um produto válido: ");
                    ListProducts(products);
                    var indexProduct = ChooseProduct(products.Count);
                    var product = products[indexProduct - 1];
                    var quantity = ChooseQuantity(product.AvailableQuantity);

                    order.AddItem(new OrderItem(product, quantity));

                    Console.WriteLine("Deseja escolher mais produtos?");
                    Console.WriteLine("1 - Sim");
                    Console.WriteLine("2 - Não");

                    chooseMoreProducts = ChooseMoreProducts();
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (chooseMoreProducts == 1);

            return order;
        }

        public static int ChooseMoreProducts()
        {
            int chooseMoreProducts = -1;
            do
            {
                try
                {
                    Console.WriteLine("Escolha um número de produto válido:");
                    chooseMoreProducts = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (chooseMoreProducts < 1 || chooseMoreProducts > 2);

            return chooseMoreProducts;
        }

        public static int ChooseProduct(int maxIndex)
        {
            int productIndex = 0;
            do
            {
                try
                {
                    Console.WriteLine("Escolha um número de produto válido:");
                    productIndex = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (productIndex < 1 || productIndex > maxIndex);

            return productIndex;
        }

        public static int ChooseQuantity(int maxQuantity)
        {
            int quantity = 0;
            do
            {
                try
                {
                    Console.WriteLine("Escolha uma quantidade válida:");
                    quantity = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (quantity < 1 || quantity > maxQuantity);

            return quantity;
        }

        public static int ChooseMenuOption()
        {
            var result = -1;
            do
            {
                try
                {
                    Message.Menu();
                    result = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Message.InvalidInput();
                }
            }
            while (result == -1);

            return result;
        }

        public static void ListCustomers(List<Customer> customers)
        {
            if(customers == null || customers.Count == 0)
            {
                Console.WriteLine("Nenhum cliente foi registrado");
                return;
            }

            var count = 1;
            Console.WriteLine("Id | Nome | CPF | E-mail | Idade");
            foreach(var customer in customers)
            {
                Console.WriteLine($"{count} | {customer.Person?.FullName} | {customer.Person?.Cpf.CpfFormatado} | {customer.Person?.Email} | {customer.Person?.Age}");
                Console.WriteLine();
                count++;
            }
        }

        public static void ListProducts(List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("Nenhum produto foi registrado");
                return;
            }

            var count = 1;
            Console.WriteLine("Id | Descrição | Valor unitário | Quantidade disponível");
            foreach (var product in products)
            {
                Console.WriteLine($"{count} | {product.Description} | {product.UnitaryValue:c} | {product.AvailableQuantity}");
                Console.WriteLine();
                count++;
            }
        }
        
        public static void ListOrders(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine("Nenhuma venda foi registrada");
                return;
            }

            var count = 1;
            foreach (var order in orders)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine($"Id da venda: {count}");
                Console.WriteLine($"Cliente: {order.Customer?.Person?.FullName}");
                Console.WriteLine($"Status do pedido: {order.Status}");
                Console.WriteLine();
                Console.WriteLine("Itens do pedido");
                Console.WriteLine("Produto | Quantidade | Valo unitário | Valor total");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"{item.Product?.Description} | {item.Quantity} | R$ {item.Product?.UnitaryValue:c} | R$ {item.TotalValue:c}");
                }
                Console.WriteLine("--------------------------------------------");
            }
        }
    }
}
