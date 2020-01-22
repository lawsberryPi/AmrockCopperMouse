using System;
using Xunit;
using AmrockStudy.Models;
using AmrockStudy.Data;
using Moq;
using AmrockStudy.Controllers;

namespace AmrockTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestAdd()
        {
            var mockRepo = new Mock<ECommerceContext>();
            mockRepo.Setup(repo => repo.Orders)
                .ReturnsAsync();

            var controller = new PurchaseController(mockRepo.Object);
            var returnedValue = controller.Get("123456");
            Assert.True(returnedValue == "success");

        }
    }
}
