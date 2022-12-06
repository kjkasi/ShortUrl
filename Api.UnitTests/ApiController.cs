using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShortUrl.Api.Controllers;
using ShortUrl.Api.Models;
using ShortUrl.Api.Repositories;
using ShortUrl.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests
{
    public class ApiController
    {
        [Fact]
        public async Task GetAllReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetAllItems()).ReturnsAsync(new List<Item> { new Item { }, new Item { } });
            var logger = Mock.Of<ILogger<ItemController>>();
            var itemController = new ItemController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItems();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Item>>((result as ObjectResult).Value);
        }

        [Fact]
        public async Task GetItemReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync( new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemController>>();
            var itemController = new ItemController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Item>((result as ObjectResult).Value);
        }

        [Fact]
        public async Task GetItemReturn404Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemById(1)).ReturnsAsync(null as Item);
            var logger = Mock.Of<ILogger<ItemController>>();
            var itemController = new ItemController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemById(1);

            // Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
            Assert.Null((result as ObjectResult).Value);
        }

        [Fact]
        public async Task GetItemUrlReturn200Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemByUrl(It.IsAny<string>())).ReturnsAsync(new Item { Id = 1 });
            var logger = Mock.Of<ILogger<ItemController>>();
            var itemController = new ItemController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemByUrl("http://ya.ru");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Item>((result as ObjectResult).Value);
        }

        [Fact]
        public async Task GetItemUrlReturn404Status()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            mockRepository.Setup(s => s.GetItemByUrl(null)).ReturnsAsync(null as Item);
            var logger = Mock.Of<ILogger<ItemController>>();
            var itemController = new ItemController(logger, mockRepository.Object);

            /// Act
            var result = await itemController.GetItemByUrl(null);

            // Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
            Assert.Null((result as ObjectResult).Value);
        }
    }
}
