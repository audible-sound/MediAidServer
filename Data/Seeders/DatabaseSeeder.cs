namespace MediAidServer.Data.Seeders;

public static class DatabaseSeeder
{
    /// <summary>
    /// Seeds all database tables with initial data
    /// </summary>
    public static void Seed(ApplicationDbContext context)
    {
        AdminSeeder.Seed(context);
        InstructorSeeder.Seed(context);
        LearnerSeeder.Seed(context);
    }
}

