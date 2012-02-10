using System.Collections.Generic;

namespace MoqSample
{
    /// <summary>
    /// テスト対象のクラス
    /// </summary>
    public class ProductLogic
    {
        public IProductDao ProductDao { get; set; }

        public decimal CalculateTax(int id)
        {
            var domain = ProductDao.GetItemById(id);
            var result = domain.Price * 0.05m;
            return result;
        }

        public void EditProduct(Product item)
        {
            var id = item.Id;
            var domain = ProductDao.GetItemById(id);
            domain.Price *= 1.05m;
            ProductDao.Insert(domain);
        }
    }

    /// <summary>
    /// モックで置き換える予定のDaoのインターフェイス
    /// </summary>
    public interface IProductDao
    {
        void Update(Product product);
        void Insert(Product product);
        Product GetItemById(int id);
        List<Product> GetAllItems();
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
