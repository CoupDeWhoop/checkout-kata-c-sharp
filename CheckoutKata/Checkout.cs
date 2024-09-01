namespace CheckoutKata;

public class Checkout : ICheckout
{
    private List<char> _basket = new List<char>();
    private DiscountList _discountList;

    public Checkout(DiscountList discountList)
    {
        _discountList = discountList;
    }
    
    public void Scan(string items)
    {
        foreach(char sku in items)
        {
            _basket.Add(sku);
        }
        
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;
        foreach(var sku in _basket)
        {
            totalPrice += GetPriceForSku(sku);
        }

        return totalPrice - _discountList.CalculateDiscount(_basket);
    }

    private int GetPriceForSku(char sku)
    {
        switch (sku)
        {
            case 'A':
                return 50;
            case 'B':
                return 30;
            case 'C':
                return 20;
            case 'D':
                return 15;
            default:
                throw new ArgumentException($"Invalid SKU: {sku}");
        }
    }

}

public class Discount
{
    private char _sku;
    private int _quantityNeeded;
    private int _discount;

    public Discount(char sku, int quantityNeeded, int discount)
    {
        _sku = sku;
        _quantityNeeded = quantityNeeded;
        _discount = discount;
    }

    public int CalculateDiscount(List<char> itemsList)
    {
        var itemCount = itemsList.Where(sku => sku == _sku).Count();
        var itemDiscount = itemCount / _quantityNeeded * _discount;

        return itemDiscount;
    }
}

public class DiscountList : IDiscountList
{
    private readonly List<Discount> _discounts;

    public DiscountList(List<Discount> discounts)
    {
        _discounts = discounts;
    }

    public int CalculateDiscount(List<char> items)
    {
        int totalDiscount = 0;

        foreach (var discount in _discounts)
        {
            totalDiscount += discount.CalculateDiscount(items);
        }

        return totalDiscount;
    }
}
