using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediAidServer.Models;

public class Admin
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(255, ErrorMessage = "Email must not exceed 255 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password hash is required")]
    public string Hash { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password salt is required")]
    public string Salt { get; set; } = string.Empty;
    
    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
    
    [Column("last_activity_at")]
    public DateTime? LastActivityAt { get; set; }
}

