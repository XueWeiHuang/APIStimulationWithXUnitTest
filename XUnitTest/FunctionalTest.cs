using System;
using Xunit;
using WebServer.Models;
using WebServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductRepoUnitTest

{
    public class FunctionalTest
    {

        [Fact]
        public void CreateProductsControllerInstanceTest()
        {
            var controller = new ProductsController();
            Assert.NotNull(controller);
        }

        [Fact]
        public void FakeDataInitializtionTest()
        {
            Assert.NotNull(FakeData.Products);
            Assert.Equal(FakeData.Products.Count, 6);

            foreach (var id in new int[] { 0, 1, 2, 3, 4, 5 })
            {
                Assert.True(FakeData.Products.ContainsKey(id));
            }

            foreach (var key in FakeData.Products.Keys)
            {
                Assert.Equal(FakeData.Products[key].ID, key);
            }
        }

        [Fact]
        public void GetActionTest()
        {
            var controller = new ProductsController();
            Assert.IsType<OkObjectResult>(controller.Get());
            foreach (var key in FakeData.Products.Keys)
            {
                Assert.IsType<OkObjectResult>(controller.Get(key));
            }
        }

        [Fact]
        public void PostActionTest()
        {
            var controller = new ProductsController();
            int oldCount = FakeData.Products.Count;
            var product = new Product { Name = "Test Product", Price = 9.9 };
            Assert.IsType<CreatedResult>(controller.Post(product));
            Assert.Equal(FakeData.Products.Count, oldCount + 1);
        }

        [Fact]
        public void DeleteActionTest()
        {
            var controller = new ProductsController();
            int oldCount = FakeData.Products.Count;
            var maxKey = FakeData.Products.Keys.Max();
            Assert.IsType<OkResult>(controller.Delete(maxKey));
            Assert.Equal(FakeData.Products.Count, oldCount - 1);
        }

        [Fact]
        public void PutActionTest()
        {
            var controller = new ProductsController();
            int oldCount = FakeData.Products.Count;
            var maxKey = FakeData.Products.Keys.Max();
            var product = FakeData.Products[maxKey];
            product.Name = "Changed";
            Assert.IsType<OkResult>(controller.Put(maxKey, product));
            Assert.Equal(FakeData.Products.Count, oldCount);
        }
    }
}
