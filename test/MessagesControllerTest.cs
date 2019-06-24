using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class MessagesControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetTestAsync()
        {
            // Arrange
            var message1 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello World"
            };
            var message2 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello again"
            };
            var mockResults = new List<Message>()
            {
                message1,
                message1
            };
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.FindAll()).ReturnsAsync(mockResults);
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Get()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult), result.GetType(), "should be OkObjectResult");
            var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.AreEqual(typeof(List<Message>), okResult.Value.GetType(), "should be List<Message");
            var messages = okResult.Value as IEnumerable<Message>;
            Assert.AreEqual(mockResults, messages, "messages don't match");
        }

        [Test]
        public async Task GetByIDTestAsync()
        {
            // Arrange
            var message1 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello World"
            };
            var message2 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello again"
            };
            var mockResult = message1;
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Find(message1.MessageID)).ReturnsAsync(mockResult);
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Get(message1.MessageID)).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult), result.GetType(), "should be OkObjectResult");
            var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.AreEqual(typeof(Message), okResult.Value.GetType(), "should be Message");
            var message = okResult.Value as Message;
            Assert.AreEqual(mockResult, message, "message doesn't match");
        }

        [Test]
        public async Task GetByIDNotFoundTestAsync()
        {
            // Arrange
            var message1 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello World"
            };
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Find(message1.MessageID));
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Get(message1.MessageID)).Result;

            // Assert
            mock.Verify(m => m.Find(message1.MessageID));
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult), result.GetType(), "should be NotFoundResult");
            var notFoundResult = result as Microsoft.AspNetCore.Mvc.NotFoundResult;
            Assert.AreEqual(404, notFoundResult.StatusCode, "status code should be 404");
        }

        [Test]
        public async Task AddMessageTestAsync()
        {
            // Arrange
            var messageText = "message";
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Add(It.IsAny<Message>())).ReturnsAsync(new Message() { Text = messageText });
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Post(messageText)).Result;

            // Assert
            mock.Verify(m => m.Add(It.Is<Message>(i => i.Text == messageText)));
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult), result.GetType(), "Result should be OkObjectResult");
            var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.AreEqual(typeof(Message), okResult.Value.GetType(), "Result.Value should be a Message");
            var message = okResult.Value as Message;
            Assert.AreEqual(messageText, message.Text, string.Format("Result.Value.Message.Text should be {0} but was {1}.", messageText, message.Text));
        }

        [Test]
        public async Task DeleteMessageTestAsync()
        {
            // Arrange
            var message1 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello World"
            };
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Find(message1.MessageID)).ReturnsAsync(message1);
            mock.Setup(m => m.Remove(message1));
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Delete(message1.MessageID));

            // Assert
            mock.Verify(m => m.Remove(It.Is<Message>(d => d == message1)));
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.NoContentResult), result.GetType());
            var noContentResult = result as Microsoft.AspNetCore.Mvc.NoContentResult;
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteMessageNotFoundTestAsync()
        {
            // Arrange
            var messageID = Guid.NewGuid();
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Remove(messageID));
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Delete(messageID));

            // Assert
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult), result.GetType());
        }

        [Test]
        public async Task PutMessageTestAsync()
        {
            // Arrange
            var message1 = new Message()
            {
                MessageID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedTime = new DateTime(2019, 06, 22),
                Text = "Hello World"
            };
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Find(message1.MessageID)).ReturnsAsync(message1);
            var updatedText = "test";
            var updatedMockMessage = new Message() { MessageID = message1.MessageID, Text = updatedText, CreatedTime = message1.CreatedTime };
            mock.Setup(m => m.Update(It.Is<Message>(um => um.MessageID == message1.MessageID && um.Text == updatedText))).ReturnsAsync(updatedMockMessage);
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Put(message1.MessageID, updatedText)).Result;

            // Assert
            mock.Verify(m => m.Update(It.Is<Message>(p => p.Text == updatedText)));
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult), result.GetType());
            var OkObjectResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.AreEqual(typeof(Message), OkObjectResult.Value.GetType());
            var messageResult = OkObjectResult.Value as Message;
            Assert.AreEqual(updatedMockMessage.MessageID, messageResult.MessageID);
            Assert.AreEqual(updatedMockMessage.Text, messageResult.Text);
            Assert.AreEqual(updatedMockMessage.CreatedTime, messageResult.CreatedTime);
        }

        [Test]
        public async Task PutMessageNotFoundTestAsync()
        {
            // Arrange
            var messageID = Guid.NewGuid();
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.Find(messageID));
            var controller = new MessagesController(mock.Object);

            // Act
            var result = (await controller.Put(messageID, "test")).Result;

            Console.WriteLine(result);

            // Assert
            mock.Verify(m => m.Find(messageID));
            Assert.AreEqual(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult), result.GetType());
            var notFoundResult = result as Microsoft.AspNetCore.Mvc.NotFoundResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);

        }
    }
}