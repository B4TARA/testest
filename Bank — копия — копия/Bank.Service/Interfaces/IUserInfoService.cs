using Bank.Domain.Models;
using Bank.Domain.Response;
using Bank.Domain.ViewModels.UserInfo;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service.Interfaces
{
    public interface IUserInfoService
    {
        IBaseResponse<List<UserInfo>> GetAllUserInfo();
        Task<IBaseResponse<UserInfo>> GetUserInfo(int id);
        Task<IBaseResponse<bool>> DeleteUserInfo(int id);
        Task<IBaseResponse<UserInfo>> CreateUserInfo(UserInfoViewModel model);
        Task<IBaseResponse<UserInfo>> EditUserInfo(int id, UserInfoViewModel model);


    }
}
