using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class WeeklyResetService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public WeeklyResetService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate the time until the next reset (e.g., Sunday at midnight)
                var now = DateTime.UtcNow;
                var nextReset = now.Date.AddDays(7 - (int)now.DayOfWeek).AddHours(0); // Adjust to your desired reset time
                var delay = nextReset - now;

                // Wait until the reset time
                await Task.Delay(delay, stoppingToken);

                // Perform the reset
                await ResetDataAsync();
            }
        }

        private async Task ResetDataAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var milestoneService = scope.ServiceProvider.GetRequiredService<IMilestoneService>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>(); // Assuming IUserService manages users
                 
                // Assuming IUserService manages users

                // Get all users
                var users = await userService.GetAllUsersAsync();

                foreach (var user in await users.Data)
                {
                    // Get the count of completed milestones for the user
                    var completedMilestones = await milestoneService.Current.GetAllListAsync(x => x.IsCompleted && x.UserId == user.Id);

                    if (completedMilestones.Count > 5)
                    {
                        var userToUpdate = await userManager.FindByIdAsync(user.Id.ToString());
                        userToUpdate.MilestonesAchieved += completedMilestones.Count;

                        if (userToUpdate.MilestonesAchieved >= 15)
                        {
                            //var userProfessions = await userService.GetUserProfessionByUserIdAndProfessionId(userId, professionId);
                        }

                        // Update the user

                        await userManager.UpdateAsync(userToUpdate);
                    }
                }
            }
        }
    }
}
