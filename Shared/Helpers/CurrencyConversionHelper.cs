namespace Shared.Helpers;

public static class CurrencyConversionHelper
{
    public static int ToEuro(this decimal value)
    {
        return (int)(value * 100);
    }
}
