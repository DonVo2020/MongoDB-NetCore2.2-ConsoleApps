namespace DonVo.MongoDb.Console2018.Transactions.ViewModels
{
    public class OrderedProduct
    {
        public OrderedProduct(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product { get; }
        public int Quantity { get; }
    }
}
