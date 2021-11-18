namespace R2S.Training.Entities
{
    class Customer
    {
        int _customerId;
        string _customerName;

        public Customer(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }

        public int CustomerId { get => _customerId; set => _customerId = value; }
        public string CustomerName { get => _customerName; set => _customerName = value; }

        public override string? ToString()
        {
            return String.Format($"{CustomerId,11}{CustomerName,30}");
        }
    }
}