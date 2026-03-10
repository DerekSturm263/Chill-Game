using System;
using UnityEngine;

public class DisplayLevel : Display<string>
{
    [SerializeField] private string _format;

    protected override string Value => string.Format(_format, Math.Truncate(SaveMethods.Current.level));
}
