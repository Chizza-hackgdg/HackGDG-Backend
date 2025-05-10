using Core.Utilities.Results;
using Dto.Roles;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;


namespace Service.Abstracts
{
    public interface IRoleService
    {
        public Task<List<ListRoleDto>> GetAllRolesAsync();
        public Task<IAsyncResult> CreateRoleAsync(CreateRoleDto roleDto);
        public Task<IAsyncDataResult<UpdateRoleDto>> GetCurrentRoleToUpdateAsync(Guid id);
        public Task<IAsyncResult> UpdateRoleAsync(UpdateRoleDto roleDto);
        public Task<IAsyncResult> DeleteRoleAsync(Guid id);
        public Task<IAsyncDataResult<List<AssignRoleToUserDto>>> GetUserRolesAsync(Guid id);
        public Task<IAsyncResult> AssignRoleToUserAsync(string userId, List<AssignRoleToUserDto> roleDto);
    }
}
