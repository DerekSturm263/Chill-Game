public class DisplayName : Display<string>
{
    protected override string Value => SaveMethods.Current.username;
}
