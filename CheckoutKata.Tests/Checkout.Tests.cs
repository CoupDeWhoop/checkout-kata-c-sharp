using CheckoutKata;
namespace CheckoutKata.Tests;


public class CheckoutTests
{

    [Test]
    public void GetTotalPrice_ShouldReturnZero_WhenPassedNothing()
    {
            // Arrange
            ICheckout checkout = new Checkout();

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(0, totalPrice);
    }
}