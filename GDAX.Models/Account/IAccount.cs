namespace GDAX.Models.Account
{
    public interface IAccount
    {

        decimal CurrencyBalance { get; set; }

        decimal AssetBalance { get; set; }

    }
}
