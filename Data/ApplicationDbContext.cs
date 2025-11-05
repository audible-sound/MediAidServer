using Microsoft.EntityFrameworkCore;
using MediAidServer.Models;

namespace MediAidServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Learner> Learners { get; set; }
}

