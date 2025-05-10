using AspNetCoreIdentityApp.Web.Extensions;
using AutoMapper;
using Dto.ApplicationUsers;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginAsync(request);
            if (await result.Success)
            {
                return Ok("User signed in successfully.");
            }

            return BadRequest(await result.Message);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(request);
            if (await result.Success)
            {
                return Ok("User registered successfully.");
            }

            return BadRequest(await result.Messages);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _userService.LogOutAsync();
            return Ok("User signed out successfully.");
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto request)
        {
            var result = await _userService.ChangePasswordAsync(request);
            if (await result.Success)
            {
                return Ok("Password changed successfully.");
            }

            return BadRequest(await result.Messages);
        }

        [HttpPost("edituser")]
        public async Task<IActionResult> EditUser([FromBody] EditUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.EditUserInformationAsync(request);
            if (await result.Success)
            {
                return Ok("User information updated successfully.");
            }

            return BadRequest(await result.Messages);
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordDto request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                return NotFound("User with the provided email does not exist.");
            }

            var result = await _userService.SendPasswordResetLinkAsync(request);
            if (await result.Success)
            {
                return Ok("Password reset link sent successfully.");
            }

            return BadRequest(await result.Messages);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto request, [FromQuery] Guid userId, [FromQuery] string token)
        {
            var result = await _userService.ResetUserPasswordAsync(request, userId, token);
            if (await result.Success)
            {
                return Ok("Password reset successfully.");
            }

            return BadRequest(await result.Messages);
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            return Forbid("You do not have access to this resource.");
        }
    }
}