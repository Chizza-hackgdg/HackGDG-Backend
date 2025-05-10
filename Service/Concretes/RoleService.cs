using AutoMapper;
using Azure.Core;
using Core.Utilities.Results;
using Dto.Roles;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Service.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<AppRole> roleManager, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IAsyncDataResult<List<AssignRoleToUserDto>>> GetUserRolesAsync(Guid id)
        {
            var currentUser = await _userManager.FindByIdAsync(id.ToString());
            if (currentUser == null)
            {
                return new AsyncErrorDataResult<List<AssignRoleToUserDto>>(Task.FromResult("Kullanıcı bulunamadı!"));
            }
            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var roleDtoList = new List<AssignRoleToUserDto>();


            foreach (var role in roles)
            {
               var assignRoleToUser = _mapper.Map<AssignRoleToUserDto>(role);

                if (userRoles.Contains(role.Name!))
                {
                    assignRoleToUser.IsAssigned = true;
                }

                roleDtoList.Add(assignRoleToUser);
            }

            return new AsyncSuccessDataResult<List<AssignRoleToUserDto>>(Task.FromResult(roleDtoList));
        }
        public async Task<IAsyncResult> AssignRoleToUserAsync(string userId, List<AssignRoleToUserDto> roleDto)
        {
            var userToAssign = await _userManager.FindByIdAsync(userId);
            if (userToAssign == null)
            {
                return new AsyncErrorResult(Task.FromResult("Kullanıcı bulunamadı!"));
            }

            foreach (var role in roleDto)
            {
                if (role.IsAssigned)
                {
                    // Add role to user
                    await _userManager.AddToRoleAsync(userToAssign, role.Name);

                    // Add claim to user
                    var claim = new Claim(ClaimTypes.Role, role.Name);
                    await _userManager.AddClaimAsync(userToAssign, claim);
                }
                else
                {
                    // Remove role from user
                    await _userManager.RemoveFromRoleAsync(userToAssign, role.Name);

                    // Remove claim from user
                    var claim = new Claim(ClaimTypes.Role, role.Name);
                    await _userManager.RemoveClaimAsync(userToAssign, claim);
                }
                

            }
            var currentUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext?.User.Identity!.Name!);
            if (userId == currentUser!.Id.ToString() )
            {
                await _signInManager.RefreshSignInAsync(userToAssign);
            }

            return new AsyncSuccessResult();
        }

        public async Task<IAsyncResult> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var role = _mapper.Map<AppRole>(roleDto);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new AsyncErrorResult(Task.FromResult(errors));
            }
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncResult> DeleteRoleAsync(Guid id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id.ToString());
            if (roleToDelete == null)
            {
                return new AsyncErrorResult(Task.FromResult("Rol bulunamadı!"));

            }
            var result = await _roleManager.DeleteAsync(roleToDelete);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new AsyncErrorResult(Task.FromResult(errors));
            }
            return new AsyncSuccessResult();
        }

        public Task<List<ListRoleDto>> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesMap = _mapper.Map<List<ListRoleDto>>(roles);
            return Task.FromResult(rolesMap);
        }

        public async Task<IAsyncResult> UpdateRoleAsync(UpdateRoleDto roleDto)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(roleDto.Id);


            if (roleToUpdate == null)
            {
                return new AsyncErrorResult(Task.FromResult("Rol bulunamadı!"));

            }
            roleToUpdate.Name = roleDto.Name;

            var result = await _roleManager.UpdateAsync(roleToUpdate);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new AsyncErrorResult(Task.FromResult(errors));
            }
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncDataResult<UpdateRoleDto>> GetCurrentRoleToUpdateAsync(Guid id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id.ToString());

            if (roleToUpdate == null)
            {
                return new AsyncErrorDataResult<UpdateRoleDto>(Task.FromResult("Rol bulunamadı!"));

            }
            var roleMap = _mapper.Map<UpdateRoleDto>(roleToUpdate);
            return new AsyncSuccessDataResult<UpdateRoleDto>(Task.FromResult(roleMap));
        }
    }
}
