using MediAidServer.Models;
using MediAidServer.Utils;

namespace MediAidServer.Data.Seeders;

public static class AdminSeeder
{
    private const string DefaultPassword = "Abcd1234";

    public static void Seed(ApplicationDbContext context)
    {
        // Check if admins already exist
        if (context.Admins.Any())
        {
            return;
        }

        var (hash, salt) = PasswordHasher.HashPassword(DefaultPassword);

        var admins = new List<Admin>
        {
            new Admin
            {
                Email = "admin@mediaid.com",
                Name = "John Doe",
                Hash = hash,
                Salt = salt,
                CreatedOn = DateTime.UtcNow
            }
        };

        context.Admins.AddRange(admins);
        context.SaveChanges();
    }
}

