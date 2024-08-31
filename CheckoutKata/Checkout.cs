namespace CheckoutKata;

public class Checkout : ICheckout
{
    private List<char> _basket = new List<char>();
    
    public void Scan(string item)
    {
        foreach(char sku in item)
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

        return totalPrice;
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
