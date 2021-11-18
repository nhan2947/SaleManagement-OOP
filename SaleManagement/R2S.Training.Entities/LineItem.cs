namespace R2S.Training.Entities
{
    class LineItem
    {
        private int _orderId;
        private int _productId;
        private int _quantity;
        private double _price;

        public LineItem(int orderId, int productId, int quantity, double price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public int OrderId { get => _orderId; set => _orderId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
        public int Quantity { get => _quantity; set { if (value > 0) _quantity = value; } }
        public double Price { get => _price; set { if (value > 0) _price = value; } }

        public override string? ToString()
        {
            return String.Format($"{OrderId,7}{ProductId,12}{Quantity,10}{Price,15}");
        }
    }
}