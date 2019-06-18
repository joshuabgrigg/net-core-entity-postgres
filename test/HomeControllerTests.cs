using NUnit.Framework;

namespace Tests
{
    public class HomeControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IndexTest()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("hello world", result);
        }
    }
}