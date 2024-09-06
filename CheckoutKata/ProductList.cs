namespace CheckoutKata;

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
