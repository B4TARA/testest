using Bank.DAL.Interfaces;
using Bank.DAL.Repositories;
using Bank.Domain.Models;
using Bank.Models;
using Bank.Service.Implementations;
using Bank.Service.Interfaces;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cors.Infrastructure;
using UserInfo = Bank.Domain.Models.UserInfo;

namespace Bank
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<UserInfo>, UserInfoRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
