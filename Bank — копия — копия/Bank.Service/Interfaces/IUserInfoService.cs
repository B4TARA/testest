using Bank.Domain.Models;
using Bank.Domain.Response;
using Bank.Domain.ViewModels.UserInfo;

namespace Bank.Service.Interfaces
{
    public interface IUserInfoService
    {
        Task<BaseResponse<IEnumerable<UserInfo>>> GetAllUserInfo();
        Task<BaseResponse<UserInfo>> GetUserInfo(string login);
        Task<IBaseResponse<bool>> DeleteUserInfo(int service_number);
        Task<IBaseResponse<UserInfo>> CreateUserInfo(UserInfoViewModel model);
        Task<IBaseResponse<UserInfo>> EditUserInfo(int service_number, UserInfoViewModel model);
        Task<IBaseResponse<bool>> ImportUserInfo(UserInfoViewModel model);

        //Task<IBaseResponse<bool>> ExcelParser(UserInfoViewModel model);
    }
}
