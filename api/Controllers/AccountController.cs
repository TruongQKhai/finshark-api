using api.Database;
using api.Dtos.Accounts;
using api.Models;
using api.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _context;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var appUser = new AppUser
            {
                UserName = req.Username,
                Email = req.Email,
            };

            var createdUser = await _userManager.CreateAsync(appUser, req.Password);
            if (!createdUser.Succeeded)
            {
                await transaction.RollbackAsync(); // Rolled back if user creational fails
                return StatusCode(500, createdUser.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded)
            {
                await transaction.RollbackAsync(); // Rolled back if role assignment fails
                return StatusCode(500, roleResult.Errors);
            }

            var token = _tokenService.CreateToken(appUser);

            await transaction.CommitAsync(); // commit only if all steps succeed

            return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = token
                    }
                    );

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, ex);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequestDto req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == req.UserName);

        if (user is null)
            return Unauthorized("Invalid username");

        var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password, false);

        if (!result.Succeeded)
            return Unauthorized("Username not found and/or password incorrect");

        return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
    }
}
