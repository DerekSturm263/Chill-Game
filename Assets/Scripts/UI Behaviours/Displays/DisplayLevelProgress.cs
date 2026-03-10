using System;

public class DisplayLevelProgress : Display<float>
{
    protected override float Value => SaveMethods.Current.level - (float)Math.Truncate(SaveMethods.Current.level);
}
