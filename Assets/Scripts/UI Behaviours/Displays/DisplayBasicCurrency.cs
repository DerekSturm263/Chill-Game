public class DisplayBasicCurrency : Display<string>
{
    protected override string Value => SaveMethods.Current.basic_currency_temp.ToString();
}
