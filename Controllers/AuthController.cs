using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediAidServer.Data;
using MediAidServer.Models.DTOs;
using MediAidServer.Utils;

namespace MediAidServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ApplicationDbContext context, JwtService jwtService, ILogger<AuthController> logger)
    {
        _context = context;
        _jwtService = jwtService;
        _logger = logger;
    }

    [HttpPost("admin/login")]
    public async Task<ActionResult<LoginResponse>> AdminLogin([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find admin by email
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Email == request.Email);

        if (admin == null)
        {
            _logger.LogWarning("Login attempt with invalid email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Verify password
        if (!PasswordHasher.VerifyPassword(request.Password, admin.Hash, admin.Salt))
        {
            _logger.LogWarning("Login attempt with invalid password for email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Update last activity timestamp
        admin.LastActivityAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtService.GenerateToken(admin.Id, admin.Email, admin.Name, "Admin");

        var response = new LoginResponse
        {
            AccessToken = token,
            Email = admin.Email,
            Name = admin.Name,
            LastActivityAt = admin.LastActivityAt.Value
        };

        _logger.LogInformation("Admin login successful for email: {Email}", request.Email);
        return Ok(response);
    }

    [HttpPost("instructor/login")]
    public async Task<ActionResult<LoginResponse>> InstructorLogin([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find instructor by email
        var instructor = await _context.Instructors
            .FirstOrDefaultAsync(i => i.Email == request.Email);

        if (instructor == null)
        {
            _logger.LogWarning("Login attempt with invalid email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Verify password
        if (!PasswordHasher.VerifyPassword(request.Password, instructor.Hash, instructor.Salt))
        {
            _logger.LogWarning("Login attempt with invalid password for email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Update last activity timestamp
        instructor.LastActivityAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtService.GenerateToken(instructor.Id, instructor.Email, instructor.Name, "Instructor");

        var response = new LoginResponse
        {
            AccessToken = token,
            Email = instructor.Email,
            Name = instructor.Name,
            LastActivityAt = instructor.LastActivityAt.Value
        };

        _logger.LogInformation("Instructor login successful for email: {Email}", request.Email);
        return Ok(response);
    }

    [HttpPost("learner/login")]
    public async Task<ActionResult<LoginResponse>> LearnerLogin([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find learner by email
        var learner = await _context.Learners
            .FirstOrDefaultAsync(l => l.Email == request.Email);

        if (learner == null)
        {
            _logger.LogWarning("Login attempt with invalid email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Verify password
        if (!PasswordHasher.VerifyPassword(request.Password, learner.Hash, learner.Salt))
        {
            _logger.LogWarning("Login attempt with invalid password for email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Update last activity timestamp
        learner.LastActivityAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtService.GenerateToken(learner.Id, learner.Email, learner.Name, "Learner");

        var response = new LoginResponse
        {
            AccessToken = token,
            Email = learner.Email,
            Name = learner.Name,
            LastActivityAt = learner.LastActivityAt.Value
        };

        _logger.LogInformation("Learner login successful for email: {Email}", request.Email);
        return Ok(response);
    }
}

