namespace CheckoutKata;

public class Checkout : ICheckout
{
    private ProductList _catalogue;
    private List<Discount> _discountList;
    private List<char> _basket = new List<char>();


    public Checkout(ProductList catalogue, List<Discount> discountList)
    {
        _discountList = discountList;
        _catalogue = catalogue;
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

        return totalPrice - GetTotalDiscount();
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

public class ProductList
{
    public List<Product> catalogue = new List<Product>();

    public ProductList AddProduct(char sku, int price)
    {
        Product product = new Product(sku, price);
        catalogue.Add(product);
        return this;
    }

    public int GetProductPrice(char sku)
    {
        var product = catalogue.Where(product => product.Sku == sku).FirstOrDefault();
        if (product == null)
        {
            throw new ArgumentException($"Invalid SKU: {sku}");
        }
        return product.Price;
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
