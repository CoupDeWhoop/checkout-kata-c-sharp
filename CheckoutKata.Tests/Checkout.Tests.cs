namespace CheckoutKata.Tests;


public class CheckoutTests
{
    private ICheckout _checkout;

    [SetUp]
    public void SetUp()
    {
        var catalogue = new ProductList();
        catalogue
            .AddProduct('A', 50)
            .AddProduct('B', 30)
            .AddProduct('C', 20)
            .AddProduct('D', 15);
        var discountList = new List<Discount>
        {
            new Discount('A', 3, 20),
            new Discount('B', 2, 15)
        };

        _checkout = new Checkout(catalogue, discountList);
    }

    [Test]
    public void GetTotalPrice_ShouldReturnZero_WhenPassedNothing()
    {
        int totalPrice = _checkout.GetTotalPrice();

        Assert.That(totalPrice, Is.EqualTo(0));
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
        Assert.That(totalPrice, Is.EqualTo(expected));
    }

    [TestCase("AA", 100)]
    [TestCase("CCC", 60)]
    [TestCase("DDDDD", 75)]
    [TestCase("CACAC", 160)]
    [TestCase("ABCD", 115)]

    public void GetTotalPrice_ShouldReturnCorrectly_ForMultiples(string items, int expected)
    {

        _checkout.Scan(items);

        int totalPrice = _checkout.GetTotalPrice();

        // Assert
        Assert.That(totalPrice, Is.EqualTo(expected));
    }

    [TestCase("A£CD", 115)]
    public void GetTotalPrice_ShouldThrowException_ForUnknownSku(string items, int expected)
    {

        _checkout.Scan(items);

        var expectedMessage = "Invalid SKU: £";

        // Assert
        var actual = Assert.Throws<ArgumentException>(() => _checkout.GetTotalPrice());
        Assert.That(actual.Message, Is.EqualTo(expectedMessage));
    }

    [TestCase("AAA", 130)]
    [TestCase("AAAB", 160)]
    [TestCase("BB", 45)]
    [TestCase("AAABB", 175)]
    [TestCase("BBD", 60)]
    public void GetTotalPrice_ShouldApplyDiscountsCorrectly(string items, int expected)
    {
        _checkout.Scan(items);

        var totalPrice = _checkout.GetTotalPrice();

        Assert.That(totalPrice, Is.EqualTo(expected));
    }
}

public class CheckoutWithBagTests
{
    private ICheckout _checkout;

    [SetUp]
    public void SetUp()
    {
        var catalogue = new ProductList();
        catalogue
            .AddProduct('A', 50)
            .AddProduct('B', 30)
            .AddProduct('C', 20)
            .AddProduct('D', 15);
        var discountList = new List<Discount>
        {
            new Discount('A', 3, 20),
            new Discount('B', 2, 15)
        };
        var bagPrice = 5;
        _checkout = new Checkout(catalogue, discountList, bagPrice);
    }

    [Test]
    public void GetTotalPrice_ShouldApplyBaggingFeeCorrectly_WhenNoItems()
    {
        var totalPrice = _checkout.GetTotalPrice();

        Assert.That(totalPrice, Is.EqualTo(0));
    }

    [TestCase("AAA", 135)]
    [TestCase("AAAB", 165)]
    [TestCase("BB", 50)]
    [TestCase("AAABB", 180)]
    [TestCase("BBBBBB", 145)]
    public void GetTotalPrice_ShouldApplyBaggingFeeCorrectly(string items, int expected)
    {
        _checkout.Scan(items);
        var totalPrice = _checkout.GetTotalPrice();

        Assert.That(totalPrice, Is.EqualTo(expected));
    }



}