using System.Drawing;

namespace MissionEngineering.Platform;

public class PlatformSettings
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public string PlatformDescription { get; set; }

    public string PlatformType { get; set; }

    public string PlatformIcon { get; set; }

    public PlatformAffiliation PlatformAffliation { get; set; }

    public Color PlatformColor { get; set; }
}