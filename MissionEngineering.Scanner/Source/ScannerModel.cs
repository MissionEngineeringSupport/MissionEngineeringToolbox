namespace MissionEngineering.Scanner;

/// <summary>
/// Models a raster scan pattern for a scanner, supporting azimuth and elevation scanning with configurable parameters.
/// </summary>
public class ScannerModel
{
    /// <summary>
    /// The total width of the scan in azimuth (degrees).
    /// </summary>
    public double AzimuthWidthDeg { get; set; }

    /// <summary>
    /// The center of the scan in azimuth (degrees).
    /// </summary>
    public double AzimuthCenterDeg { get; set; }

    /// <summary>
    /// The center of the scan in elevation (degrees).
    /// </summary>
    public double ElevationCenterDeg { get; set; }

    /// <summary>
    /// The number of elevation bars in the scan.
    /// </summary>
    public int NumberOfBars { get; set; }

    /// <summary>
    /// The current scan angle in azimuth (degrees).
    /// </summary>
    public double ScanAngleDeg { get; private set; }

    /// <summary>
    /// The current scan rate in azimuth (degrees/second).
    /// </summary>
    public double ScanRateDegPerSec { get; private set; }

    /// <summary>
    /// The current scan acceleration in azimuth (degrees/second^2).
    /// </summary>
    public double ScanAccelerationDegPerSec2 { get; private set; }

    /// <summary>
    /// The current bar being scanned (0-based).
    /// </summary>
    public int CurrentBar { get; private set; }

    /// <summary>
    /// The direction of scan: +1 for right, -1 for left.
    /// </summary>
    private int scanDirection = 1;

    /// <summary>
    /// The time since the start of the current bar (seconds).
    /// </summary>
    private double barTime = 0.0;

    /// <summary>
    /// The total time to scan one bar (seconds).
    /// </summary>
    public double BarScanTimeSec { get; set; } = 1.0;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScannerModel"/> class.
    /// </summary>
    public ScannerModel(
        double azimuthWidthDeg,
        double azimuthCenterDeg,
        double elevationCenterDeg,
        int numberOfBars)
    {
        AzimuthWidthDeg = azimuthWidthDeg;
        AzimuthCenterDeg = azimuthCenterDeg;
        ElevationCenterDeg = elevationCenterDeg;
        NumberOfBars = numberOfBars > 0 ? numberOfBars : 1;
        ScanAngleDeg = AzimuthCenterDeg - AzimuthWidthDeg / 2.0;
        ScanRateDegPerSec = 0.0;
        ScanAccelerationDegPerSec2 = 0.0;
        CurrentBar = 0;
    }

    /// <summary>
    /// Updates the scan state based on elapsed time.
    /// </summary>
    /// <param name="deltaTimeSec">Elapsed time in seconds.</param>
    public void Update(double deltaTimeSec)
    {
        if (NumberOfBars < 1) return;

        // Calculate scan endpoints
        double leftLimit = AzimuthCenterDeg - AzimuthWidthDeg / 2.0;
        double rightLimit = AzimuthCenterDeg + AzimuthWidthDeg / 2.0;

        // Simple constant rate scan for demonstration
        double scanSpeed = AzimuthWidthDeg / BarScanTimeSec;
        ScanRateDegPerSec = scanDirection * scanSpeed;
        ScanAccelerationDegPerSec2 = 0.0; // No acceleration in this simple model

        ScanAngleDeg += ScanRateDegPerSec * deltaTimeSec;
        barTime += deltaTimeSec;

        // Check for reaching scan limits
        if ((scanDirection > 0 && ScanAngleDeg >= rightLimit) ||
            (scanDirection < 0 && ScanAngleDeg <= leftLimit))
        {
            // Clamp to limit
            ScanAngleDeg = scanDirection > 0 ? rightLimit : leftLimit;
            scanDirection *= -1; // Reverse direction

            // Move to next bar
            CurrentBar++;
            if (CurrentBar >= NumberOfBars)
            {
                CurrentBar = 0;
            }
            // Reset scan angle for new bar
            ScanAngleDeg = scanDirection > 0 ? leftLimit : rightLimit;
            barTime = 0.0;
        }
    }

    /// <summary>
    /// Gets the current elevation angle for the current bar.
    /// </summary>
    public double GetCurrentElevationDeg()
    {
        if (NumberOfBars == 1) return ElevationCenterDeg;
        // Distribute bars symmetrically around the center
        double barSpacing = 1.0; // You can parameterize this as needed
        double firstBar = ElevationCenterDeg - (NumberOfBars - 1) * barSpacing / 2.0;
        return firstBar + CurrentBar * barSpacing;
    }
}