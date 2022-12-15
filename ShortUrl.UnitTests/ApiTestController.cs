using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShortUrl.Api.Controllers;
using ShortUrl.Api.Models;
using ShortUrl.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShortUrl.UnitTests
{
    public class ApiTestController
    {
        [Fact]
        public async Task GetAllReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetAllItems()).ReturnsAsync(new List<Item> { new Item { }, new Item { } });
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItems();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Item>>(((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task GetItemReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync(new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Item>(((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task GetItemReturn404Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync(null as Item);
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemById(1);

            // Assert
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public async Task GetItemUrlReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemByUrl(It.IsAny<string>())).ReturnsAsync(new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemByUrl("http://ya.ru");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Item>(((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task GetItemUrlReturn404Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemByUrl(null)).ReturnsAsync(null as Item);
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemByUrl(null);

            // Assert
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public async Task AddItemReturn201Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.CreateItem(It.IsAny<Item>())).ReturnsAsync(true);
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.AddItem(new Item { OriginalUrl = "http://ya.ru" });

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<Item>(((CreatedAtActionResult)result).Value);
        }

        [Fact]
        public async Task DeleteItemReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.DeleteItem(It.IsAny<Item>())).ReturnsAsync(true);
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync(new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.DeleteItem(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task UpdateItemReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.UpdateItem(It.IsAny<Item>())).ReturnsAsync(true);
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync(new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemApiController>>();
            var itemController = new ItemApiController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.UpdateItem(1, new Item { Id = 1 });

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(((OkObjectResult)result).Value);
        }
    }
}
