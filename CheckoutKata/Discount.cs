namespace CheckoutKata;

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
