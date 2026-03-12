public class DisplayPremiumCurrency : Display<string>
{
    protected override string Value => SaveMethods.Current.premiumCurrency.ToString();
}
