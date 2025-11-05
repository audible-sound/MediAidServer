using System.Security.Cryptography;
using System.Text;

namespace MediAidServer.Utils;

public static class PasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000; // PBKDF2 iterations

    private static string GetSecretKey()
    {
        var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
        if (string.IsNullOrEmpty(secretKey))
            throw new InvalidOperationException("SECRET_KEY environment variable is not set");
        return secretKey;
    }

    private static string CombinePasswordWithSecret(string password)
    {
        var secretKey = GetSecretKey();
        return $"{password}{secretKey}";
    }

    public static (string Hash, string Salt) HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));

        // Combine password with SECRET_KEY (pepper)
        var passwordWithSecret = CombinePasswordWithSecret(password);

        // Generate a random salt
        var saltBytes = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        // Hash the password with PBKDF2
        var hashBytes = new byte[HashSize];
        using (var pbkdf2 = new Rfc2898DeriveBytes(
            passwordWithSecret,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256))
        {
            hashBytes = pbkdf2.GetBytes(HashSize);
        }

        // Convert to base64 strings for storage
        var hash = Convert.ToBase64String(hashBytes);
        var salt = Convert.ToBase64String(saltBytes);

        return (hash, salt);
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
            return false;

        try
        {
            // Combine password with SECRET_KEY
            var passwordWithSecret = CombinePasswordWithSecret(password);

            var saltBytes = Convert.FromBase64String(storedSalt);
            var hashBytes = Convert.FromBase64String(storedHash);

            // Hash the provided password with the stored salt
            using var pbkdf2 = new Rfc2898DeriveBytes(
                passwordWithSecret,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256);

            var computedHash = pbkdf2.GetBytes(HashSize);

            // Compare the hashes
            return CryptographicOperations.FixedTimeEquals(hashBytes, computedHash);
        }
        catch
        {
            return false;
        }
    }
}

