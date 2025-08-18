using AppointmentBooking.Business.Contract;
using AppointmentBooking.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration,
                              IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var appUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Assign default role
            if (!await _roleManager.RoleExistsAsync("Applicant"))
                await _roleManager.CreateAsync(new IdentityRole("Applicant"));

            await _userManager.AddToRoleAsync(appUser, "Applicant");

            var user = new UserMaster
            {
                Id = Guid.Parse(appUser.Id),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedAt = DateTime.UtcNow,
            };

            var response = await _authService.Create(user);

            return Ok(response);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var loginResponse = await _authService.GetUserProfile(Guid.Parse(user.Id));
            loginResponse.Result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            loginResponse.Result.Expiration = token.ValidTo;

            return Ok(loginResponse);
        }

        // POST: api/auth/assign-role (Admin only)
        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var response = new ApiGenericResponseModel<bool>();
            response.IsSuccess=true;
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return NotFound("User not found");

                if (!await _roleManager.RoleExistsAsync(dto.Role))
                    await _roleManager.CreateAsync(new IdentityRole(dto.Role));

                string[] roles = { "Applicant", "Staff", "Admin" };
                foreach (var role in roles)
                {
                    if (role.ToLower() != dto.Role.ToLower())
                        await _userManager.RemoveFromRoleAsync(user, role);
                }

                await _userManager.AddToRoleAsync(user, dto.Role);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
                
            }
            return Ok(response);
        }

        //// GET: /api/auth/users
        //[HttpGet("users")]
        //public async Task<IActionResult> GetAllUser()
        //{
        //    return Ok(await _authService.GetAllUser());
        //}
    }
}
