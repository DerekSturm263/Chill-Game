public class DisplayBasicCurrency : Display<string>
{
    protected override string Value => SaveMethods.Current.basicCurrency.ToString();
}
