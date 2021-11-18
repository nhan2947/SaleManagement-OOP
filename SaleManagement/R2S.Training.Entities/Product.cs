using System;

namespace R2S.Training.Entities
{
    class Product
    {
        int _productId;
        string _productName;
        double _productPrice;

        public Product(int productId, string productName, double productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
        }

        public int ProductId { get => _productId; set => _productId = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public double ProductPrice { get => _productPrice; set => _productPrice = value; }
    }
}