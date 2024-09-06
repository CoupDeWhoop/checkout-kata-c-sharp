namespace CheckoutKata;

public class Product
{
    public char Sku { get; }
    public int Price { get; }

    public Product(char sku, int price)
    {
        Price = price;
        Sku = sku;
    }
}
