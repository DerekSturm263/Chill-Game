public class DisplayYourName : Display<string>
{
    protected override string Value => SaveMethods.Current.familyData.yourName;
}
