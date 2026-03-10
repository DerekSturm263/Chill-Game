public class DisplayPremiumCurrency : Display<string>
{
    protected override string Value => SaveMethods.Current.premium_currency_temp.ToString();
}
