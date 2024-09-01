namespace CheckoutKata;

public class Checkout : ICheckout
{
    private List<char> _basket = new List<char>();
    
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

        return totalPrice - CalculateDiscount();
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

    private int CalculateDiscount()
    {
        var a_count = _basket.Where(sku => sku == 'A').Count();
        var a_discount = a_count / 3 * 20;
        var b_count = _basket.Where(sku => sku == 'B').Count();
        var b_discount = b_count / 2 * 15;

        return a_discount + b_discount;
    }


}
