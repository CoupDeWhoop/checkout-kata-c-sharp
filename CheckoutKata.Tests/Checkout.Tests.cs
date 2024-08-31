using CheckoutKata;
namespace CheckoutKata.Tests;


public class CheckoutTests
{
    private ICheckout _checkout;
    
    [SetUp]
    public void SetUp()
    {
        _checkout = new Checkout();
    }

    [Test]
    public void GetTotalPrice_ShouldReturnZero_WhenPassedNothing()
    {
        int totalPrice = _checkout.GetTotalPrice();

        Assert.AreEqual(0, totalPrice);
    }

    [TestCase("A", 50)]
    [TestCase("B", 30)]
    [TestCase("C", 20)]
    [TestCase("D", 15)]
    public void GetTotalPrice_ShouldReturnCorrectly_WhenPassedSingleItem(string item, int expected)
    {

        _checkout.Scan(item);

        int totalPrice = _checkout.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, totalPrice);
    }

    [TestCase("AA", 100)]
    [TestCase("CCC", 60)]
    [TestCase("DDDDD", 75)]
    [TestCase("CACAC", 160)]
    [TestCase("ABCD", 115)]

    public void GetTotalPrice_ShouldReturnCorrectly_ForMultiples(string item, int expected)
    {

        _checkout.Scan(item);

        int totalPrice = _checkout.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, totalPrice);
    }
}