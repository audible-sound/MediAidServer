using MediAidServer.Models;
using MediAidServer.Utils;

namespace MediAidServer.Data.Seeders;

public static class InstructorSeeder
{
    private const string DefaultPassword = "Abcd1234";

    public static void Seed(ApplicationDbContext context)
    {
        if (context.Instructors.Any())
        {
            return;
        }

        var instructors = new List<Instructor>();

        var (hash1, salt1) = PasswordHasher.HashPassword(DefaultPassword);
        instructors.Add(new Instructor
        {
            Email = "instructor1@mediaid.com",
            Name = "John Instructor",
            Hash = hash1,
            Salt = salt1,
            CreatedOn = DateTime.UtcNow
        });

        var (hash2, salt2) = PasswordHasher.HashPassword(DefaultPassword);
        instructors.Add(new Instructor
        {
            Email = "instructor2@mediaid.com",
            Name = "Jane Instructor",
            Hash = hash2,
            Salt = salt2,
            CreatedOn = DateTime.UtcNow
        });

        context.Instructors.AddRange(instructors);
        context.SaveChanges();
    }
}

