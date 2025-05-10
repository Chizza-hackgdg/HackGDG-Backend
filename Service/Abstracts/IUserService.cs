
using Core.Utilities.Results;
using Dto.ApplicationUsers;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Service.Abstracts
{
    public interface IUserService
    {
        public Task<IAsyncResult> LoginAsync(LoginUserDto userDto);

        public Task<IAsyncResult> RegisterAsync(RegisterUserDto userDto);

        public Task<IAsyncResult> LogOutAsync();

        public Task<IAsyncResult> ChangePasswordAsync(ChangeUserPasswordDto userDto);
        public Task<IAsyncDataResult<List<ListUserDto>>> GetAllUsersAsync();

        public Task<IAsyncDataResult<ApplicationUser>> GetCurrentUserAsync();

        public Task<IAsyncDataResult<EditUserDto>> EditUserInformationAsync(EditUserDto userDto);

        public Task<IAsyncResult> SendPasswordResetLinkAsync(UserForgotPasswordDto userDto);
        public Task<IAsyncResult> ResetUserPasswordAsync(UserResetPasswordDto userDto, Guid userId, string token);
    }
}
