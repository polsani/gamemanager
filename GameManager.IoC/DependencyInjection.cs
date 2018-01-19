using GameManager.Application.Helpers;
using GameManager.Application.Services;
using GameManager.Data;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Helpers;
using GameManager.Domain.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameManager.IoC
{
    public class DependencyInjection
    {
        public static void RegisterApplicationComponents(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddSingleton<IEmailHelper, EmailHelper>();
            services.AddScoped<IDunService, DunService>();
        }
    }
}
