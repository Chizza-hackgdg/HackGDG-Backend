﻿
using AutoMapper;
using Dto.AutoMapper.ApplicationUsers;
using Dto.AutoMapper.ForumCategories;
using Dto.AutoMapper.ForumComments;
using Dto.AutoMapper.ForumPosts;
using Dto.AutoMapper.Milestones;
using Dto.AutoMapper.Roles;
using Dto.AutoMapper.UserProfessions;
using Dto.ValidationRules;
using Entity;
using Microsoft.AspNetCore.Identity;
using Service;
using Service.Abstracts;
using Service.Concretes;
using WebAPI;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddIdentity<ApplicationUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_!?.-";
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;


                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddPasswordValidator<PasswordValidator>().AddUserValidator<UserValidator>().
            AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        }

        public static void AddServicesWithExtensions(this IServiceCollection services)
        {
            services.AddHostedService<WeeklyResetService>();
            services.AddAutoMapper(
       typeof(Program),
       typeof(ForumPostProfile),
       typeof(ForumCategoryProfile),
       typeof(ForumCommentProfile),
       typeof(MilestoneProfile),
       typeof(UserProfessionProfile),
       typeof(UserProfile),
       typeof(RoleProfile)
       );
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IForumCategoryService, ForumCategoryService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IMilestoneService, MilestoneService>();
            services.AddScoped<IUserProfessionService, UserProfessionService>();
            services.AddHttpClient<IGeminiService, GeminiService>();
            services.AddScoped<IGeminiService, GeminiService>();
            services.AddScoped(typeof(ITBaseService<,>), typeof(TBaseService<,>));

        }
    }
}
