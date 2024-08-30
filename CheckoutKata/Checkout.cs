namespace CheckoutKata;

public class Checkout : ICheckout
{
    private string _basket;
    
    public void Scan(string item)
    {
        _basket = item;
    }
    public int GetTotalPrice()
    {
        if (_basket == "A") return 50;
        if (_basket == "B") return 30;
        if (_basket == "C") return 20;
        if (_basket == "D") return 15;
        return 0;
    }
}
