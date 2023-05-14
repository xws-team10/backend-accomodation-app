using account_service.Model;
using account_service.Model.DTO;
using account_service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace account_service.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager,
            TokenService tokenService)
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;   
        }

        [HttpPost]
        [Route("roles/add")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var appRole = new ApplicationRole { Name = request.Role };
            var createRole = await _roleManager.CreateAsync(appRole);

            return Ok(new { message = "role created successfully" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                UserRole = user.UserRole
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email, Address = registerDto.Address, UserRole = registerDto.UserRole, Name = registerDto.Name, Surname = registerDto.Surname };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, registerDto.UserRole);

            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                UserRole = user.UserRole,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Id = user.Id,
                Username = user.UserName,
            };
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);


            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return NoContent();
        }

        //Maybe adding username in path (update/{username}) is required?
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            user.UserName = updateUserDto.Username;
            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;
            user.Address = updateUserDto.Address;   
            user.Email = updateUserDto.Email;
            user.UserRole = updateUserDto.Role;

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return ValidationProblem();
                }
            }

            var resultUpdate = await _userManager.UpdateAsync(user);

            if (!resultUpdate.Succeeded)
            {
                foreach (var error in resultUpdate.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return Ok(new { message = "user data updated successfully" });
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<UserDto> Authenticate(LoginDto loginDto)
        {
            User user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("UserRole", "member")
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is a secret key and need to be at least 12 characters")), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserDto
            {

                Email = user.Email,
                Token = tokenHandler.WriteToken(token),
            };
        }

    }
}
