
using AspNetCoreIdentityApp.Web.Extensions;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Add Identity services
            StartupExtension.AddIdentityWithExt(builder.Services);
            StartupExtension.AddServicesWithExtensions(builder.Services);

            // Configure cookie settings
            builder.Services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder
                {
                    Name = "ChizzaHackGDGCookie"
                };

                opt.LoginPath = new PathString("/Home/SignIn");
                opt.LogoutPath = new PathString("/User/LogOut");
                opt.AccessDeniedPath = new PathString("/User/AccessDenied");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
