namespace CheckoutKata;

public class Checkout : ICheckout
{
    private List<char> _basket = new List<char>();
    private List<Discount> _discountList;

    public Checkout(List<Discount> discountList)
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

        return totalPrice - GetTotalDiscount();
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
