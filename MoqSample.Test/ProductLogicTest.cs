using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MoqSample.Test
{
    [TestClass]
    public class ProductLogicTest
    {
        [TestMethod]
        public void CalculateTaxTest()
        {
            //Arrange
            var dao = new Mock<IProductDao>();
            dao.Setup(m => m.GetItemById(1))
                .Returns(new Product
                {
                    Id = 1,
                    Name = "Book",
                    Price = 300
                });
            var target = new ProductLogic { ProductDao = dao.Object };

            //Act
            var result = target.CalculateTax(1);

            //Assert
            Assert.AreEqual(15, result);

        }

        [TestMethod]
        public void EditProductTest()
        {
            //Arrange
            //DBアクセス部分をモックで作成
            var dao = new Mock<IProductDao>();
            dao.Setup(m => m.GetItemById(10))
                .Returns(new Product
                                {
                                    Id = 10,
                                    Name = "Book",
                                    Price = 300
                                });
            dao.Setup(m => m.Insert(It.IsAny<Product>()))
                .Callback<Product>(p =>
                                        {
                                            //コールバックにてAssertを実行
                                            Assert.AreEqual(10, p.Id);
                                            Assert.AreEqual("Book", p.Name);
                                            Assert.AreEqual(315, p.Price);
                                        });
            var target = new ProductLogic { ProductDao = dao.Object };

            //Act
            target.EditProduct(new Product { Id = 10 });

            //Assert(一部)
            dao.Verify(m => m.Insert(It.IsAny<Product>()), Times.Once());
            dao.Verify(m => m.Update(It.IsAny<Product>()), Times.Never());
        }

    }
}
