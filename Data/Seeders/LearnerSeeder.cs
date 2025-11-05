using MediAidServer.Models;
using MediAidServer.Utils;

namespace MediAidServer.Data.Seeders;

public static class LearnerSeeder
{
    private const string DefaultPassword = "Abcd1234";

    public static void Seed(ApplicationDbContext context)
    {
        // Check if learners already exist
        if (context.Learners.Any())
        {
            return;
        }

        var learners = new List<Learner>();

        // Generate unique hash and salt for each learner
        var (hash1, salt1) = PasswordHasher.HashPassword(DefaultPassword);
        learners.Add(new Learner
        {
            Email = "learner1@mediaid.com",
            Name = "Alice Learner",
            Hash = hash1,
            Salt = salt1,
            CreatedOn = DateTime.UtcNow
        });

        var (hash2, salt2) = PasswordHasher.HashPassword(DefaultPassword);
        learners.Add(new Learner
        {
            Email = "learner2@mediaid.com",
            Name = "Bob Learner",
            Hash = hash2,
            Salt = salt2,
            CreatedOn = DateTime.UtcNow
        });

        var (hash3, salt3) = PasswordHasher.HashPassword(DefaultPassword);
        learners.Add(new Learner
        {
            Email = "learner3@mediaid.com",
            Name = "Charlie Learner",
            Hash = hash3,
            Salt = salt3,
            CreatedOn = DateTime.UtcNow
        });

        context.Learners.AddRange(learners);
        context.SaveChanges();
    }
}

