namespace CheckoutKata;

public class BaggingService : IBaggingService
{
    private readonly int _bagPrice;
    private readonly int _itemsPerBag;

    public BaggingService(int bagPrice, int itemsPerBag)
    {
        _bagPrice = bagPrice;
        _itemsPerBag = itemsPerBag;
    }

    public int GetBaggingPrice(List<char> basket)
    {
        if (_bagPrice <= 0 || _itemsPerBag <= 0) return 0;
        var bagCount = (basket.Count + _itemsPerBag - 1) / _itemsPerBag;
        return bagCount * _bagPrice;
    }

}
