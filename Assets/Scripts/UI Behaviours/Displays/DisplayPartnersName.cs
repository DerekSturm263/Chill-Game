public class DisplayPartnersName : Display<string>
{
    protected override string Value => SaveMethods.Current.familyData.partnersName;
}
