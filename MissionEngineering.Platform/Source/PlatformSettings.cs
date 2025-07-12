using CsvHelper.Configuration.Attributes;
using System.Drawing;

namespace MissionEngineering.Platform;

public class PlatformSettings
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public string PlatformDescription { get; set; }

    public string PlatformType { get; set; }

    public string PlatformIcon { get; set; }

    public PlatformAffiliation PlatformAffiliation { get; set; }

    public string PlatformColor { get; set; }

    public PlatformSettings()
    {
        PlatformId = 1;
        PlatformName = "Platform 1";
        PlatformDescription = "Platform 1 Description";
        PlatformType = "Aircraft";
        PlatformIcon = "F-35A";
        PlatformAffiliation = PlatformAffiliation.Friendly;
        PlatformColor = "Red";
    }
}