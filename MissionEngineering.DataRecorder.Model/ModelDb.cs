using Microsoft.EntityFrameworkCore;
using MissionEngineering.Platform;

public class ModelDb : DbContext
{
    public ModelDb(DbContextOptions<ModelDb> options)
        : base(options) { }

    public DbSet<FlightpathData> FlightpathData => Set<FlightpathData>();
}