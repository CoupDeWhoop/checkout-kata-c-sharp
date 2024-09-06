namespace CheckoutKata;

public class Checkout : ICheckout
{
    private ProductList _catalogue;
    private List<Discount> _discountList;
    private List<char> _basket = new List<char>();
    private IBaggingService _baggingService;


    public Checkout(ProductList catalogue, List<Discount> discountList, IBaggingService? baggingService = null)
    {
        _discountList = discountList;
        _catalogue = catalogue;
        _baggingService = baggingService ?? new BaggingService(0,0);
    }

    public void Scan(string items)
    {
        foreach (char sku in items)
        {
            _basket.Add(sku);
        }

    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;
        foreach (var sku in _basket)
        {
            totalPrice += GetPriceForSku(sku);
        }
        var baggingPrice = _baggingService.GetBaggingPrice(_basket);
        var totalDiscount = GetTotalDiscount();
        return totalPrice - totalDiscount + baggingPrice;
    }

    private int GetPriceForSku(char sku)
    {
        return _catalogue.GetProductPrice(sku);
    }

    public int GetTotalDiscount()
    {
        int totalDiscount = 0;

        foreach (var discount in _discountList)
        {
            totalDiscount += discount.CalculateDiscount(_basket);
        }

        return totalDiscount;
    }
}
