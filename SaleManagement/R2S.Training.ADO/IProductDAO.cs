
namespace R2S.Training.ADO
{
    public interface IProductDAO
    {
        bool IsProductExist(int productId);
        double GetProductPrice(int productId);
    }
}
