using R2S.Training.ADO;
using R2S.Training.Entities;

namespace R2S.Training.Main
{
    class Manager
    {
        private DatabaseCRUD _database;
        private CustomerADO _customerADO;
        private OrderADO _orderADO;
        private LineItemADO _lineItemADO;
        private EmployeeDAO _employeeDAO;
        private ProductDAO _productDAO;
        private enum MenuOption
        {
            CreateCustomer = 1,
            DeleteCustomer = 2,
            UpdateCustomer = 3,
            GetAllCustomers = 4,
            CreateOrder = 5,
            CreateLineItem = 6,
            GetAllOrdersByCustomerID = 7,
            GetAllItemsByOrderID = 8,
        }

        public Manager(string connectionString)
        {
            _database = new DatabaseCRUD(connectionString);
            _customerADO = new CustomerADO(_database);
            _orderADO = new OrderADO(_database);
            _lineItemADO = new LineItemADO(_database);
            _productDAO = new ProductDAO(_database);
            _employeeDAO = new EmployeeDAO(_database);
        }

        public void Manage()
        {
            while (true)
            {
                ShowMenu();
                byte input = Convert.ToByte(Console.ReadLine());
                if (input > 0 && input <= Enum.GetValues(typeof(MenuOption)).Length)
                {
                    MenuOption option = (MenuOption)input;
                    switch (option)
                    {
                        case MenuOption.CreateCustomer:
                            CreateCustomer();
                            break;
                        case MenuOption.DeleteCustomer:
                            DeleteCustomer();
                            break;
                        case MenuOption.UpdateCustomer:
                            UpdateCustomer();
                            break;
                        case MenuOption.GetAllCustomers:
                            GetAllCustomers();
                            break;
                        case MenuOption.CreateOrder:
                            CreateOrder();
                            break;
                        case MenuOption.CreateLineItem:
                            CreateLineItem();
                            break;
                        case MenuOption.GetAllOrdersByCustomerID:
                            GetAllOrdersByCustomerID();
                            break;
                        case MenuOption.GetAllItemsByOrderID:
                            GetAllItemsByOrderID();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("==========");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. Delete Customer");
            Console.WriteLine("3. Update Customers");
            Console.WriteLine("4. Get All Customers");
            Console.WriteLine("==========");
            Console.WriteLine("5. Create Order");
            Console.WriteLine("6. Create Line Item");
            Console.WriteLine("7. Get All Orders by Customer ID");
            Console.WriteLine("8. Get All Items by Order ID");
            Console.WriteLine("==========");
            Console.WriteLine("Other numbers to Quit");

        }

        private void ShowDataTable<T>(List<T> list)
        {
            if (list == null)
            {
                return;
            }

            if (list is List<Customer>)
            {
                Console.WriteLine(String.Format($"{"CustomerID",11}{"CustomerName",30}"));

            }
            else if (list is List<Order>)
            {
                Console.WriteLine(String.Format($"{"OrderID",7}{"OrderDate",30}{"CustomerID",15}{"EmployeeID",15}{"Total",15}"));
            }
            else if (list is List<LineItem>)
            {
                Console.WriteLine(String.Format($"{"OrderID",7}{"ProductID",12}{"Quantity",10}{"Price",15}"));
            }
            else
            {
                Console.WriteLine("Not valid list type!");
                return;
            }
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }

        private Customer CreateCustomerObject()
        {
            Console.WriteLine("Customer id: ");
            int customerId = Input.GetInt();
            Console.WriteLine("Customer name: ");
            string name = Console.ReadLine();
            Customer customer = new Customer(customerId, name);
            return customer;
        }

        private void CreateCustomer()
        {
            Customer customer = CreateCustomerObject();

            _customerADO.AddCustomer(customer);
        }

        private void UpdateCustomer()
        {
            Customer customer = CreateCustomerObject();
            if (!_customerADO.IsCustomerExist(customer.CustomerId))
            {
                Console.WriteLine("Customer doesn't exist");
                return;
            }
            _customerADO.UpdateCustomer(customer);
        }

        private void DeleteCustomer()
        {
            Console.Write("Customer ID to remove: ");
            int customerId = Input.GetInt();
            if (!_customerADO.IsCustomerExist(customerId))
            {
                Console.WriteLine("Customer doesn't exist");
                return;
            }

            Console.WriteLine("Deleting customer...");
            _customerADO.DeleteCustomer(customerId);
        }

        private void CreateOrder()
        {
            Console.WriteLine("Creating order...");
            DateTime now = DateTime.Today;
            Console.WriteLine("Customer ID: ");
            int customerId = Input.GetInt();
            if(!_customerADO.IsCustomerExist(customerId))
            {
                Console.WriteLine("Customer doesn't exist");
                return;
            }

            Console.WriteLine("Employee ID: ");
            int employeeId = Input.GetInt();
            if (!_employeeDAO.IsEmployeeExist(employeeId))
            {
                Console.WriteLine("Employee doesn't exist");
                return;
            }

            double total = 0;
            Order order = new Order(0, now, customerId, employeeId, total);
            _orderADO.AddOrder(order);
        }

        private void GetAllCustomers()
        {
            List<Customer> customers = _customerADO.GetAllCustomers();
            ShowDataTable<Customer>(customers);
        }

        private void GetAllOrdersByCustomerID()
        {
            Console.Write("Customer ID: ");
            int customerID = Convert.ToInt32(Console.ReadLine());
            List<Order> items = _orderADO.GetAllOrdersByCustomerId(customerID);
            ShowDataTable<Order>(items);
        }

        private void GetAllItemsByOrderID()
        {
            Console.Write("Order ID: ");
            int orderID = Convert.ToInt32(Console.ReadLine());
            List<LineItem> items = _lineItemADO.GetAllItemsByOrderId(orderID);
            ShowDataTable<LineItem>(items);
        }

        private void CreateLineItem()
        {
            Console.WriteLine("Creating line item...");
            Console.WriteLine("Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());
            if (!_orderADO.IsOrderExist(orderId))
            {
                Console.WriteLine("Order doesn't exist");
                return;
            }

            Console.WriteLine("Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());
            if (!_productDAO.IsProductExist(productId))
            {
                Console.WriteLine("Product doesn't exist");
                return;
            }

            Console.WriteLine("Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            double price = _productDAO.GetProductPrice(productId) * quantity;
            LineItem lineItem = new LineItem(orderId, productId, quantity, price);
            _lineItemADO.AddLineItem(lineItem);
            _orderADO.UpdateOrderTotal(orderId);
        }
    }
}
